using System.Reflection;
using TirsvadCLI.MenuPaginator;

namespace GenSpil;

internal class Program
{
    const string TITLE = "GenSpil";
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

    static void ShowReportBoardGameSort()
    {
        throw new NotImplementedException();
    }

    static void ShowReportBoardGameSortTitle()
    {
        throw new NotImplementedException();
    }

    static void ShowReportBoardGameSortGenre()
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
            menuItems.Add(new MenuItem("Brætspil", (Action)MenuBoardGame));
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
        do
        {
            Console.Clear();
            HeadLine("Rapport");
            List<MenuItem> menuItems = new();
            menuItems.Add(new MenuItem("Lagerstatus sorteret på titel", (Action)ShowReportBoardGameSortTitle));
            menuItems.Add(new MenuItem("Lagerstatus sorteret på gerne", (Action)ShowReportBoardGameSortGenre));
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

    static void MenuAdmin()
    {
        do
        {
            Console.Clear();
            HeadLine("Administrator");
            List<MenuItem> menuItems = new();
            throw new NotImplementedException();
        } while (true);
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


    // maybe tihs is not needed as a menu. Maybe it should be a method in the BoardGame class
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
