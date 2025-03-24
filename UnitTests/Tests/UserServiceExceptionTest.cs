using CodingExerciseUnitTest1;
using Shouldly;

namespace UnitTesting.Tests;

public class UserServiceExceptionTest
{
    private IUserService _userService;

    [SetUp]
    public void Setup()
    {
        _userService = new UserService();
    }


    [TestCase("")]
    public void AddUser_ShouldThrowArgumentException_WhenBodyIsNotProvided(string name)
    {
        Should.Throw<ArgumentException>(() =>
        {
            _userService.AddUser(name);
        });
    }
}