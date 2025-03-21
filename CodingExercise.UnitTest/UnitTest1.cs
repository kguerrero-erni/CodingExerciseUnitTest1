using NUnit.Framework;
using CodingExerciseUnitTest1;
using Shouldly;
using Moq;
using Moq.Protected;

namespace CodingExercise.UnitTest
{
    [TestFixture]
    public class UserServiceTest
    {
        //private Mock<IUserService> _userService;
        private IUserService _userService;// = new UserService();

        [SetUp]
        public void SetUp()
        {
            //_userService = new UserService();
            _userService = new Mock<UserService>().Object;
        }


        //source = www.lambdatest.com/blog/nunit-parameterized-test-examples/
        private static IEnumerable<TestCaseData> AddUserTestData()
        {
            yield return new TestCaseData("Mhike", new User { Name = "Mhike" });
            yield return new TestCaseData("MJ", new User { Name = "MJ" });
            yield return new TestCaseData("Cron", new User { Name = "Cron" });
            //yield return new TestCaseData("", new ArgumentException("User name cannot be empty."));
        }


        [Test, TestCaseSource("AddUserTestData")]
        public void AddUser_ShouldReturnUserWithName_UserInput(string name, User expectedUser)
        {
            //_userService.Setup(x => x.AddUser(name)).Returns(new User { Name = name });
            //var user = _userService.Object.AddUser(name);

            //user.Name.ShouldBe(expectedUser.Name);


            var user = _userService.AddUser(name);
            user.Name.ShouldBe(expectedUser.Name);
        }

        [Test]
        [TestCase("")]
        public void AddUser_ShouldReturnArgumentException_UserInput(string name)
        {
            //var user = _userService.AddUser(name);
            //ref: github.com/shouldly/shouldly/issues/554
            Should.Throw<ArgumentException>(() => _userService.AddUser(name)).Message.ShouldBe("User name cannot be empty.");
        }


        public List<User> GenerateUsersData()
        {
            _userService.AddUser("Mhike");
            _userService.AddUser("MJ");
            _userService.AddUser("Cron");

            List<User> users = new List<User>();
            users.Add(new User { Id = 1, Name = "Mhike" });
            users.Add(new User { Id = 2, Name = "MJ" });
            users.Add(new User { Id = 3, Name = "Cron" });
            return users;
        }

        [Test]
        public void GetAllUser_ShouldReturnAllUsers_UserAction()
        {
            var generatedUsers = GenerateUsersData();
            var users = _userService.GetAllUsers();
            users.ShouldBeEquivalentTo(generatedUsers);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetUserById_ShouldReturnUser_UserInput(int id)
        {
            var generatedUsers = GenerateUsersData();
            var user = _userService.GetUserById(id);
            user.Name.ShouldBe(generatedUsers.FirstOrDefault(x => x.Id == id).Name);
        }

        [Test]
        public void GetByUserId_ShouldReturnNull_UserInput()
        {
            var generatedUsers = GenerateUsersData();
            var user = _userService.GetUserById(10);
            user.ShouldBeNull();
        }

        [Test]
        [TestCase(1,"EditedName")]
        [TestCase(2, "MJ No.2")]
        [TestCase(3, "Cron 3")]
        public void UpdateUser_ShouldUpdateUser_UserInput(int Id, String name)
        {
            var generatedUsers = GenerateUsersData();
            var testUser = generatedUsers.FirstOrDefault(x => x.Id == Id);
            testUser.Name = name;

            var result = _userService.UpdateUser(Id, name);
            result.ShouldBeTrue();

            var user = _userService.GetUserById(Id);
            user.Name.ShouldBe(generatedUsers.FirstOrDefault(x => x.Id == Id).Name);
        }

        [Test]
        [TestCase(-1, "SampleName")]
        [TestCase(1, "SampleName")]
        public void UpdateUser_ShouldReturnFalse_UserInput(int Id, String name)
        {
            var result = _userService.UpdateUser(Id, name);
            result.ShouldBeFalse();
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void DeleteUser_ShouldDeleteUser_UserAction(int Id)
        {
            _userService.DeleteUser(Id);
            var users = _userService.GetAllUsers();
            users.ShouldNotContain(x => x.Id == Id);
        }


    }

}
