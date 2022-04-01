using AutoMapper;
using Contracts;
using Entities.Dto.Product;
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
    public class ProductsControllerTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<ILoggerManager> _loggerMock;
        private Mock<IRepositoryManager> _repositoryMock;

        [Fact]
        public void GetProduct_TestResult_ReturnStatusOk()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();

            var controller = new ProductsController(null, null, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = controller.GetProduct(It.IsAny<Guid>());

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetProduct_TestMapper_MapMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<ProductDto>(It.IsAny<Product>()));

            var controller = new ProductsController(null, null, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = controller.GetProduct(It.IsAny<Guid>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task GetProducts_TestResult_ReturnStatusOk()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.Product
                .GetAllProductsAsync(It.IsAny<ProductParameters>(), false))
                .Returns(Task.FromResult(GetPagedListOfProducts()));

            var controller = new ProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.GetProducts(It.IsAny<ProductParameters>());

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetProducts_TestRepository_GetAllProductsAsyncMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.Product
                .GetAllProductsAsync(It.IsAny<ProductParameters>(), false))
                .Returns(Task.FromResult(GetPagedListOfProducts()));

            var controller = new ProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.GetProducts(It.IsAny<ProductParameters>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetProducts_TestMapper_MapMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Product>>()));

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(
                rp => rp.Product
                .GetAllProductsAsync(It.IsAny<ProductParameters>(), false))
                .Returns(Task.FromResult(GetPagedListOfProducts()));

            var controller = new ProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.GetProducts(It.IsAny<ProductParameters>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task CreateProduct_TestResult_ReturnStatusCreated()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(new ProductDto());

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Product.CreateProduct(It.IsAny<Product>()));

            var controller = new ProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.CreateProduct(It.IsAny<ProductForCreationDto>());

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task CreateProduct_TestMapper_MapMustBeCalled2Times()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(new ProductDto());
            _mapperMock.Setup(mp => mp.Map<Product>(It.IsAny<ProductForCreationDto>()));

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Product.CreateProduct(It.IsAny<Product>()));

            var controller = new ProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.CreateProduct(It.IsAny<ProductForCreationDto>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task CreateProduct_TestRepository_CreateAndSaveMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(new ProductDto());

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Product.CreateProduct(It.IsAny<Product>()));
            _repositoryMock.Setup(rp => rp.SaveAsync());

            var controller = new ProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.CreateProduct(It.IsAny<ProductForCreationDto>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteProduct_TestResult_ReturnStatusNoContent()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Product.DeleteProduct(It.IsAny<Product>()));

            var controller = new ProductsController(null, _repositoryMock.Object, null);
            SetContext(controller);

            //Act
            var result = await controller.DeleteProduct(It.IsAny<Guid>());

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_TestRepository_DeleteAndSaveMustBeCalled()
        {
            //Arrange
            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.Product.DeleteProduct(It.IsAny<Product>()));
            _repositoryMock.Setup(rp => rp.SaveAsync());

            var controller = new ProductsController(null, _repositoryMock.Object, null);
            SetContext(controller);

            //Act
            var result = await controller.DeleteProduct(It.IsAny<Guid>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_TestResult_ReturnStatusNoContent()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map(It.IsAny<ProductForUpdateDto>(), It.IsAny<Product>()));

            _repositoryMock = new Mock<IRepositoryManager>();

            var controller = new ProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.UpdateProduct(It.IsAny<Guid>(), It.IsAny<ProductForUpdateDto>());

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_TestMapper_MapMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map(It.IsAny<ProductForUpdateDto>(), It.IsAny<Product>()));

            _repositoryMock = new Mock<IRepositoryManager>();

            var controller = new ProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.UpdateProduct(It.IsAny<Guid>(), It.IsAny<ProductForUpdateDto>());

            //Assert
            _mapperMock.VerifyAll();
        }

        [Fact]
        public async Task UpdateProduct_TestRepository_SaveMustBeCalled()
        {
            //Arrange
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(mp => mp.Map(It.IsAny<ProductForUpdateDto>(), It.IsAny<Product>()));

            _repositoryMock = new Mock<IRepositoryManager>();
            _repositoryMock.Setup(rp => rp.SaveAsync());

            var controller = new ProductsController(null, _repositoryMock.Object, _mapperMock.Object);
            SetContext(controller);

            //Act
            var result = await controller.UpdateProduct(It.IsAny<Guid>(), It.IsAny<ProductForUpdateDto>());

            //Assert
            _repositoryMock.VerifyAll();
        }

        private PagedList<Product> GetPagedListOfProducts()
        {
            return new PagedList<Product>(new List<Product>(), 0, 0, 1);
        }
        private void SetContext(ProductsController controller)
        {
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }
    }
}