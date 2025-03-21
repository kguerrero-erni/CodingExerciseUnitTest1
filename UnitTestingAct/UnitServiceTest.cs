using NUnit.Framework;
using CodingExerciseUnitTest1;
using Shouldly;
using Moq;

namespace UnitTestingAct
{
    [TestFixture]
    public class UserServiceTest
    {
        private IUserService _userService;
        private Mock<IUserService> _mockUserService;

        [SetUp]
        public void Setup()
        {
            _userService = new UserService();
            _mockUserService = new Mock<IUserService>();
        }

        [Test]
        public void AddUser_ShouldAddUserToList()
        {
            var userName = "Danilo Pelaso";

            var user = _userService.AddUser(userName);
            _mockUserService.Setup(x => x.AddUser(userName)).Returns(user);

            _userService.GetAllUsers().Count.ShouldBe(1);
        }

        [Test]
        public void UpdateUser_ShouldUpdateUser()
        {
            var user = _userService.AddUser("Danilo Pelaso");
            var newName = "John Wick";
            _mockUserService.Setup(x => x.UpdateUser(user.Id, newName)).Returns(true);

            var result = _userService.UpdateUser(user.Id, newName);

            result.ShouldBeTrue();
        }

        [Test]
        public void DeleteUser_ShouldDeleteUser()
        {
            var user = _userService.AddUser("Danilo Pelaso");
            _mockUserService.Setup(x => x.DeleteUser(user.Id)).Returns(true);

            var result = _userService.DeleteUser(user.Id);

            result.ShouldBeTrue();
        }

        [Test]
        public void GetUserById_ShouldReturnUser()
        {
            var user = _userService.AddUser("Danilo Pelaso");
            _mockUserService.Setup(x => x.GetUserById(user.Id)).Returns(user);

            var retrievedUser = _userService.GetUserById(user.Id);

            retrievedUser.ShouldNotBeNull();
        }

        [Test]
        public void AddUser_WithEmptyName_ShouldThrowException()
        {
            _mockUserService.Setup(x => x.AddUser(string.Empty)).Throws(new ArgumentException("user name shouldn't be empty"));

            Should.Throw<ArgumentException>(() => _userService.AddUser(string.Empty));
        }

        [Test]
        public void UpdateUser_WithEmptyName_ShouldReturnFalse()
        {
            var user = _userService.AddUser("Danilo Pelaso");
            _mockUserService.Setup(x => x.UpdateUser(user.Id, string.Empty)).Returns(false);

            var result = _userService.UpdateUser(user.Id, string.Empty);

            result.ShouldBeFalse();
        }

        [Test]
        public void UpdateUser_NonExistentUser_ShouldReturnFalse()
        {
            var nonExistentUserId = 999;
            var newName = "not found :<";
            _mockUserService.Setup(x => x.UpdateUser(nonExistentUserId, newName)).Returns(false);

            var result = _userService.UpdateUser(nonExistentUserId, newName);

            result.ShouldBeFalse();
        }

        [Test]
        public void DeleteUser_NonExistentUser_ShouldReturnFalse()
        {
            var nonExistentUserId = 12;
            _mockUserService.Setup(x => x.DeleteUser(nonExistentUserId)).Returns(false);

            var result = _userService.DeleteUser(nonExistentUserId);

            result.ShouldBeFalse();
        }

        [Test]
        public void GetUserById_NonExistentUser_ShouldReturnNull()
        {
            var nonExistentUserId = 999;
            _mockUserService.Setup(x => x.GetUserById(nonExistentUserId)).Returns((User)null);

            var retrievedUser = _userService.GetUserById(nonExistentUserId);

            retrievedUser.ShouldBeNull();
        }
    }
}