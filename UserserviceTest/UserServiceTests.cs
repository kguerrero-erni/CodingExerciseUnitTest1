using CodingExerciseUnitTest1;

using Shouldly;
using NUnit;
using System.Xml.Linq;

namespace UserserviceTests
{
    public class UserServiceTests
    {
        private IUserService _userservice;


        [SetUp]
        public void Setup()
        {

            _userservice = new UserService();

        }

        [Test]
        [TestCase("John")]
        public void AddUser_ShouldReturnUser_WhenUserExist(String name)
        {
            var user = _userservice.AddUser("John");
            var getId = _userservice.GetUserById(user.Id);
            getId.ShouldBe(user);
        }


        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        public void AddUser_ShouldThrowException_WhenNameIsNullorEmpty(string? name)
        {

            var exception = Should.Throw<ArgumentException>(() => _userservice.AddUser(name));
            exception.Message.ShouldBe("User name cannot be empty.");
        }


        [Test]
        [TestCase("John")]
        public void UpdateUser_ShouldUpdateUser_WhenUserExist(String name)
        {

            var user = _userservice.AddUser(name);

            var getId = _userservice.GetUserById(user.Id);
            var result = _userservice.UpdateUser(1, "Paul");

            result.ShouldBeTrue();
        }

        [Test]
        [TestCase(1, "Emmanuel")]
        public void UpdateUser_ShouldReturnFalse_WhenUserDoesNotExist(int id, string newName)
        {

            var result = _userservice.UpdateUser(id, newName);

            result.ShouldBeFalse();
        }

        [Test]
        [TestCase(1)]
        public void UpdateUser_ShouldAddUser_WhenUserDoesNotExist(int id)
        {
            var result = _userservice.UpdateUser(id, "Paul");
            result.ShouldBeFalse();
            var addedUser = _userservice.AddUser("Paul");
            addedUser.Name.ShouldBe("Paul");
        }


        [Test]
        [TestCase("John")]
        public void DeleteUser_ShouldDeleteUser_WhenUserExist(String name)
        {

            var user = _userservice.AddUser(name);

            var getId = _userservice.GetUserById(user.Id);
            var result = _userservice.DeleteUser(1);

            result.ShouldBeTrue();
        }

        [Test]
        [TestCase(1)]
        public void DeleteUser_ShouldReturnFalse_WhenUserDoesNotExist(int id)
        {
            var result = _userservice.DeleteUser(id);
            result.ShouldBeFalse();
        }

        [Test]
        [TestCase("John", "Emmanuel", "Piccolo")]
        public void GetUsers_ShouldReturnAllUsers_WhenUsersExist(params string[] names)
        {

            foreach (var name in names)
            {
                var user = _userservice.AddUser(name);
            }

            var users = _userservice.GetAllUsers();

            users.Count.ShouldBe(names.Length);
            foreach (var name in names)
            {
                users.ShouldContain(u => u.Name == name);
            }
        }

        [Test]
        [TestCase(1)]
        public void GetUserById_ShouldReturnUser_WhenUserExist(int id)
        {
            var user = _userservice.AddUser("John");
            var getId = _userservice.GetUserById(user.Id);
            getId.ShouldBe(user);
        }


       
        [Test]
        [TestCase(1)]
        public void GetUserById_ShouldReturnNull_WhenUserDoesNotExist(int id)
        {

            var user = _userservice.GetUserById(id);

            user.ShouldBeNull();
        }

       
       
    }
}






