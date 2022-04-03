using AutoMapper;
using Contracts;
using Entities.Dto.FridgeProduct;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NLog;
using Server.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Server.Tests.ControllersTests
{
    public class FridgeProductsControllerTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<ILoggerManager> _loggerMock;
        private Mock<IRepositoryManager> _repositoryMock;

        [Fact]
        public void GetProductForFridge_TestResult_ReturnOkObjectResult()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();

            var controller = new FridgeProductsController(null, null, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = controller.GetProductForFridge(It.IsAny<Guid>(), It.IsAny<Guid>());

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetProductForFridge_TestMapper_MapMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<FridgeProductDto>(It.IsAny<FridgeProduct>()));

            var controller = new FridgeProductsController(null, null, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = controller.GetProductForFridge(It.IsAny<Guid>(), It.IsAny<Guid>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task GetProductsForFridge_TestResult_ReturnOkObjectResult()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.FridgeProduct
                .GetProductsForFridgeAsync(It.IsAny<Guid>(), It.IsAny<FridgeProductParameters>(), true))
                .Returns(Task.FromResult(GetPagedListOfFridgeProducts()));

            var controller = new FridgeProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.GetProductsForFridge(It.IsAny<Guid>(), It.IsAny<FridgeProductParameters>());

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetProductsForFridge_TestRepository_GetAllFridgesAsyncMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.FridgeProduct
                .GetProductsForFridgeAsync(It.IsAny<Guid>(), It.IsAny<FridgeProductParameters>(), true))
                .Returns(Task.FromResult(GetPagedListOfFridgeProducts()));

            var controller = new FridgeProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.GetProductsForFridge(It.IsAny<Guid>(), It.IsAny<FridgeProductParameters>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetProductsForFridge_TestMapper_MapMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<IEnumerable<FridgeProductDto>>(It.IsAny<IEnumerable<FridgeProduct>>()));

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.FridgeProduct
                .GetProductsForFridgeAsync(It.IsAny<Guid>(), It.IsAny<FridgeProductParameters>(), true))
                .Returns(Task.FromResult(GetPagedListOfFridgeProducts()));

            var controller = new FridgeProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.GetProductsForFridge(It.IsAny<Guid>(), It.IsAny<FridgeProductParameters>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task AddProductInFridge_TestResult_ReturnStatusCreated()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<FridgeProduct>(It.IsAny<FridgeProductForCreationDto>()));
            _mapperMock.Setup(mp => mp.Map<FridgeProductDto>(It.IsAny<FridgeProduct>())).Returns(new FridgeProductDto());

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Product.GetProductAsync(It.IsAny<Guid>(), false))
                .Returns(Task.FromResult(new Product()));
            _repositoryMock.Setup(rp => rp.FridgeProduct.AddProductInFridge(It.IsAny<Guid>(), It.IsAny<FridgeProduct>()));

            var controller = new FridgeProductsController(null, _repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await controller.AddProductInFridge(It.IsAny<Guid>(), new FridgeProductForCreationDto());

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task AddProductInFridge_TestResult_ReturnStatusNotFound()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Product.GetProductAsync(It.IsAny<Guid>(), false))
                .Returns(Task.FromResult((Product)null));

            _loggerMock = new Mock<ILoggerManager>();
            _loggerMock.Setup(l => l.LogInfo(It.IsAny<string>()));

            var controller = new FridgeProductsController(_loggerMock.Object, _repositoryMock.Object, null);

            //Act
            var result = await controller.AddProductInFridge(It.IsAny<Guid>(), new FridgeProductForCreationDto());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddProductInFridge_TestRepository_FindProductCreateAndSaveMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<FridgeProduct>(It.IsAny<FridgeProductForCreationDto>()));
            _mapperMock.Setup(mp => mp.Map<FridgeProductDto>(It.IsAny<FridgeProduct>())).Returns(new FridgeProductDto());

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Product.GetProductAsync(It.IsAny<Guid>(), false))
                .Returns(Task.FromResult(new Product()));
            _repositoryMock.Setup(rp => rp.FridgeProduct.AddProductInFridge(It.IsAny<Guid>(), It.IsAny<FridgeProduct>()));
            _repositoryMock.Setup(rp => rp.SaveAsync());

            var controller = new FridgeProductsController(null, _repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await controller.AddProductInFridge(It.IsAny<Guid>(), new FridgeProductForCreationDto());

            //Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task AddProductInFridge_TestMapper_MapMustBeCalled2Times()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<FridgeProduct>(It.IsAny<FridgeProductForCreationDto>()));
            _mapperMock.Setup(mp => mp.Map<FridgeProductDto>(It.IsAny<FridgeProduct>())).Returns(new FridgeProductDto());

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Product.GetProductAsync(It.IsAny<Guid>(), false))
                .Returns(Task.FromResult(new Product()));
            _repositoryMock.Setup(rp => rp.FridgeProduct.AddProductInFridge(It.IsAny<Guid>(), It.IsAny<FridgeProduct>()));

            var controller = new FridgeProductsController(null, _repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await controller.AddProductInFridge(It.IsAny<Guid>(), new FridgeProductForCreationDto());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteProductFromFridge_TestResult_ReturnStatusNoContent()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.FridgeProduct.DeleteProductFromFridge(It.IsAny<FridgeProduct>()));

            var controller = new FridgeProductsController(null, _repositoryMock.Object, null);
            SetContext(controller);

            //Act
            var result = await controller.DeleteProductFromFridge(It.IsAny<Guid>(), It.IsAny<Guid>());

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProductFromFridge_TestRepository_DeleteAndSaveMustBeCalled()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.FridgeProduct.DeleteProductFromFridge(It.IsAny<FridgeProduct>()));
            _repositoryMock.Setup(rp => rp.SaveAsync());

            var controller = new FridgeProductsController(null, _repositoryMock.Object, null);
            SetContext(controller);

            //Act
            var result = await controller.DeleteProductFromFridge(It.IsAny<Guid>(), It.IsAny<Guid>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateProductForFridge_TestResult_ReturnStatusNoContent()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map(It.IsAny<FridgeProductForUpdateDto>(), It.IsAny<FridgeProduct>()));

            _repositoryMock = new Mock<IRepositoryManager>();

            var controller = new FridgeProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller
                .UpdateProductForFridge(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<FridgeProductForUpdateDto>());

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateProductForFridge_TestMapper_MapMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map(It.IsAny<FridgeProductForUpdateDto>(), It.IsAny<FridgeProduct>()));

            _repositoryMock = new Mock<IRepositoryManager>();

            var controller = new FridgeProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.UpdateProductForFridge(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<FridgeProductForUpdateDto>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateProductForFridge_TestRepository_SaveMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map(It.IsAny<FridgeProductForUpdateDto>(), It.IsAny<FridgeProduct>()));

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.SaveAsync());

            var controller = new FridgeProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.UpdateProductForFridge(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<FridgeProductForUpdateDto>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        private void SetContext(FridgeProductsController controller)
        {
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }
        private PagedList<FridgeProduct> GetPagedListOfFridgeProducts()
        {
            return new PagedList<FridgeProduct>(new List<FridgeProduct>(), 0, 0, 1);
        }
    }
}