using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using User.Api.Controllers;
using User.Api.data;
using Xunit;

namespace User.API.UnitTests
{
    public class UserControllerUnitTests
    {
        private UserContext GetUserContext()
        {
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var userContext = new UserContext(options);
            userContext.Users.Add(new Api.model.AppUser() {
                Id =1,
                Name ="jesse"
            });
            userContext.SaveChanges();
            return userContext;
        }
        [Fact]
        public async Task Get_ReturnRigthUser_WithExpectedParameters()
        {
            var context = GetUserContext();
            var loggerMoq = new Mock<ILogger<UserController>>();
            //var controller = new UserController(context, loggerMoq.Object);

            //var response = await controller.Get();
            ////Assert.IsType<JsonResult>(response);

            //var result = response.Should().BeOfType<JsonResult>().Subject;
            //var appUser = result.Value.Should().BeAssignableTo<Api.model.AppUser>().Subject;
            //appUser.Id.Should().Be(1);
            //appUser.Name.Should().Be("jesse");
        }
    }
}
