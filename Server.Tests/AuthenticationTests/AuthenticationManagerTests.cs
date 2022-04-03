using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Server.Authenticate;
using Microsoft.AspNetCore.Identity;
using Entities.Models;
using Entities.Dto.User;

namespace Server.Tests.AuthenticationTests
{
    public class AuthenticationManagerTests
    {
        private Mock<UserManager<User>> _userManagerMock;

        [Fact]
        public async Task ValidateUser_TestResult_ReturnTrueIfUsernameAndPasswordCorrect()
        {
            //Arrange
            var store = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            _userManagerMock.Setup(um=>um.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User()));
            _userManagerMock.Setup(um => um.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            var controller = new AuthenticationManager(_userManagerMock.Object, null);

            //Act
            var result = await controller.ValidateUser(new UserForAuthenticationDto());

            //Asser
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateUser_TestUserManager_FindUserAndCheckPasswordMustBeCalled()
        {
            //Arrange
            var store = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            _userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>()));
            _userManagerMock.Setup(um => um.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()));

            var controller = new AuthenticationManager(_userManagerMock.Object, null);

            //Act
            var result = await controller.ValidateUser(new UserForAuthenticationDto());

            //Asser
            _userManagerMock.VerifyAll();
        }

        [Fact]
        public async Task CreateToken_Test()
        {
            //Arrange
            var controller=new AuthenticationManager(null, null);

            //Act
            var result = await controller.CreateToken();

            //Assert
            Assert.NotNull(result);
        }
    }
}
