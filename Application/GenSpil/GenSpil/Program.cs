using System.Reflection;
using GenSpil.Handler;
using GenSpil.Model;
using GenSpil.UI;
using TirsvadCLI.Frame;
using TirsvadCLI.MenuPaginator;

namespace GenSpil;

internal class Program
{
    const string TITLE = "GenSpil";
    static readonly string DATA_JSON_FILE = "data/genspil.json"; // fjernet skråstreg først ("/data/...")

    static Authentication _auth;

    static Program()
    {
        _auth = new Authentication();
    }

    static string GetVersion()
    {
        Version? version = Assembly.GetExecutingAssembly().GetName().Version;
        return version != null ? $"{version.Major}.{version.Minor}" : "Unknown version";
    }

    static void Login()
    {
        int cTop;
        int cInputLeft = 14;
        do
        {
            Console.CursorVisible = true;
            HeadLine("Log på");

            cTop = Console.CursorTop;
            Console.Write("Brugernavn");
            Console.CursorLeft = cInputLeft - 2;
            Console.WriteLine(":");
            Console.Write("Adgangskode");
            Console.CursorLeft = cInputLeft - 2;
            Console.WriteLine(":");

            Console.SetCursorPosition(cInputLeft, cTop++);
            string? username = Console.ReadLine();
            Console.SetCursorPosition(cInputLeft, cTop++);
            string? password = Console.ReadLine();
            Console.CursorVisible = false;

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

    static void Logout()
    {
        _auth.Logout();
        Login();
    }

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

    static string CenterString(string text, int width)
    {
        if (width <= text.Length)
            return text;

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
       

    static void MenuMain()
    {
        do
        {
            Console.Clear();
            HeadLine("Hoved menu");

            List<MenuItem> menuItems = new()
            {
                new MenuItem("Brætspil", MenuBoardGame),
                new MenuItem("Kunde", MenuCustomer),
                new MenuItem("Rapporter", MenuReport),
                new MenuItem("Admin", MenuAdmin),
                new MenuItem("Logout", Logout)
            };

            MenuPaginator menu = new(menuItems, 10);
            if (menu.menuItem != null && menu.menuItem.Action is Action action)
                action();
            else
                return;

        } while (true);
    }

    static void MenuBoardGame()
    {
        do
        {
            Console.Clear();
            HeadLine("Brætspil menu");
            List<MenuItem> menuItems = new()
        {
            new MenuItem("Vælg spil", BoardGameUI.EditGameUI),
            new MenuItem("Tilføj spil", BoardGameUI.AddGameUI),         // [NEW]
            new MenuItem("List spil", BoardGameUI.DisplayGamesUI),      // [NEW]
            new MenuItem("Fjern spil", BoardGameUI.RemoveGameUI),       // [NEW]
            new MenuItem("Tilføj reservation", MenuAddReservation),
            new MenuItem("Fjern reservation", MenuRemoveReservation),
            new MenuItem("Vis reservationer", MenuShowReservations),
            new MenuItem("Søg", BoardGameUI.SearchGamesUI)              // [NEW]
        };

            MenuPaginator menu = new(menuItems, pageSize: 10);
            if (menu.menuItem != null && menu.menuItem.Action is Action action)
                action();
            else
                return;

        } while (true);
    }


    static void MenuCustomer()
    {
        Console.Clear();
        Console.WriteLine("Kundemenu er ikke implementeret endnu.");
        Console.ReadLine();
    }

    static void MenuReport()
    {
        do
        {
            Console.Clear();
            HeadLine("Rapport");
            List<MenuItem> menuItems = new()
            {
                new MenuItem("Lagerstatus sorteret på titel", ShowReportBoardGameSortTitle),
                new MenuItem("Lagerstatus sorteret på genre", ShowReportBoardGameSortGenre)
            };

            MenuPaginator menu = new(menuItems, 10);
            if (menu.menuItem != null && menu.menuItem.Action is Action action)
                action();
            else
                return;

        } while (true);
    }

    static void MenuAdmin()
    {
        Console.Clear();
        Console.WriteLine("Adminmenu er ikke implementeret endnu.");
        Console.ReadLine();
    }

    static void ShowReportBoardGameSortTitle()
    {
        Console.WriteLine("Viser sortering efter titel (ikke implementeret endnu)");
        Console.ReadLine();
    }

    static void ShowReportBoardGameSortGenre()
    {
        Console.WriteLine("Viser sortering efter genre (ikke implementeret endnu)");
        Console.ReadLine();
    }

    static void Main(string[] args)
    {
        JsonFileHandler.Instance.ImportData(DATA_JSON_FILE);
        Login();
        MenuMain();
        JsonFileHandler.Instance.ExportData(DATA_JSON_FILE);
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
}
