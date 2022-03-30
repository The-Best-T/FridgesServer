using AutoMapper;
using Entities.Dto.Product;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Controllers;
using System;
using Xunit;

namespace Server.Tests
{
    public class ProductsControllerTests
    {

        [Fact]
        public void GetProduct_ReturnStatusCodeOk()
        {
            //Arrange
            var mock=new Mock<IMapper>();
            mock.Setup(mp=>mp.Map<ProductDto>(new Product())).Returns(new ProductDto());

            var controller = new ProductsController(null,null,mock.Object);
            controller.ControllerContext=new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            //Act
            var result = controller.GetProduct(Guid.Empty);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        public Product GetOneTestProduct()
        {
            return new Product
            {
                Name = "TestProduct",
                DefaultQuantity = 1,
            };
        }
    }
}
