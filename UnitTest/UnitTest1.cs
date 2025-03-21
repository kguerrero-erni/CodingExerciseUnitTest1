using CodingExerciseUnitTest1;
using Moq;
using Shouldly;

namespace UnitTest
{
    public class Tests
    {
        private Mock<UserService> _mockUserService;
        private UserService userService;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<UserService>();
            userService = _mockUserService.Object;

        }

        [TestCase("Henry")]
        public void AddUser_ReturnUserValues_WhenUserInputIsValid(string name)
        {
            User result = userService.AddUser(name);
            bool userid = Convert.ToBoolean(result.Id);

            result.Name.ShouldBe(name);
            userid.ShouldBeTrue();
        }

        [TestCase("")]
        [TestCase(null)]
        public void AddUser_ShouldThrowException_WhenUserInputIsEmpty(string name)
        {
            Should.Throw<ArgumentException>(() => userService.AddUser(name));
        }

        [TestCase("Henry")]
        public void GetAllUser_ShouldNotBeEmpty_WhenUserisAdded(string name)
        {
            userService.AddUser(name);
            List<User> users = userService.GetAllUsers();

            users.ShouldNotBeEmpty();
        }

        [Test]
        public void GetAllUser_ShouldBeEmpty_WhenNoUserAdded()
        {
            List<User> users = userService.GetAllUsers();

            users.ShouldBeEmpty();
        }

        [Test]
        public void GetAllUsers_ShouldReturnList_WhenUserisFetched()
        {
            List<User> users = userService.GetAllUsers();
            users.ShouldBeOfType<List<User>>();
        }

        [TestCase(1, "Henry")]
        public void GetUserById_ShouldReturnUserById(int id, string name)
        {
            userService.AddUser(name);
            User user = userService.GetUserById(id);

            user.Name.ShouldBe(name);
        }

        [TestCase(1, "Henry", "Alden")]
        public void UpdateUser_ShouldReturnNewName_InputNewValue(int id, string oldName, string newName)
        {
            userService.AddUser(oldName);
            userService.UpdateUser(id, newName);
            User user = userService.GetUserById(id);

            user.Name.ShouldBe(newName);
        }

        [TestCase(10, "Henry", "Alden")]
        public void UpdateUser_ShouldReturnFalse_WhenUserIdisInvalid(int id, string oldName, string newName)
        {
            userService.AddUser(oldName);
            bool result = userService.UpdateUser(id, newName);

            result.ShouldBeFalse();
        }

        [TestCase(1, "Henry")]
        public void DeleteUser_ShouldReturnTrue_WhenDeletedAnExistingUser(int id, string name)
        {
            userService.AddUser(name);
            bool result = userService.DeleteUser(id);

            result.ShouldBeTrue();
        }
    }
}
