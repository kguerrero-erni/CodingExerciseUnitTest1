using NUnit.Framework;
using Shouldly;
using CodingExerciseUnitTest1;
using System.Xml.Linq;
using NUnit.Framework.Constraints;

namespace CodingExercise.Tests
{
    [TestFixture]
    public class UserServiceTest
    {
        // initialize object here
        private IUserService _userService;
        private List<User> _users;

        [SetUp]
        public void Setup()
        {
            _userService = new UserService();
            _users = SampleUserData();

        }
        public List<User> SampleUserData()
        {
            _userService.AddUser("John");
            _userService.AddUser("Jack");
            _userService.AddUser("Jill");

            List<User> users= new List<User>();
            users.Add(new User { Id = 1, Name = "John" });
            users.Add(new User { Id = 2, Name = "Jack" });
            users.Add(new User { Id = 3, Name = "Jill" });
            return users;

        }

        // testname format: function_exepectedresult_info

        public static IEnumerable<TestCaseData> AddTestNames()
        {
            //reference "www.smaclellan.com/posts/parameterized-tests-made-simple/"

            yield return new TestCaseData("John");
            yield return new TestCaseData("Jack");
            yield return new TestCaseData("Jill");
        }

        [Test, TestCaseSource(nameof(AddTestNames))]
        public void AddUser_ShouldCreateNewUser_BasedOnUserInput(string name)
        {
            //reference "www.smaclellan.com/posts/parameterized-tests-made-simple/"
            
            //act
            var user = _userService.AddUser(name);
            
            //assert
            user.Name.ShouldBe(name);
        }

        [Test, TestCase("")]
        public void AddUser_ShouldThrowException_IfNameIsNull(string name)
        {
            // reference "docs.shouldly.org/documentation/exceptions/throw"

            Should.Throw<ArgumentException>(() => _userService.AddUser(name));
        }

        [Test]
        public void GetAllUsers_ShouldReturnAllUsers_IfPopulated()
        {
            var listOfUsers = _userService.GetAllUsers();
            var expectedResult = _users.Count;

            listOfUsers.Count.ShouldBe(expectedResult);

        }

        [Test]
        public void GetUserById_ShouldReturnUserCredentials_IfReferencedById()
        {
            var expectedResult = _users.First();

            var retrievedData = _userService.GetUserById(expectedResult.Id);

            retrievedData.Id.ShouldBe(expectedResult.Id);
            retrievedData.Name.ShouldBe(expectedResult.Name);
            retrievedData.ShouldNotBeNull();
        }

        [Test, TestCase(5)]
        public void GetUserById_ShouldThrowException_IfIdDoesNotExist(int id)
        {
            var user = _userService.GetUserById(id);
            user.ShouldBeNull();
        }

        [Test]
        public void UpdateUser_ShouldChangeTheUsersName_IfReferencedById()
        {
            var user = _userService.GetAllUsers().First();
            var updateUser = _userService.UpdateUser(user.Id, "Jonathan");
            var getUpdatedUser = _userService.GetUserById(user.Id);

            updateUser.ShouldBe(true);
            getUpdatedUser.Name.ShouldNotBe("John");
        }

        [Test]
        public void UpdateUser_ShouldReturnFalse_IfNameIsEmpty()
        {
            var user = _userService.UpdateUser(1, "");
            user.ShouldBeFalse();
        }

        [Test, TestCase(5)]
        public void UpdateUser_ShouldReturnFalse_IfIdDoesNotExist(int id)
        {
            var user = _userService.UpdateUser(id, "Jackie");
            user.ShouldBeFalse();
        }

        [Test, TestCase(5)]
        public void DeleteUser_ShouldBeNull_IfIdDoesNotExist(int id)
        {
            var user = _userService.DeleteUser(id);
           
        }
    }

}
