using CodingExerciseUnitTest1;
using Moq;
using Shouldly;

namespace TestProject_UnitTesting_Activity
{
    [TestFixture]
    public class UserService_Tests
    {

        //public void AddUser_ShouldAddUser()
        //{
        //    //Arrange
        //   var userService = new UserService();
        //   var sourceList = new List<User>();

        //    var mockUserService = new Mock<IUserService>();
        //    mockUserService.Setup(x => x.AddUser

        //    //Act
        //    var userActual = userService.AddUser(name)

        //}
        [Test]
        public void GetUserById_ShouldGetUserById()
        {
            //Arrange
            var id = 1;
            var name = "test";
            var userService = new UserService();
            var users = new User { Id = id, Name = name };
            
            //Act
            var user = userService.GetUserById(id);
            
            //Assert
            users.ShouldBe(user);

        }
        
        
     
       




    }
}
