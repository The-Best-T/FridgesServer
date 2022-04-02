using AutoMapper;
using Contracts;
using Entities.Dto.Fridge;
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
    public class FridgesControllerTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<ILoggerManager> _loggerMock;
        private Mock<IRepositoryManager> _repositoryMock;

        [Fact]
        public void GetFridge_TestResult_ReturnOkObjectResult()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();

            var controller = new FridgesController(null, null, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = controller.GetFridge(It.IsAny<Guid>());

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetFridge_TestMapper_MapMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<FridgeDto>(It.IsAny<Fridge>()));

            var controller = new FridgesController(null, null, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = controller.GetFridge(It.IsAny<Guid>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task GetFridges_TestResult_ReturnOkObjectResult()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.Fridge
                .GetFridgesAsync(It.IsAny<FridgeParameters>(), true))
                .Returns(Task.FromResult(GetPagedListOfFridges()));

            var controller = new FridgesController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.GetFridges(It.IsAny<FridgeParameters>());

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFridges_TestRepository_GetAllFridgesAsyncMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.Fridge
                .GetFridgesAsync(It.IsAny<FridgeParameters>(), true))
                .Returns(Task.FromResult(GetPagedListOfFridges()));

            var controller = new FridgesController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.GetFridges(It.IsAny<FridgeParameters>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetFridges_TestMapper_MapMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<IEnumerable<FridgeDto>>(It.IsAny<IEnumerable<Fridge>>()));

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.Fridge
                .GetFridgesAsync(It.IsAny<FridgeParameters>(), true))
                .Returns(Task.FromResult(GetPagedListOfFridges()));

            var controller = new FridgesController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.GetFridges(It.IsAny<FridgeParameters>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task CreateFridge_TestResult_ReturnStatusCreated()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.FridgeModel
                .GetFridgeModelAsync(It.IsAny<Guid>(), false))
                .Returns(Task.FromResult(new FridgeModel()));
            _repositoryMock.Setup(rp => rp.Fridge.CreateFridge(It.IsAny<Fridge>()));

            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<Fridge>(It.IsAny<FridgeForCreationDto>())).Returns(new Fridge());
            _mapperMock.Setup(mp => mp.Map<FridgeDto>(It.IsAny<Fridge>())).Returns(new FridgeDto());

            var controller = new FridgesController(null, _repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await controller.CreateFridge(new FridgeForCreationDto());

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task CreateFridge_TestResult_ReturnStatusNotFound()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.FridgeModel
                .GetFridgeModelAsync(It.IsAny<Guid>(), false))
                .Returns(Task.FromResult(It.IsAny<FridgeModel>()));

            _loggerMock = new Mock<ILoggerManager>();
            _loggerMock.Setup(l => l.LogInfo(It.IsAny<string>()));

            var controller = new FridgesController(_loggerMock.Object, _repositoryMock.Object, null);

            //Act
            var result = await controller.CreateFridge(new FridgeForCreationDto());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateFridge_TestRepository_FindModelCreateAndSaveMustBeCalled()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.FridgeModel
                .GetFridgeModelAsync(It.IsAny<Guid>(), false))
                .Returns(Task.FromResult(new FridgeModel()));
            _repositoryMock.Setup(rp => rp.Fridge.CreateFridge(It.IsAny<Fridge>()));
            _repositoryMock.Setup(rp => rp.SaveAsync());

            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<Fridge>(It.IsAny<FridgeForCreationDto>())).Returns(new Fridge());
            _mapperMock.Setup(mp => mp.Map<FridgeDto>(It.IsAny<Fridge>())).Returns(new FridgeDto());

            var controller = new FridgesController(null, _repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await controller.CreateFridge(new FridgeForCreationDto());

            //Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task CreateFridge_TestMapper_MapMustBeCalled2Times()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.FridgeModel
                .GetFridgeModelAsync(It.IsAny<Guid>(), false))
                .Returns(Task.FromResult(new FridgeModel()));
            _repositoryMock.Setup(rp => rp.Fridge.CreateFridge(It.IsAny<Fridge>()));

            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<Fridge>(It.IsAny<FridgeForCreationDto>())).Returns(new Fridge());
            _mapperMock.Setup(mp => mp.Map<FridgeDto>(It.IsAny<Fridge>())).Returns(new FridgeDto());

            var controller = new FridgesController(null, _repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await controller.CreateFridge(new FridgeForCreationDto());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteFridge_TestResult_ReturnStatusNoContent()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Fridge.DeleteFridge(It.IsAny<Fridge>()));

            var controller = new FridgesController(null, _repositoryMock.Object, null);
            SetContext(controller);

            //Act
            var result = await controller.DeleteFridge(It.IsAny<Guid>());

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteFridge_TestRepository_DeleteAndSaveMustBeCalled()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Fridge.DeleteFridge(It.IsAny<Fridge>()));
            _repositoryMock.Setup(rp => rp.SaveAsync());

            var controller = new FridgesController(null, _repositoryMock.Object, null);
            SetContext(controller);

            //Act
            var result = await controller.DeleteFridge(It.IsAny<Guid>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateFridge_TestResult_ReturnStatusNoContent()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map(It.IsAny<FridgeForUpdateDto>(), It.IsAny<Fridge>()));

            _repositoryMock = new Mock<IRepositoryManager>();

            var controller = new FridgesController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.UpdateFridge(It.IsAny<Guid>(), It.IsAny<FridgeForUpdateDto>());

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateFridge_TestMapper_MapMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map(It.IsAny<FridgeForUpdateDto>(), It.IsAny<Fridge>()));

            _repositoryMock = new Mock<IRepositoryManager>();

            var controller = new FridgesController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.UpdateFridge(It.IsAny<Guid>(), It.IsAny<FridgeForUpdateDto>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateFridge_TestRepository_SaveMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map(It.IsAny<FridgeForUpdateDto>(), It.IsAny<Fridge>()));

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.SaveAsync());

            var controller = new FridgesController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.UpdateFridge(It.IsAny<Guid>(), It.IsAny<FridgeForUpdateDto>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task FillFridges_TestStoredProcedure_MustInvokeFillFridges()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.StoredProcedureWithoutParamasAsync("[dbo].[FillFridges]"));

            var controller = new FridgesController(null, _repositoryMock.Object, null);

            //Act
            var result = await controller.FillFridges();

            //Assert
            _repositoryMock.VerifyAll();
            Assert.IsType<OkResult>(result);
        }

        private void SetContext(FridgesController controller)
        {
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }
        private PagedList<Fridge> GetPagedListOfFridges()
        {
            return new PagedList<Fridge>(new List<Fridge>(), 0, 0, 1);
        }

    }
}