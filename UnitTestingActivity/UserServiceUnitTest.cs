using CodingExerciseUnitTest1;
using Shouldly;

namespace UnitTestingActivity
{
    public class Tests
    {
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userService = new UserService();
            _userService.AddUser("Bob");
            _userService.AddUser("May");
            _userService.AddUser("Mar");
            _userService.AddUser("Nic");
            _userService.AddUser("Ric");

        }


        public List<User> UsersDataMock()
        {
            return new List<User>
            {
                new User { Id = 1, Name = "Bob" },
                new User { Id = 2, Name = "May" },
                new User { Id = 3, Name = "Mar" },
                new User { Id = 4, Name = "Nic" },
                new User { Id = 5, Name = "Ric" }
            };

           
        }
        [TestCase("Mary")]
        public void AddUser_ShouldAddUserToList_WhenUserDoesNotExists(string name)
        {
           

            //Act
            var result = _userService.AddUser(name).Name;

            //Assert
            result.ShouldBe(name);

        }

        [TestCase("May")]
        public void AddUser_ShouldShowAlreadyAddedUser_WhenUserDoesExists(string name)
        {

            //Act
            var result = _userService.AddUser(name).Name;

            //Assert
            result.ShouldBe(name);

        }

        [TestCase(" ")]
        public void AddUser_ShouldShowAlreadyAddedUser_WhenUserInputsEmpty(string name)
        {

            //Act & Assert
            Should.Throw<ArgumentException>(() => _userService.AddUser(name));

        }


        [TestCase(1)]
        public void GetUser_ShouldGetUserById_WhenUserExists(int id)
        {

            //Act
            var result = _userService.GetUserById(id);

            //assert
            result.Id.ShouldBe(id);
        }


        [TestCase(1,"Bob")]
        public void UpdateUser_ShouldReturnTrueVerifyUpdate_WhenUserExists(int id,string name)
        {
           
            //Act
            var result = _userService.UpdateUser(id, name);

            //Assert
            result.ShouldBeTrue();

        }

        [TestCase(7, "Bleh")]
        public void UpdateUser_ShouldReturnTrueVerifyUpdate_WhenUserDoesNotExists(int id, string name)
        {

            //Act
            var result = _userService.UpdateUser(id, name);

            //Assert
            result.ShouldBeFalse();

        }

        [Test]
        public void GetAllUser_ShouldGetAllUsers_WhenListNotEmpty()
        {
            //arrange
            var users = UsersDataMock();

            //act
            var result = _userService.GetAllUsers();
            int countOfUsers = users.Count();
            int countOfResult = result.Count();

            //assert
            countOfResult.ShouldBeEquivalentTo(countOfUsers);
            
        }

        [TestCase(1)]
        public void DeleteUser_ShouldDeleteUser_WhenUserExists(int id)
        {

            //act
            var result = _userService.DeleteUser(id);

            //assert
            result.ShouldBeTrue();
        }
    }
}
