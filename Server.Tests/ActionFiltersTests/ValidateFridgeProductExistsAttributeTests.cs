using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using NLog;
using Server.ActionFilters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Server.Tests.ActionFiltersTests
{
    public class ValidateFridgeProductExistsAttributeTests
    {
        private Mock<ILoggerManager> _loggerMock;
        private Mock<IRepositoryManager> _repositoryMock;

        [Fact]
        public async Task OnActionExecutionAsync_TestResult_GoToAction()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Fridge.GetFridgeAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new Fridge()));
            _repositoryMock.Setup(
                rp => rp.FridgeProduct.GetProductForFridgeAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new FridgeProduct()));

            var controller = new ValidateFridgeProductExistsAttribute(null, _repositoryMock.Object);

            var (context, next) = GetContextAndDelegate(controller);

            context.ActionArguments.Add("fridgeId", It.IsAny<Guid>());
            context.ActionArguments.Add("productId", It.IsAny<Guid>());

            //Act
            await controller.OnActionExecutionAsync(context, next);

            //Asser
            Assert.NotNull(context.HttpContext.Items["EndOfTest"]);
        }

        [Fact]
        public async Task OnActionExecutionAsync_TestHttpItems_SetFridgeProduct()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Fridge.GetFridgeAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new Fridge()));
            _repositoryMock.Setup(
                rp => rp.FridgeProduct.GetProductForFridgeAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new FridgeProduct()));

            var controller = new ValidateFridgeProductExistsAttribute(null, _repositoryMock.Object);

            var (context, next) = GetContextAndDelegate(controller);

            context.ActionArguments.Add("fridgeId", It.IsAny<Guid>());
            context.ActionArguments.Add("productId", It.IsAny<Guid>());

            //Act
            await controller.OnActionExecutionAsync(context, next);

            //Asser
            Assert.NotNull(context.HttpContext.Items["fridgeProduct"]);
        }

        [Fact]
        public async Task OnActionExecutionAsync_TestResult_ReturnNotFoundFridge()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Fridge.GetFridgeAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .Returns(Task.FromResult((Fridge)null));

            _loggerMock = new Mock<ILoggerManager>();
            _loggerMock.Setup(l => l.LogInfo(It.IsAny<string>()));

            var controller = new ValidateFridgeProductExistsAttribute(_loggerMock.Object, _repositoryMock.Object);

            var (context, next) = GetContextAndDelegate(controller);

            context.ActionArguments.Add("fridgeId", It.IsAny<Guid>());

            //Act
            await controller.OnActionExecutionAsync(context, next);

            //Asser
            Assert.IsType<NotFoundResult>(context.Result);
        }

        [Fact]
        public async Task OnActionExecutionAsync_TestResult_ReturnNotFoundFridgeProduct()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Fridge.GetFridgeAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new Fridge()));
            _repositoryMock.Setup(
                rp => rp.FridgeProduct.GetProductForFridgeAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool>()))
                .Returns(Task.FromResult((FridgeProduct)null));

            _loggerMock = new Mock<ILoggerManager>();
            _loggerMock.Setup(l => l.LogInfo(It.IsAny<string>()));

            var controller = new ValidateFridgeProductExistsAttribute(_loggerMock.Object, _repositoryMock.Object);

            var (context, next) = GetContextAndDelegate(controller);

            context.ActionArguments.Add("fridgeId", It.IsAny<Guid>());
            context.ActionArguments.Add("productId", It.IsAny<Guid>());

            //Act
            await controller.OnActionExecutionAsync(context, next);

            //Asser
            Assert.IsType<NotFoundResult>(context.Result);
        }

        [Fact]
        public async Task OnActionExecutionAsync_TestRepository_GetFridgeAndGetFridgeProductMustBeCalled()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Fridge.GetFridgeAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new Fridge()));
            _repositoryMock.Setup(
                rp => rp.FridgeProduct.GetProductForFridgeAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new FridgeProduct()));

            var controller = new ValidateFridgeProductExistsAttribute(null, _repositoryMock.Object);

            var (context, next) = GetContextAndDelegate(controller);

            context.ActionArguments.Add("fridgeId", It.IsAny<Guid>());
            context.ActionArguments.Add("productId", It.IsAny<Guid>());

            //Act
            await controller.OnActionExecutionAsync(context, next);

            //Asser
            _repositoryMock.VerifyAll();
        }

        private (ActionExecutingContext, ActionExecutionDelegate) GetContextAndDelegate(ValidateFridgeProductExistsAttribute controller)
        {
            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
            var metadata = new List<IFilterMetadata>();

            var context = new ActionExecutingContext(
                actionContext,
                metadata,
                new Dictionary<string, object>(),
                controller);

            ActionExecutionDelegate next = () =>
            {
                var ctx = new ActionExecutedContext(actionContext, metadata, controller);
                context.HttpContext.Items.Add("EndOfTest", 125);
                return Task.FromResult(ctx);
            };

            return (context, next);
        }
    }
}
