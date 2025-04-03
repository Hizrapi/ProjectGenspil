using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenSpil.Model;


/// <summary>
/// Singleton class for handling a list of users.
/// </summary>
public class UserList
{
    private static UserList? _instance;
    private static readonly object padlock = new object();

    private UserList()
    {
        // Eventuel initialisering af UserList
    }
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

} ///> Singleton instance of the UserList
