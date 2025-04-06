using System.Text.Json.Serialization;

namespace GenSpil.Model;


/// <summary>
/// Singleton class for handling a list of users.
/// </summary>
public class UserList
{
    private static UserList? _instance;
    private static readonly object padlock = new object();
    public static UserList Instance
    {
        get
        {
            lock (padlock)
            {
                if (_instance == null)
                {
                    _instance = new UserList();
                }
                return _instance;
            }
        }

    }

    public List<User> Users { get; private set; } // List of users 

    private UserList()
    {
        Users = new List<User>();
#if DEBUG
        Seed();
#endif
    }

    [JsonConstructor]
    private UserList(List<User> users)
    {
        Users = users ?? new List<User>();
    }

    public void Add(User user)
    {
        Users.Add(user);
    }

    public void Remove(User user)
    {
        Users.Remove(user);
    }

# if DEBUG
    public void Seed()
    {
        Users.Add(new User("admin", "admin", Type.Role.Admin));
        Users.Add(new User("user", "user", Type.Role.User));
    }
#endif
} ///> Singleton instance of the UserList
