using TirsvadCLI.MenuPaginator;

namespace GenSpil;

internal class Program
{
    static readonly string TITLE = "GenSpil";
    static readonly string DATA_JSON_FILE = "/data/genspil.json";

    static void Login()
    {
        throw new NotImplementedException();
    }

    static void Logout()
    {
        throw new NotImplementedException();
    }


    static void showBoardGame()
    {
    }

    static void SeekBoardGame()
    {
    }

    static void HeadLine(string headLine)
    {
        Console.Clear();
        Console.WriteLine(TITLE);
        Console.WriteLine("=======");
        Console.WriteLine(headLine);
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
        throw new NotImplementedException();
    }

    static void MenuChooseBoardGame()
    {
    }

    static void MenuChooseBoardGameVariant()
    {
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
