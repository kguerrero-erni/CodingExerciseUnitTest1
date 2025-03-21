using System.Xml.Linq;
using CodingExerciseUnitTest1;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using Newtonsoft.Json;
using Shouldly;


namespace UnitTest_CodingExercise1

{
    public class Test
    {
        private Mock<UserService> _mockService;
        private IUserService _userService;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<UserService>();
            _userService = _mockService.Object;
           
        }
        [Test]
        [TestCase("")]
        [TestCase(" ")]
        public void AddUser_ShouldReturnExeption_WhenUsernameIsEmpty(String name)
        {
            Should.Throw<ArgumentException>(() => _userService.AddUser(name));
        }

        [Test]
        [TestCase("Test")]
        [TestCase("Rafael")]
        [TestCase("Gomez")]
        public void AddUser_ShouldReturnAddedUser_WhenNameIsValid(String name)
        {
            var _users = _userService.AddUser(name);
            var user_name = _users.Name;

            user_name.ShouldBe(name);

        }

        [Test]
        [TestCase("Gomez")]
        public void GetAllUsers_ShouldReturnUsersList_WhenDataSourceIsNotEmpty(String name)
        {

            _userService.AddUser(name);
            var _users = _userService.GetAllUsers();

            _users.ShouldNotBeEmpty();
        }

        [Test]
        public void GetAllUsers_ShouldReturnNull_WhenDataSourceIsNull()
        {
            var _users = _userService.GetAllUsers();

            _users.ShouldBeEmpty();
        }

        [Test]
        [TestCase(1,"Rafael")]
        public void GetUserById_ShouldReturnUser_WhenUserIdExists(int id,String name)
        {

            _userService.AddUser(name);
            var _users = _userService.GetUserById(id);

            _users.ShouldNotBeNull();
        }

        [Test]
        [TestCase(1)]
        public void GetUserById_ShouldReturnUserNotFound_WhenUserIdDoesNotExist(int id)
        {
            var _users = _userService.GetUserById(id);

            _users.ShouldBeNull();
        }

        [Test]
        [TestCase(1,"asjkbhf","asjfjkasdfkha")]
        public void UpdateUser_ShouldReturnNewUserName_WhenUserIdExist(int userId,string newUserName, string currentUsername)
        {
            _userService.AddUser(currentUsername);
            _userService.UpdateUser(userId, newUserName);
            string updatedUsername = _userService.GetUserById(userId).Name;

            updatedUsername.ShouldBe(newUserName);
        }

        [Test]
        [TestCase(1,"fasdfhklas")]
        public void UpdateUser_ShouldReturnFalse_WhenUserDoesNotExist(int userId,string newUserName)
        {
            var result = _userService.UpdateUser(userId,newUserName);

            result.ShouldBeFalse();
        }

        [Test]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(3)]
        public void DeleteUser_ShouldReturn1LessUser_WhenIdExists(int number_of_data_to_remove)
        {
            int users_toBe_Deleted = number_of_data_to_remove;

            var user_List = new List<string>()
            {
                "Rafael","Gomez","Test","Test2",
            };

            int i = 0;

            for(i = 0; i < user_List.Count; i++) 
            {
                var user = _userService.AddUser(user_List[i]);
            }

            int before_deletion = _userService.GetAllUsers().Count;

            while (users_toBe_Deleted > 0)
            {
                _userService.DeleteUser(users_toBe_Deleted);
                users_toBe_Deleted--;
            }
           
            int after_deletion = _userService.GetAllUsers().Count;
            TestContext.Out.WriteLine(after_deletion);
            after_deletion.ShouldBe(before_deletion- number_of_data_to_remove);
        }

        [Test]
        [TestCase(1)]
        public void DeleteUser_ShouldReturnNull_WhenUserIdDoesNotExist(int id)
        {
            var result = _userService.DeleteUser(id);

            result.ShouldBeFalse();
        }

    }
}
