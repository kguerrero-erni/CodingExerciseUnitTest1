namespace CodingExercuseUnitTest1.Tests;

using CodingExerciseUnitTest1;
using NUnit.Framework;
using Shouldly;

using Moq;
using Moq.Protected;

   

[TestFixture]
public class Tests2
{
    private readonly UserService _userservice;
    private readonly Mock<IUserService> _mockUserService;

    public Tests2 () {
        _userservice = new UserService();
        _mockUserService = new Mock<IUserService>();
    }

    [Test]
    [TestCase("Carl")]
    [TestCase("Ryle")]
    public void AddUser_ShouldAddUser(string userName)
    {
        // Arrange
            var user = new User { Name = userName};
           _mockUserService.Setup(s => s.AddUser(It.IsAny<string>())).Returns(user);

        // Act
            var result = _mockUserService.Object.AddUser(userName);

        // Assert
            result.Name.ShouldBe(userName);
    }

    [Test]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("                      ")]
    public void AddUser_WithEmptyOrNullName_ShouldThrowArgumentException(string userName)
    {
        // Arrange
        _mockUserService.Setup(s => s.AddUser(It.IsAny<string>())).Throws(new ArgumentException("User name cannot be empty."));
 
         // Act
         var ex = Assert.Throws<ArgumentException>(() => _mockUserService.Object.AddUser(userName));

         // Assert
         ex.Message.ShouldBe("User name cannot be empty.");

    }

    [Test]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "Carl" },
                new User { Id = 2, Name = "Ryle" }
            };
            _mockUserService.Setup(s => s.GetAllUsers()).Returns(users);

            // Act
            var result = _mockUserService.Object.GetAllUsers();

            // Assert
            foreach(var user in users) {
                var resultUser = result.FirstOrDefault(u => u.Id == user.Id);
                resultUser.Id.ShouldBe(user.Id);
                resultUser.Name.ShouldBe(user.Name);
            }
            
        }

        [Test]
        [TestCase(1, "Carl")]
        [TestCase(2, "Ryle")]
        public void GetUserById_ShouldReturnUserById(int userId, string expectedName)
        {
            // Arrange
            var user = new User { Id = userId, Name = expectedName };
            _mockUserService.Setup(s => s.GetUserById(userId)).Returns(user);
            // Act
            var result = _mockUserService.Object.GetUserById(userId);

            // Assert
            result.Id.ShouldBe(userId);
            result.Name.ShouldBe(expectedName);
        }

        [Test]
        public void DeleteUser()
        {
            // Arrange
            var id = 1;
            _mockUserService.Setup(s => s.DeleteUser(id)).Returns(true);

            // Act
            var result = _mockUserService.Object.DeleteUser(id);

            // Assert
           result.ShouldBeTrue();
            
        }

        [Test]
        public void UpdateUser()
        {
            // Arrange
            var userId = 1;
            var newName = "Carl";
            _userservice.AddUser("Karl");

            // Act
            var result = _userservice.UpdateUser(userId, newName);
            var updated = _userservice.GetUserById(userId);

            // Assert
           result.ShouldBeTrue();
           updated.Name.ShouldBe(newName);
            
        }

        [Test]
        public void GetAllUsers_ShouldEmptyUsers()
        {
            // Arrange
            _mockUserService.Setup(s => s.GetAllUsers()).Returns(new List<User>());
            // Act
            var result = _mockUserService.Object.GetAllUsers();

            // Assert
            result.ShouldBeEmpty();
            
        }


}

