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
    }

    [JsonConstructor]
    private UserList(List<User> users)
    {
        Users = users ?? [];
    }

    public void Add(User user)
    {
        Users.Add(user);
    }

    public void Remove(User user)
    {
        Users.Remove(user);
    }
} ///> Singleton instance of the UserList
