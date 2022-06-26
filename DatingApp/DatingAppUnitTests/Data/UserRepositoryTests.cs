using Api.Data;
using Api.DataAccess;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingAppUnitTests.Data
{
    public class UserRepositoryTests
    {
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

        public UserRepositoryTests()
        {
        }

        [Fact]
        public async void GetUserByNameAsync_GivenMethodCalled_ThenShouldReturnDataFromDataBase()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Users") //  using this, instead of going to original DB, it will call this DB
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DataContext(options))
            {
                context.Users.Add(appUser);
                context.SaveChanges();
                var obj = new UserRepository(context);
                var user = await obj.GetUserByIdAsync(90);
                Assert.Equal(90, user.Id);
                Assert.Equal(DateTime.Now, user.Create_Ts);
            }
        }
    }
}