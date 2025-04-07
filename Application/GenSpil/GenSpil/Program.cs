using System.Reflection;
using System.Text;
using GenSpil.Handler;
using GenSpil.Model;
using TirsvadCLI.Frame;
using TirsvadCLI.MenuPaginator;
//using GenSpil.Model;

namespace GenSpil;

internal class Program
{

    const string TITLE = "GenSpil";
    static readonly string DATA_JSON_FILE = "./data/genspil.json";

    static Authentication _auth;

    //BoardGameList _boardGameList = BoardGameList.Instance;

    static Program()
    {
        _auth = new Authentication();

    }

    static string GetVersion()
    {
        Version? version = Assembly.GetExecutingAssembly().GetName().Version;
        return version != null ? $"{version.Major}.{version.Minor}" : "Unknown version";
    }

    /// <summary>
    /// Login with username and password
    /// </summary>
    static void Login()
    {
        int cTop;
        int cInputLeft = 14;
        do
        {
            Console.CursorVisible = true;
            // Headline
            HeadLine("Log på");
            // Form 
            cTop = Console.CursorTop;
            Console.Write("Brugernavn");
            Console.CursorLeft = cInputLeft - 2;
            Console.WriteLine(":");
            Console.Write("Adgangskode");
            Console.CursorLeft = cInputLeft - 2;
            Console.WriteLine(":");
            // user input 
            Console.SetCursorPosition(cInputLeft, cTop++);
            string? username = ReadLineWithEscape();
            Console.SetCursorPosition(cInputLeft, cTop++);
            //TODO hide password input
            string? password = ReadLineWithEscape(true);
            Console.CursorVisible = false;
            // Authenticate
            if (username == null || password == null)
            {
                ErrorMessage("Brugernavn eller adgangskode er tom");
                continue;
            }

            if (_auth.Login(username, password))
            {
                Console.WriteLine($"Du er logget ind som {username}");
                var role = _auth.GetRole(username);
                Console.WriteLine($"Din rolle er {role}");
                break;
            }
            else
            {
                ErrorMessage("Forkert brugernavn eller adgangskode");

            }

        } while (true);


    }
    BoardGameList boardgamelist = new BoardGameList();
    static void Logout()
    {
        _auth.Logout();
        Login();
    }

    static void ShowBoardGame()
    {

        BoardGameList.Instance.DisplayBoardGames();
    }

    static void AddBoardGame()
    {
        BoardGameList.Instance.Add();
    }

    static void RemoveBoardGame()
    {
        BoardGameList.Instance.Remove();
    }

    static void SeekBoardGame()
    {
        BoardGameList.Instance.Search();
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

    static void ErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Console.WriteLine(message);
        Console.ResetColor();
        Console.WriteLine("Tryk en tast for at fortsætte");
        Console.ReadKey();
    }

    /// <summary>
    /// Reads a line of input from the console, with optional hiding of input characters.
    /// </summary>
    /// <param name="hideInput">Whether to hide the input characters (e.g., for passwords).</param>
    /// <returns>The input string, or null if the escape key was pressed.</returns>
    static string? ReadLineWithEscape(bool hideInput = false)
    {
        StringBuilder input = new StringBuilder();
        ConsoleKeyInfo keyInfo;
        while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
        {
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                return null; // Return null if Esc is pressed
            }
            if (keyInfo.Key == ConsoleKey.Tab)
            {
                continue; // Ignore Tab key
            }
            if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input.Remove(input.Length - 1, 1);
                Console.Write("\b \b");
            }
            else if (keyInfo.Key != ConsoleKey.Backspace)
            {
                input.Append(keyInfo.KeyChar);
                if (hideInput)
                {
                    Console.Write('*');
                }
                else
                {
                    Console.Write(keyInfo.KeyChar);
                }
            }
        }
        Console.WriteLine();
        return input.ToString();
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
            menuItems.Add(new MenuItem("Kunde", MenuCustomer));
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

    static void RemoveCustomer()
    {
        CustomerList.Instance.RemoveCustomer();
    }

    static void SearchCustomers()
    {
        CustomerList.Instance.SearchCustomers();
    }

    static void AddCustomer()
    {
        CustomerList.Instance.AddCustomer();
    }

    static void MenuCustomer()
    {
        do
        {
            Console.Clear();
            HeadLine("Kunder");

            // Create a list of menu items
            List<MenuItem> menuItems = new();
            menuItems.Add(new MenuItem("Tilføje kunde", AddCustomer));
            menuItems.Add(new MenuItem("Fjern kunde", RemoveCustomer));
            menuItems.Add(new MenuItem("Søge i kunder", SearchCustomers));
            menuItems.Add(new MenuItem("Logout", Logout));

            // Create a menu paginator
            MenuPaginator menu = new(menuItems, 10);
            if (menu.menuItem != null && menu.menuItem.Action is Action action)
                action(); // Execute the action
            else
                return;
        } while (true);
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
        do
        {
            Console.Clear();
            HeadLine("Brætspil menu");
            List<MenuItem> menuItems = new();
            menuItems.Add(new MenuItem("Vælg spil", MenuChooseBoardGame));
            menuItems.Add(new MenuItem("Tilføj spil", AddBoardGame));
            menuItems.Add(new MenuItem("List spil", ShowBoardGame));
            menuItems.Add(new MenuItem("Fjern spil", RemoveBoardGame));
            menuItems.Add(new MenuItem("Tilføj reservation", MenuAddReservation));
            menuItems.Add(new MenuItem("Fjern reservation", MenuRemoveReservation));
            menuItems.Add(new MenuItem("Vis reservationer", MenuShowReservations));
            menuItems.Add(new MenuItem("Søg", SeekBoardGame));
            MenuPaginator menu = new(menuItems, pageSize: 10);
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
        BoardGameList.Instance.Edit();
        //do
        //{
        //    Console.Clear();
        //    HeadLine("Vælg spil");
        //    List<MenuItem> menuItems = new();
        //    foreach (BoardGame boardGame in _boardGameList.BoardGames)
        //    {
        //        menuItems.Add(new MenuItem(boardGame.Title, () => ShowBoardGame(boardGame)));
        //    }
        //    MenuPaginator menu = new(menuItems, 10);
        //    if (menu.menuItem != null && menu.menuItem.Action is Action action)
        //        action();
        //    else
        //        return;
        //} while (true);

    }

    // maybe tihs is not needed as a menu. Maybe it should be a method in the BoardGame class
    static void MenuChooseBoardGameVariant()
    {
        throw new NotImplementedException();
    }

    static void MenuAddReservation()
    {
        HeadLine(CenterString("Tilføj reservation", 40));   
        AddReservation();
    }

    static void AddReservation()
    {
        Console.Clear();
       

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Søg efter spil: ");
        Console.ResetColor();
        string? search = Console.ReadLine();

        var foundGames = BoardGameList.Instance
            .GetAllBoardGames()
            .Where(g => g.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!foundGames.Any())
        {
            Console.WriteLine("Ingen spil fundet.");
            Console.ReadLine();
            return;
        }

        for (int i = 0; i < foundGames.Count; i++)
        {
            var game = foundGames[i];
            Console.WriteLine($"{i + 1}. {game.Title} ({game.Variant.Variant}) - Genre: {game.Genre}, Tilstand: {game.Condition}");
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Vælg spil: ");
        Console.ResetColor();
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > foundGames.Count)
        {
            Console.WriteLine("Ugyldigt valg.");
            Console.ReadLine();
            return;
        }

        var selectedGame = foundGames[index - 1];

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Kunde-ID: ");
        Console.ResetColor();
        int customerID = Convert.ToInt32(Console.ReadLine());

        //Sætter dato til tidspunkt for indtasdtning
        DateTime reservedDate = DateTime.Now;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Antal: ");
        Console.ResetColor();
        int quantity = Convert.ToInt32(Console.ReadLine());

        BoardGameList.Instance.RegisterReservation(selectedGame, customerID, reservedDate, quantity);

        Console.WriteLine("Reservation tilføjet.");
        Console.ReadLine();
    }

    static void MenuRemoveReservation()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.White;
        HeadLine("Fjern reservationer");

        Console.WriteLine("\nIndtast en del af titlen eller tryk Enter for at se alle reservationer:");
        Console.ResetColor();

        string searchTerm = Console.ReadLine().ToLower();

        var games = BoardGameList.Instance.GetAllBoardGames();
        var reservations = games
            .Where(game => string.IsNullOrEmpty(searchTerm) || game.Title.ToLower().Contains(searchTerm))
            .SelectMany(game => game.Variant.GetReservations(), (game, reservation) => new { Game = game, Reservation = reservation })
            .ToList();

        if (reservations.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nIngen reservationer fundet.");
            Console.ResetColor();
            return;
        }

        Console.WriteLine(); // mellemrum før liste
        for (int i = 0; i < reservations.Count; i++)
        {
            var res = reservations[i];
            string variantPart = string.IsNullOrEmpty(res.Game.Variant.Variant) ? "" : $" ({res.Game.Variant.Variant})";

            // Nummer i GRØN
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{i + 1}. ");

            // Titel og variant i HVID
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{res.Game.Title}{variantPart}");

            // Detaljer i MAGENTA
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"   - Antal: {res.Reservation.Quantity}, Dato: {res.Reservation.ReservedDate.ToShortDateString()}, Kunde-ID: {res.Reservation.CustomerID}");

            Console.ResetColor();
        }

        // Nyt linjeskift + prompt i hvid
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Indtast nummeret på den reservation, du vil fjerne:");
        Console.ResetColor();

        if (!int.TryParse(Console.ReadLine(), out int reservationIndex) || reservationIndex < 1 || reservationIndex > reservations.Count)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ugyldigt reservationsnummer.");
            Console.ResetColor();
            return;
        }

        var reservationToRemove = reservations[reservationIndex - 1].Reservation;
        reservations[reservationIndex - 1].Game.Variant.RemoveReservation(reservationToRemove);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Reservationen er fjernet.");
        Console.ResetColor();
    }


    static void MenuShowReservations()
    {
        HeadLine(CenterString("Reservationer", 20));
        ShowReservations();
    }

    public static void ShowReservations()
    {
        

        var allGames = BoardGameList.Instance.GetAllBoardGames();

        if (!allGames.Any())
        {
            Console.WriteLine("Der er ingen reservationer i listen endnu.");
            Console.ReadLine();
            return;
        }

        var reservationsGrouped = allGames
            .Where(game => game.Variant.GetReservations().Any())
            .OrderBy(game => game.Title)
            .ThenBy(game => game.Variant.Variant);

        foreach (var game in reservationsGrouped)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nTitel: {game.Title}");
            Console.ResetColor();

            var reservations = game.Variant.GetReservations().OrderBy(r => r.ReservedDate);

            foreach (var reservation in reservations)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" - Variant: ");
                Console.ResetColor();
                Console.WriteLine(game.Variant.Variant);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("   Dato: ");
                Console.ResetColor();
                Console.WriteLine(reservation.ReservedDate.ToString("dd/MM/yyyy"));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("   Kunde-ID: ");
                Console.ResetColor();
                Console.WriteLine(reservation.CustomerID);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("   Antal: ");
                Console.ResetColor();
                Console.WriteLine(reservation.Quantity);
            }
        }

            Console.WriteLine();
            Console.ReadLine();

    }



        #endregion menu

        /// <summary>
        /// Main method of the program.
        /// Loads data from a JSON file, displays the main menu, and exports data back to the JSON file.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
    {
        JsonFileHandler.Instance.ImportData(DATA_JSON_FILE);
        Login();
        MenuMain();
        JsonFileHandler.Instance.ExportData(DATA_JSON_FILE);
    }
}