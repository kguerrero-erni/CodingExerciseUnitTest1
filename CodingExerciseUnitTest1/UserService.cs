namespace CodingExerciseUnitTest1
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>();
        private int _nextId = 1; // Auto-incrementing ID simulation
    
        public event Action<User> UserAdded;
        public event Action<User> UserUpdated;
        public event Action<int> UserDeleted;
    
        public User AddUser(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("User name cannot be empty.");

            var user = new User { Id = _nextId++, Name = name };
            _users.Add(user);
        
            UserAdded?.Invoke(user);
            return user;
        }
    
        public List<User> GetAllUsers() => _users;
    
        public User GetUserById(int id) => _users.FirstOrDefault(u => u.Id == id);
    
        public bool UpdateUser(int id, string newName)
        {
            var user = GetUserById(id);
            if (user == null || string.IsNullOrWhiteSpace(newName))
                return false;

            user.Name = newName;
        
            UserUpdated?.Invoke(user);
            return true;
        }
    
        public bool DeleteUser(int id)
        {
            var removed = _users.RemoveAll(u => u.Id == id) > 0;
            if (removed)
                UserDeleted?.Invoke(id);

            return removed;
        }

        public object Setup(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}