using CodingExerciseUnitTest1;
using Moq;
using Shouldly;

namespace CodingExerciseUnitTest1Tests
{
    public class Tests
    {
        private IUserService _userService;

        [SetUp]
        public void Setup()
        {
            _userService = new Mock<UserService>().Object;
        }

        [Test]
        [TestCase("Joachim",1,"Joachim")]
        [TestCase("Juan Carlo",1,"Juan Carlo")]
        public void Add_ShouldCreateUser_HasNameJoachimAnd1ID(string name, int expected_userID, string expected_name)
        {
            // Act
            var new_user = _userService.AddUser(name);

            // Assert
            new_user.Id.ShouldBe(expected_userID);
            new_user.Name.ShouldBe(expected_name);
        }

        [Test]
        [TestCase("")]
        public void Add_ShouldThrowException_NullNameArgumentException(string name)
        {
            // Act & Assert
            Should.Throw<ArgumentException>(() => _userService.AddUser(name));
        }

        [Test]
        [TestCase("Joachim", "Elijah",1,2)]
        [TestCase("Jeff","Jaff",1,2)]
        public void Add_ShouldMatchID_TwoUsersCreated(string name, string name2, int expected_id_1, int expected_id_2)
        {
            // Act
            var new_user1 = _userService.AddUser(name);
            var new_user2 = _userService.AddUser(name2);

            // Assert
            new_user1.Id.ShouldBe(expected_id_1);
            new_user2.Id.ShouldBe(expected_id_2);
        }

        [Test]
        [TestCase(new string[] {"Olivia","Liam","Ava","Noah"},4, new string[] { "Olivia", "Liam", "Ava", "Noah" })]
        [TestCase(new string[] {"Sophia","Mason","Isabella","Elijah","Mia","Lucas"},6, new string[] { "Sophia", "Mason", "Isabella", "Elijah", "Mia", "Lucas" })]
        public void GetAll_ShouldMatchLengthAndNames_ArrayWithLength(string[] names, int expected_length, string[] expected_names)
        {
            // Act
            for (int i = 0; i < names.Length; i++)
                _userService.AddUser(names[i]);

            var all_users = _userService.GetAllUsers();

            // Assert
            all_users.Select(x => x.Name).ToArray().ShouldBe(expected_names);
            _userService.GetAllUsers().Count.ShouldBe(expected_length);
        }

        [Test]
        [TestCase(new object[] { })]
        public void GetAll_ShouldReturnNoLength_NoUsersAdded(params string[] names)
        {
            // Act
            for (int i = 0; i < names.Length; i++)
                _userService.AddUser(names[i]);

            // Assert
            _userService.GetAllUsers().Count().ShouldBe(0);
        }

        [Test]
        [TestCase(new string[] { "Joachim","Elijah","Cobar"}
        ,new int[] { 1,3}
        ,new string[] { "Joachim","Cobar"})]
        [TestCase(new string[] { "Alice", "Alicia", "Alyssa", "Ally", "Allison" }, new int[] { 1, 3, 5}, new string[] { "Alice", "Alyssa", "Allison" })]
        public void GetUserById_ShouldMatchData_FetchByIdAndMatchByName(string[] names, int[] id, string[] expected_names)
        {
            // Act
            for (int i = 0; i < names.Length; i++)
                _userService.AddUser(names[i]);

            // Assert
            for (int i = 0; i < id.Length; i++)
                _userService.GetUserById(id[i]).Name.ShouldBe(expected_names[i]);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void GetUserById_ShouldBeNull_NoUserGetFound(int id)
        {
            //Should.Throw<NullReferenceException>(() => _userService.GetUserById(id));
            _userService.GetUserById(id).ShouldBeNull();
        }

        [Test]
        [TestCase("Joachim","TestName",true)]
        [TestCase("Joachim","",false)]
        [TestCase("Juan","Cruz",true)]
        [TestCase("Hello","",false)]
        public void UpdateUser_ShouldBeTrue_UpdatesUserName(string name, string expected_name, bool expected_isUpdated)
        {
            var user = _userService.AddUser(name);

            _userService.UpdateUser(user.Id, expected_name).ShouldBe(expected_isUpdated);
        }

        [Test]
        [TestCase("Joachim", "TestName")]
        [TestCase("Joa", "Chim")]
        public void UpdateUser_ShouldMatchNewName_UpdatesUserName(string name, string expected_name)
        {
            var user = _userService.AddUser(name);

            _userService.UpdateUser(user.Id, expected_name);

            _userService.GetUserById(user.Id).Name.ShouldBe(expected_name);
        }

        [Test]
        [TestCase(new string[] { "Olivia", "Liam", "Ava", "Noah" }, new int[] {1,7,8 }, new bool[] {true,false,false })]
        [TestCase(new string[] { "Olivia", "Liam", "Ava", "Noah" }, new int[] {1,2,3,4 }, new bool[] {true,true,true,true })]
        public void DeleteUser_ShouldDeleteUser_ChecksIfTrueDeletedUser(string[] names, int[] ids, bool[] expected_bool)
        {
            // Act
            for (int i = 0; i < names.Length; i++)
                _userService.AddUser(names[i]);

            // Assert
            for(int i = 0; i < ids.Length; i++)
                _userService.DeleteUser(ids[i]).ShouldBe(expected_bool[i]);
        }

        [Test]
        [TestCase(new string[] { "Olivia", "Liam", "Ava", "Noah" }, new int[] { 1, 3, 2 }, 1)]
        [TestCase(new string[] { "Olivia", "Liam", "Ava", "Noah" }, new int[] { 1}, 3)]
        public void DeleteUser_ShouldMatchLength_ChecksIfLengthChangesAfterDeletion(string[] names, int[] ids, int expected_length)
        {
            // Act
            for (int i = 0; i < names.Length; i++)
                _userService.AddUser(names[i]);

            // Assert
            for (int i = 0; i < ids.Length; i++)
                _userService.DeleteUser(ids[i]);

            _userService.GetAllUsers().Count().ShouldBe(expected_length);
        }
    }
}