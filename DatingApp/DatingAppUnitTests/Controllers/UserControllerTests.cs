using Api.Controllers;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiTests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> service;

        private AppUser appUser = new AppUser
        {
            City = "chaibasa",
            Country = "india",
            Create_Ts = DateTime.Now,
            DateOfBirth = new DateTime(1995, 06, 06),
            Gender = "Male",
            Id = 90,
            Interests = "Music",
            Introduction = "blah",
            LastActive = DateTime.Now.AddMinutes(-10d),
            LookingFor = "Female",
            NickName = "Nikhil",
            PasswordHash = new byte[9],
            PasswordSalt = new byte[8],
            Photos = null,
            UserName = "Nikhil"
        };

        public UserControllerTests()
        {
            service = new Mock<IUserRepository>();
        }

        [Fact]
        public async void GetUsersAsync_GivenEndpointCalled_ThenAllAppUsersIsReturned()
        {
            //arrange
            List<AppUser> appUsers = new List<AppUser>();
            appUsers.Add(appUser);
            IEnumerable<AppUser> allUsers = appUsers;
            var x = Task.FromResult(allUsers);  // FromResults converts from sync to async( T to Task<T> )
            service.Setup(x => x.GetUsersAsync()).Returns(x);
            var controller = new UsersController(service.Object);

            //act
            var users = await controller.GetUsers();
            var result = users.Result as OkObjectResult;
            var actualResult = result.Value as List<AppUser>;

            //Asserts
            Assert.Equal(200, result.StatusCode);
            Assert.Single(actualResult);
            Assert.Equal("chaibasa", actualResult[0].City);
        }
    }
}