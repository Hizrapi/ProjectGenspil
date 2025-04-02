using System.Reflection;
using GenSpil.Model;
using TirsvadCLI.Frame;
using TirsvadCLI.MenuPaginator;
//using GenSpil.Model;

namespace GenSpil;

internal class Program
{
    const string TITLE = "GenSpil";
    static readonly string DATA_JSON_FILE = "/data/genspil.json";
    static BoardGameList _boardGameList = BoardGameList.Instance;

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

    static BoardGame ChooseBoardGame()
    {
        throw new NotImplementedException();
    }

    static List<BoardGame> SearchBoardGame()
    {
        int cTop;
        int cInputLeft = 14;
        int i;
        string? title;
        string? genre;
        string? variant;
        string? condition;
        string? price;

        Console.CursorVisible = true;

        HeadLine("Søg efter brætspil");
        cTop = Console.CursorTop;
        Console.Write("Title");
        Console.CursorLeft = cInputLeft - 2;
        Console.WriteLine(":");
        Console.Write("Genre");
        Console.CursorLeft = cInputLeft - 2;
        Console.WriteLine(":");
        Console.Write("Variant");
        Console.CursorLeft = cInputLeft - 2;
        Console.WriteLine(":");
        Console.Write("Condition");
        Console.CursorLeft = cInputLeft - 2;
        Console.WriteLine(":");
        for (i = 0; i < Enum.GetValues<Type.Condition>().Length; i++)
        {
            Console.Write((int)Enum.GetValues(typeof(Type.Condition)).GetValue(i));
            Console.Write(" - ");
            Console.WriteLine(Enum.GetName(typeof(Type.Condition), i));
        }
        Console.Write("Pris");
        Console.CursorLeft = cInputLeft - 2;
        Console.WriteLine(":");

        Console.SetCursorPosition(cInputLeft, cTop++);
        title = Console.ReadLine();
        Console.SetCursorPosition(cInputLeft, cTop++);
        genre = Console.ReadLine();
        Console.SetCursorPosition(cInputLeft, cTop++);
        variant = Console.ReadLine();
        Console.SetCursorPosition(cInputLeft, cTop++);
        condition = Console.ReadLine();
        Console.SetCursorPosition(cInputLeft, cTop + i);
        price = Console.ReadLine();

        Console.CursorVisible = false;

        return _boardGameList.SearchBoardGames(); // should be with parameters (Tirsvad)
        //return _boardGameList.SearchBoardGames(title, genre, variant, condition, price);
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
            menuItems.Add(new MenuItem("Vælg spil", MenuChooseBoardGame));
            menuItems.Add(new MenuItem("Tilføj spil", AddBoardGame));
            menuItems.Add(new MenuItem("Søg", new Action(() => boardGames = SearchBoardGame())));
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
