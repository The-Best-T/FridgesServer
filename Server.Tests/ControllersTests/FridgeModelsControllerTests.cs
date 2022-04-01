using AutoMapper;
using Contracts;
using Entities.Dto.FridgeModel;
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
    public class FridgeModelsControllerTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<ILoggerManager> _loggerMock;
        private Mock<IRepositoryManager> _repositoryMock;

        [Fact]
        public void GetFridgeModel_TestResult_ReturnStatusOk()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();

            var controller = new FridgeModelsController(null, null, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = controller.GetFridgeModel(It.IsAny<Guid>());

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetFridgeModel_TestMapper_MapMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<FridgeModelDto>(It.IsAny<FridgeModel>()));

            var controller = new FridgeModelsController(null, null, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = controller.GetFridgeModel(It.IsAny<Guid>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task GetFridgeModels_TestResult_ReturnStatusOk()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.FridgeModel
                .GetFridgeModelsAsync(It.IsAny<FridgeModelParameters>(), false))
                .Returns(Task.FromResult(GetPagedListOfFridgeModels()));

            var controller = new FridgeModelsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.GetFridgeModels(It.IsAny<FridgeModelParameters>());

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFridgeModels_TestRepository_GetAllFridgeModelsAsyncMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.FridgeModel
                .GetFridgeModelsAsync(It.IsAny<FridgeModelParameters>(), false))
                .Returns(Task.FromResult(GetPagedListOfFridgeModels()));

            var controller = new FridgeModelsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.GetFridgeModels(It.IsAny<FridgeModelParameters>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetFridgeModels_TestMapper_MapMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<IEnumerable<FridgeModelDto>>(It.IsAny<IEnumerable<FridgeModel>>()));

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.FridgeModel
                .GetFridgeModelsAsync(It.IsAny<FridgeModelParameters>(), false))
                .Returns(Task.FromResult(GetPagedListOfFridgeModels()));

            var controller = new FridgeModelsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.GetFridgeModels(It.IsAny<FridgeModelParameters>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task CreateFridgeModel_TestResult_ReturnStatusCreated()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<FridgeModelDto>(It.IsAny<FridgeModel>()))
                .Returns(new FridgeModelDto());

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.FridgeModel.CreateFridgeModel(It.IsAny<FridgeModel>()));

            var controller = new FridgeModelsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.CreateFridgeModel(It.IsAny<FridgeModelForCreationDto>());

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task CreateFridgeModel_TestMapper_MapMustBeCalled2Times()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<FridgeModelDto>(It.IsAny<FridgeModel>()))
                .Returns(new FridgeModelDto());
            _mapperMock.Setup(mp => mp.Map<FridgeModel>(It.IsAny<FridgeModelForCreationDto>()));

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.FridgeModel.CreateFridgeModel(It.IsAny<FridgeModel>()));

            var controller = new FridgeModelsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.CreateFridgeModel(It.IsAny<FridgeModelForCreationDto>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task CreateFridgeModel_TestRepository_CreateAndSaveMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<FridgeModelDto>(It.IsAny<FridgeModel>()))
                .Returns(new FridgeModelDto());

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.FridgeModel.CreateFridgeModel(It.IsAny<FridgeModel>()));
            _repositoryMock.Setup(rp => rp.SaveAsync());

            var controller = new FridgeModelsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.CreateFridgeModel(It.IsAny<FridgeModelForCreationDto>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteFridgeModel_TestResult_ReturnStatusNoContent()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.FridgeModel.DeleteFridgeModel(It.IsAny<FridgeModel>()));

            var controller = new FridgeModelsController(null, _repositoryMock.Object, null);
            SetContext(controller);

            //Act
            var result = await controller.DeleteFridgeModel(It.IsAny<Guid>());

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteFridgeModel_TestRepository_DeleteAndSaveMustBeCalled()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.FridgeModel.DeleteFridgeModel(It.IsAny<FridgeModel>()));
            _repositoryMock.Setup(rp => rp.SaveAsync());

            var controller = new FridgeModelsController(null, _repositoryMock.Object, null);
            SetContext(controller);

            //Act
            var result = await controller.DeleteFridgeModel(It.IsAny<Guid>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateFridgeModel_TestResult_ReturnStatusNoContent()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map(It.IsAny<FridgeModelForUpdateDto>(),It.IsAny<FridgeModel>()));

            _repositoryMock = new Mock<IRepositoryManager>();

            var controller = new FridgeModelsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.UpdateFridgeModel(It.IsAny<Guid>(), It.IsAny<FridgeModelForUpdateDto>());

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateFridgeModel_TestMapper_MapMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map(It.IsAny<FridgeModelForUpdateDto>(), It.IsAny<FridgeModel>()));

            _repositoryMock = new Mock<IRepositoryManager>();

            var controller = new FridgeModelsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.UpdateFridgeModel(It.IsAny<Guid>(), It.IsAny<FridgeModelForUpdateDto>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateFridgeModel_TestRepository_SaveMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map(It.IsAny<FridgeModelForUpdateDto>(), It.IsAny<FridgeModel>()));

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.SaveAsync());

            var controller = new FridgeModelsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.UpdateFridgeModel(It.IsAny<Guid>(), It.IsAny<FridgeModelForUpdateDto>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        private void SetContext(FridgeModelsController controller)
        {
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }
        private PagedList<FridgeModel> GetPagedListOfFridgeModels()
        {
            return new PagedList<FridgeModel>(new List<FridgeModel>(), 0, 0, 1);
        }

    }
}