using CodingExerciseUnitTest1;
using Shouldly;
using NUnit.Framework;


namespace UserServiceTest
{
  [TestFixture]
  public class Tests
  {
    private UserService _userService;
    [SetUp]
    public void Setup()
    {
      _userService = new UserService();
    }

    [TestCase("")]
    public void AddUser_CannotBeEmpty(string name)
    {
      // Arrange
      string n = name;

      // Act, Assert
      Should.Throw<ArgumentException>(()=> _userService.AddUser(n));
    }

    [TestCase("martial")]
    public void TAddUser_ReturnUser(string name)//name
    {
      // Arrange
      bool addUserTriggered = false;
      string n = name;
      _userService.UserAdded += (n) => addUserTriggered = true;

      // Act
      _userService.AddUser(n);

      // Assert
      addUserTriggered.ShouldBeTrue("");

      
    }
    [Test]
    public void GetAllUsersReturnEmpty()
    {
      // Arrange, Act, Assert
      _userService.GetAllUsers().ToArray().ShouldBeEmpty();
    }
    [TestCase(1)]
    public void GetUserByIdReturnId(int id)//id
    {
      // Arrange
      _userService.AddUser("claiya");

      // Act
      var result=_userService.GetUserById(id);

      // Assert
      result.ShouldNotBeNull();

    }
    [TestCase(1,"")]
    public void UpdateUserNull_ReturnFalse(int id, string name)//id name
    {
      // Arrange
      _userService.AddUser("chloe");

      // Act
      var res=_userService.UpdateUser(id, name);

      // Assert
      res.ShouldBeFalse();

      
    }
    [TestCase(2,"Sad")]
    public void UpdateUser_ReturnTrue(int id, string name)//id name
    {
      // Arrange
      _userService.AddUser("Natsu");
      _userService.AddUser("Lucy");
      _userService.AddUser("Happy");

      // Act
      var res=_userService.UpdateUser(id,name);

      // Assert
      res.ShouldBeTrue();

    }
    [TestCase(1)]
    public void DeleteUserExisting_ReturnTrue(int id)//id
    {
      // Arrange
      _userService.AddUser("uno");
      _userService.AddUser("dos");
      Console.WriteLine(_userService.GetUserById(id));

      // Act
      var res=_userService.DeleteUser(id);

      // Assert
      res.ShouldBeTrue();

      
    }
    [TestCase(2)]
    public void DeleteUserNonExistent_NoReturn(int id)//id
    {
      // Arrange
      _userService.AddUser("tres");

      // Act
      var res= _userService.DeleteUser(id);

      // Assert
      res.ShouldBeFalse();
    }
  }
}
