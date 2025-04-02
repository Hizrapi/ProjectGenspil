using System.Reflection;
using GenSpil.Model;
using TirsvadCLI.Frame;
using TirsvadCLI.MenuPaginator;
//using GenSpil.Model;

namespace GenSpil;

internal class Program
{
    const string TITLE = "GenSpil";
    const string DATA_JSON_FILE = "/data/genspil.json";
    static BoardGameList _boardGameList = BoardGameList.Instance;

    //BoardGameList _boardGameList = BoardGameList.Instance;

    static string GetVersion()
    {
        Version? version = Assembly.GetExecutingAssembly().GetName().Version;
        return version != null ? $"{version.Major}.{version.Minor}" : "Unknown version";
    }

    static void Login()
    {
        Console.Clear();
        HeadLine("Log på");
        throw new NotImplementedException();
    }

    static void Logout()
    {
        throw new NotImplementedException();
    }

    static void ShowBoardGame()
    {
        Console.Clear();
        HeadLine("");
        throw new NotImplementedException();
    }

    static void AddBoardGame()
    {
        Console.Clear();
        HeadLine("Tilføj brætspil");
        throw new NotImplementedException();
    }

    static void RemoveBoardGame()
    {
        Console.Clear();
        HeadLine("Fjern brætspil");
        BoardGame item = ChooseBoardGame();
        if (item != null)
        {
            //if (_boardGameList.Remove(item))
            //{
            //        //    Console.WriteLine("Spillet er fjernet fra listen.");
            //        //}
            //        //else
            //        //{
            //        //    Console.WriteLine("Spillet kunne ikke fjernes fra listen.");
            //}
        }
        //else
        //{
        //    Console.WriteLine("Spillet blev ikke fundet.");
        //}
        //throw new NotImplementedException();
    }

    static BoardGame ChooseBoardGame()
    {
        Console.Clear();
        HeadLine("Vælg brætspil");
        throw new NotImplementedException();
    }

    public static List<BoardGame> SearchBoardGame()
    {
        int cInputLeft = 14;
        Console.Clear();
        Console.CursorVisible = true;
        HeadLine("Søg efter brætspil");
        var cTop = Console.GetCursorPosition().Top;
        Console.WriteLine("Titel       : ");
        Console.WriteLine("Genre       : ");
        Console.WriteLine("Variant     : ");
        Console.WriteLine("Tilstand    : ");
        for (int i = 0; i < Enum.GetValues(typeof(Type.Condition)).Length; i++)
        {
            Console.Write($"{i + 1}={(Type.Condition)i} ");
        }
        Console.WriteLine();
        Console.WriteLine("Pris        : ");
        Console.CursorTop = cTop;
        Console.CursorLeft = cInputLeft;
        string title = Console.ReadLine() ?? "";
        Console.CursorLeft = cInputLeft;
        string genre = Console.ReadLine() ?? "";
        Console.CursorLeft = cInputLeft;
        string variant = Console.ReadLine() ?? "";
        Console.CursorLeft = cInputLeft;
        string condition = Console.ReadLine() ?? "";
        Console.CursorLeft = cInputLeft;
        Console.CursorTop++;
        string price = Console.ReadLine() ?? "";
        Console.CursorVisible = false;

        return _boardGameList.SearchBoardGames(); //SearchBoardGames() should take parameters (Tirsvad)
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

    /// <summary>
    /// Display a headline with the title and version of the program.
    /// </summary>
    static void HeadLine(string headLine)
    {
        Console.Clear();
        string title = $" {TITLE} version {GetVersion()} ";

        int l = Math.Max(title.Length, title.Length) + 1;
        Frame frame = new Frame(l, 2);
        frame.SetFrameText(title);
        frame.Render();
        Console.WriteLine();
        Console.WriteLine(CenterString(headLine, l));
        Console.WriteLine(new string('-', l + 1));
        Console.WriteLine();
    }
    /// <summary>
    /// Centers the given text within a specified width.
    /// </summary>
    /// <param name="text">The text to center.</param>
    /// <param name="width">The width within which to center the text.</param>
    /// <returns>The centered text with padding.</returns>
    static string CenterString(string text, int width)
    {
        if (width <= text.Length)
        {
            return text; // Or throw an exception, or truncate the string
        }
        int padding = width - text.Length;
        int leftPadding = padding / 2;
        int rightPadding = padding - leftPadding;
        return new string(' ', leftPadding) + text + new string(' ', rightPadding);
    }

    #region menu
    /// <summary>
    /// Main menu
    /// (Action) is a delegate to a method with no parameters and no return value.
    /// When calling PaginateMenu() the menu is displayed and the user can select an item.
    /// Item with it action is returned.
    /// Then we execute the action.
    /// </summary>
    static void MenuMain()
    {
        do
        {
            Console.Clear();
            HeadLine("Hoved menu");
            // Create a list of menu items
            List<MenuItem> menuItems = new();
            menuItems.Add(new MenuItem("Brætspil", MenuBoardGame));
            menuItems.Add(new MenuItem("Kunde", MenuCostumer));
            menuItems.Add(new MenuItem("Rapporter", MenuReport));
            menuItems.Add(new MenuItem("Admin", MenuAdmin));
            menuItems.Add(new MenuItem("Logout", Logout));
            // Create a menu paginator
            MenuPaginator menu = new(menuItems, 10);
            if (menu.menuItem != null && menu.menuItem.Action is Action action)
                action(); // Execute the action
            else
                return;
        } while (true);
    }

    static void MenuCostumer()
    {
        Console.Clear();
        HeadLine("Kunde menu");
        throw new NotImplementedException();
    }

    /// <summary>
    /// Report menu
    /// (Action) is a delegate to a method with no parameters and no return value.
    /// When calling PaginateMenu() the menu is displayed and the user can select an item.
    /// Item with it action is returned.
    /// Then we execute the action.
    /// </summary>
    static void MenuReport()
    {
        do
        {
            Console.Clear();
            HeadLine("Rapport");
            List<MenuItem> menuItems = new();
            menuItems.Add(new MenuItem("Lagerstatus sorteret på titel", ShowReportBoardGameSortTitle));
            menuItems.Add(new MenuItem("Lagerstatus sorteret på gerne", ShowReportBoardGameSortGenre));
            MenuPaginator menu = new(menuItems, 10);
            if (menu.menuItem != null && menu.menuItem.Action is Action action)
                action();
            else
                return;
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

    /// <summary>
    /// Board game menu
    /// (Action) is a delegate to a method with no parameters and no return value.
    /// When calling PaginateMenu() the menu is displayed and the user can select an item.
    /// Item with it action is returned.
    /// Then we execute the action.
    /// </summary>
    static void MenuBoardGame()
    {
        List<BoardGame> boardGames;
        do
        {
            Console.Clear();
            HeadLine("Brætspil menu");
            List<MenuItem> menuItems = new();
            menuItems.Add(new MenuItem("Vælg spil", (Action)MenuChooseBoardGame));
            menuItems.Add(new MenuItem("Tilføj spil", (Action)AddBoardGame));
            menuItems.Add(new MenuItem("Søg", new Action(() => { boardGames = SearchBoardGame(); })));
            MenuPaginator menu = new(menuItems, 10);
            if (menu.menuItem != null && menu.menuItem.Action is Action action)
                action();
            else
                return;
        } while (true);
    }

    /// <summary>
    /// Choose board game menu
    /// (Action) is a delegate to a method with no parameters and no return value.
    /// When calling PaginateMenu() the menu is displayed and the user can select an item.
    /// Item with it action is returned.
    /// Then we execute the action.
    /// </summary>
    static void MenuChooseBoardGame()
    {
        do
        {
            Console.Clear();
            HeadLine("Vælg spil");
            List<MenuItem> menuItems = new();
            //foreach (BoardGame boardGame in _boardGameList)
            //{
            //    menuItems.Add(new MenuItem(boardGame.Title, () => ShowBoardGame(boardGame)));
            //}
            throw new NotImplementedException();
        } while (true);
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
