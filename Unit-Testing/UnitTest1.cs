using CodingExerciseUnitTest1;
using Moq;
using Shouldly;

namespace Unit_Testing
{
    public class Tests
    {
        private Mock<IUserService> _mockUserService;
        private IUserService userServiceMock;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            userServiceMock = _mockUserService.Object;
        }

        [Test]
        [TestCase("")]
        public void AddUser_AddUserCannotBeEmpty_BasedOnUserInput(String name)
        {
            Should.Throw<ArgumentException>(() => userServiceMock.AddUser(name));
        }
    }
}
