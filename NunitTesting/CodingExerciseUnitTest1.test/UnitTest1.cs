
using Shouldly;

namespace CodingExerciseUnitTest1.test;


public class Tests
{
    private UserService _userService;

    [SetUp]
    public void Setup()
    {
        _userService = new UserService();
    }

    [Test]
    [TestCase("Adrix")]
    
    public void addUser_shouldAddUser_IsBasedOnUserInput(string name)
    {
        var result = _userService.AddUser(name);

        result.Name.ShouldBe(name);

    }

    [Test]
    [TestCase("")]
    
    public void addUser_shouldReturnException_IsBasedOnUserInput(string name)
    {
        var result = Assert.Throws<ArgumentException>(() => _userService.AddUser(name));
        result.Message.ShouldBe("User name cannot be empty.");

    }

    [Test]
    public void GetAllUsers_shouldTestIfTheListIsNotEmpty_ForCheckingIfUserIsAvailableInTheList()
    {
        // var users = new List<User>
        // {
        //     new User{Id= 1, Name= "Bruh"},
        //     new User{Id = 2 , Name = "Bruh2s"}
        // };

        // var result = _userService.GetAllUsers();
        // result.ShouldBeOfType(typeof(List<User>));

        // for(int i = 0 ; i < result.Count; i++){
        //     var user = result[i];
        //     user.Id.ShouldBe(users[i].Id);
        //     user.Name.ShouldBe(users[i].Name);

         var result = _userService.GetAllUsers();
        var users = new List<User>
        {
            new User{Id= 1, Name= "Bruh"},
            new User{Id = 2 , Name = "Bruh2s"}
        };
        var userCount = result.Count;
        foreach(var user in users){
            _userService.AddUser(user.Name);
        }
        var newUserCount = _userService.GetAllUsers().Count();

        newUserCount.ShouldBeGreaterThan(userCount);
    }

    [Test]
    public void GetAllUsers_CheckIfTheListIsEmpty_ForGettingUsers()
    {
          var result = _userService.GetAllUsers();
          result.ShouldBeEmpty();
    }

       [Test]
       [TestCase(1, "Br")]
       [TestCase(2,"gr")]
    public void GetUserId_shouldGetUserId_ForGettingUserId(int Id, string Name)
    {

        _userService.AddUser("Br");
        _userService.AddUser("gr");

        var result = _userService.GetUserById(Id);

        result.Id.ShouldBe(result.Id);
        result.Name.ShouldBe(result.Name);
    }

    [Test]
    [TestCase(1,"Jerrey","Jerry")]
    public void Update_ShouldGetUserIdBeforeUpdating_ForUpdating(int id, string current_name, string new_name){
        _userService.AddUser(current_name); 
        var result = _userService.UpdateUser(id,new_name);
        result.ShouldBe(result);
    }

    [Test]
    [TestCase(2,"Jerrey","Jerry")]
    public void Update_ShouldReturnFalseIfIdIsNotAvaialble_ForCheckingNullValueInUpdate(int id, string current_name, string new_name){
         _userService.AddUser(current_name);
        var result = _userService.UpdateUser(id,new_name);
        result.ShouldBeFalse();
    }


    [Test]
    [TestCase(1,"Br")]
        public void Delete_shouldGetUserId_ForDeleting(int id, string name){
        
      
        _userService.AddUser("Aye");

        var result = _userService.DeleteUser(id);

        result.ShouldBeTrue();
    }

    
    [Test]
    [TestCase(1)]

        public void Delete_shouldreturnFalse_ForDeleting(int id){
        

        var result = _userService.DeleteUser(id);

        result.ShouldBeFalse();

    }

        
   
    

}
