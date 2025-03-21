
namespace CodingExerciseUnitTest1;

public class Program
{
    public static void Main(object value)
    {
        throw new NotImplementedException();
    }

    static void Main(string[] args)
    {
        IUserService userService = new UserService();
        
        userService.UserAdded += user => Console.WriteLine($"[Event] User Added: {user.Id} - {user.Name}");
        userService.UserUpdated += user => Console.WriteLine($"[Event] User Updated: {user.Id} - {user.Name}");
        userService.UserDeleted += id => Console.WriteLine($"[Event] User Deleted: ID {id}");
        
        while (true)
        {
            Console.WriteLine("\nUser Management System");
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. View All Users");
            Console.WriteLine("3. Update User");
            Console.WriteLine("4. Delete User");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
    
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter user name: ");
                    var name = Console.ReadLine();
                    var newUser = userService.AddUser(name);
                    Console.WriteLine($"User added: {newUser.Id} - {newUser.Name}");
                    break;

                case "2":
                    var users = userService.GetAllUsers();
                    if (!users.Any()) Console.WriteLine("No users found.");
                    else users.ForEach(u => Console.WriteLine($"{u.Id} - {u.Name}"));
                    break;

                case "3":
                    Console.Write("Enter user ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int updateId))
                    {
                        Console.Write("Enter new name: ");
                        var newName = Console.ReadLine();
                        var updated = userService.UpdateUser(updateId, newName);
                        Console.WriteLine(updated ? "User updated." : "User not found.");
                    }
                    break;

                case "4":
                    Console.Write("Enter user ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteId))
                    {
                        var deleted = userService.DeleteUser(deleteId);
                        Console.WriteLine(deleted ? "User deleted." : "User not found.");
                    }
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
