namespace CodingExercuseUnitTest1.Tests;

using CodingExerciseUnitTest1;
using NUnit.Framework;
using Shouldly;

using Moq;
using Moq.Protected;



[TestFixture]
public class Tests
{
    private UserService _userservice;

    [SetUp]
    public void Setup()
    {
        _userservice = new UserService();
    }

    [Test]
    [TestCase("Carl")]
    [TestCase("Ryle")]
    public void AddUser_ShouldAddUser(string userName)
    {
        // Act
        var result = _userservice.AddUser(userName);

        // Assert
        result.Name.ShouldBe(userName);
    }

    [Test]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("                      ")]
    public void AddUser_WithEmptyOrNullName_ShouldThrowArgumentException(string userName)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _userservice.AddUser(userName));
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
            _userservice.AddUser("Carl");
            _userservice.AddUser("Ryle");

            // Act
            var result = _userservice.GetAllUsers();

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
            _userservice.AddUser("Carl");
            _userservice.AddUser("Ryle");

            // Act
            var result = _userservice.GetUserById(userId);

            // Assert
            result.Id.ShouldBe(userId);
            result.Name.ShouldBe(expectedName);
        }

        [Test]
        public void DeleteUser()
        {
            // Arrange
            var id = 1;
            _userservice.AddUser("Carl");

            // Act
            var result = _userservice.DeleteUser(id);

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

            // Act
            var result = _userservice.GetAllUsers();

            // Assert
            result.ShouldBeEmpty();
            
        }

}

