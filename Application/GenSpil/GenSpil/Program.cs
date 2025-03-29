using System.Reflection;
using TirsvadCLI.MenuPaginator;

namespace GenSpil;

internal class Program
{
    static readonly string TITLE = "GenSpil";
    static readonly string DATA_JSON_FILE = "/data/genspil.json";

    static string GetVersion()
    {
        Version? version = Assembly.GetExecutingAssembly().GetName().Version;
        return version != null ? $"{version.Major}.{version.Minor}" : "Unknown version";
    }

    static void Login()
    {
        throw new NotImplementedException();
    }

    static void Logout()
    {
        throw new NotImplementedException();
    }


    static void ShowBoardGame()
    {
        throw new NotImplementedException();
    }

    static void AddBoardGame()
    {
        throw new NotImplementedException();
    }

    static void RemoveBoardGame()
    {
        throw new NotImplementedException();
    }

    static void SeekBoardGame()
    {
        throw new NotImplementedException();
    }

    static void HeadLine(string headLine)
    {
        Console.Clear();
        string title = TITLE + " version " + GetVersion();
        Console.WriteLine(title);
        Console.WriteLine(new string('=', title.Length));
        Console.WriteLine(headLine);
        Console.WriteLine(new string('-', headLine.Length));
        Console.WriteLine();
    }


    #region menu
    static void MenuMain()
    {
        do
        {
            Console.Clear();
            HeadLine("Hoved menu");
            List<MenuItem> menuItems = new();
            menuItems.Add(new MenuItem("Board game", (Action)MenuBoardGame));
            menuItems.Add(new MenuItem("Kunde", (Action)MenuCostumer));
            menuItems.Add(new MenuItem("Rapporter", (Action)MenuReport));
            menuItems.Add(new MenuItem("Admin", (Action)MenuAdmin));
            menuItems.Add(new MenuItem("Logout", (Action)Logout));
            MenuPaginator menu = new(menuItems, 10);
            if (menu.menuItem != null && menu.menuItem.Action is Action action)
            {
                action();
            }
            else
            {
                return;
            }
        } while (true);
    }

    static void MenuCostumer()
    {
        throw new NotImplementedException();
    }

    static void MenuReport()
    {
        throw new NotImplementedException();
    }

    static void MenuAdmin()
    {
        throw new NotImplementedException();
    }

    static void MenuBoardGame()
    {
        do
        {
            Console.Clear();
            HeadLine("Brætspil menu");
            List<MenuItem> menuItems = new();
            menuItems.Add(new MenuItem("Vælg spil", (Action)MenuChooseBoardGame));
            menuItems.Add(new MenuItem("Tilføj spil", (Action)AddBoardGame));
            menuItems.Add(new MenuItem("Søg", (Action)SeekBoardGame));
            MenuPaginator menu = new(menuItems, 10);
            if (menu.menuItem != null && menu.menuItem.Action is Action action)
            {
                action();
            }
            else
            {
                return;
            }
        } while (true);
    }

    static void MenuChooseBoardGame()
    {
        throw new NotImplementedException();
    }

    static void MenuChooseBoardGameVariant()
    {
        throw new NotImplementedException();
    }
    #endregion menu

    static void Main(string[] args)
    {
        do
        {
            //Login();
            MenuMain();
        } while (true);
    }
}
