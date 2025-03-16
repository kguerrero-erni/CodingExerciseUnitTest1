namespace CodingExerciseUnitTest1
{
    public interface IUserService
    {
        event Action<User> UserAdded;
        event Action<User> UserUpdated;
        event Action<int> UserDeleted;
        User AddUser(string name);
        List<User> GetAllUsers();
        bool UpdateUser(int id, string newName);
        bool DeleteUser(int id);
        User GetUserById(int id);
    }
}