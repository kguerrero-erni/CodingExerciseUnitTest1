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
            var result = _userservice.AddUser(name);
            //Assert
            result.Name.ShouldBe(name);
        }

        [Test]
        [TestCase("John")]
        public void UpdateUser_ShouldUpdateUser_WhenUserExist(String name)
        {
            //Arange
            var user = _userservice.AddUser(name);
            //Act
            var getId = _userservice.GetUserById(user.Id);
            var result = _userservice.UpdateUser(1, "Paul");
            //Assert
            result.ShouldBeTrue();
        }

        [Test]
        [TestCase("John")]
        public void DeleteUser_ShouldDeleteUser_WhenUserExist(String name)
        {
            //Arange
            var user = _userservice.AddUser(name);
            //Act
            var getId = _userservice.GetUserById(user.Id);
            var result = _userservice.DeleteUser(1);
            //Assert
            result.ShouldBeTrue();
        }

        [Test]
        [TestCase("John", "Emmanuel", "Piccolo")]
        public void GetUsers_ShouldReturnAllUsers_WhenUserExist(params string[] names)
        {
            // Arrange
            foreach (var name in names)
            {
                var user = _userservice.AddUser(name);
            }
            // Act
            var users = _userservice.GetAllUsers();
            // Assert
            users.Count.ShouldBe(names.Length);
            foreach (var name in names)
            {
                users.ShouldContain(u => u.Name == name);
            }
        }

        //[Test]
        //public void AddUser_ShouldThrowException_WhenNameIsNull()
        //{
        //    //Arange
        //    var user = new User
        //    {
        //        Id = 1,
        //        Name = null
        //    };
        //    //Act
        //    _userserviceMock.Setup(x => x.AddUser(null)).Throws<ArgumentException>();
        //    //Assert
        //    Should.Throw<ArgumentException>(() => _userserviceMock.Object.AddUser(null));
        //}
        //[Test]
        //public void AddUser_ShouldThrowException_WhenNameIsEmpty()
        //{
        //    //Arange
        //    var user = new User
        //    {
        //        Id = 5,
        //        Name = ""
        //    };
        //    //Act
        //    _userserviceMock.Setup(x => x.AddUser("")).Throws<ArgumentException>();

        //    //Assert
        //    var test = Should.Throw<ArgumentException>(() => _userserviceMock.Object.AddUser(""));
        //}

        ////Others
        //[Test]
        //public void GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
        //{
        //    //Arange

        //    //Act
        //    _userserviceMock.Setup(x => x.GetUserById(2)).Returns((User)null);
        //    var result = _userserviceMock.Object.GetUserById(2);
        //    //Assert
        //    result.ShouldBeNull();
        //}
        //[Test]
        //public void UpdateUser_ShouldReturnFalse_WhenUserDoesNotExist()
        //{
        //    //Arange
        //    var user = new User
        //    {
        //        Id = 1,
        //        Name = "John"
        //    };
        //    //Act
        //    _userserviceMock.Setup(x => x.UpdateUser(2, "Emmanuel")).Returns(false);
        //    var result = _userserviceMock.Object.UpdateUser(2, "Emmanuel");
        //    //Assert
        //    result.ShouldBeFalse();
        //}
        ////[Test]
        ////public void UpdateUser_ShouldReturnNewUser_WhenUserDoesNotExist()
        ////{
        ////    //Arange
        ////    var user = new User
        ////    {
        ////        Id = 1,
        ////        Name = "John"
        ////    };
        ////    //Act
        ////    _userserviceMock.Setup(x => x.UpdateUser(1, "John")).Returns((User));
        ////    var result = _userserviceMock.Object.UpdateUser(1, "Emmanuel");
        ////    //Assert
        ////    result.ShouldBeTrue();
        ////}

    }
    }