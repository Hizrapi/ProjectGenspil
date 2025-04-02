using GenSpil.Type;

namespace GenSpil.Model;

/// <summary>
/// Singleton class for handling a list of board games.
/// TODO Should not interact with the user directly. (Tirsvad)
/// </summary>
public class BoardGameList
{
    private static BoardGameList? _instance;
    private static readonly object _lock = new object();
    public static BoardGameList Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new BoardGameList();
                }
                return _instance;
            }
        }
    } ///> Singleton instance of the BoardGameList


    private List<BoardGame> _boardGames = new List<BoardGame>();

    public void ManageBoardGames() //TODO Not nessesary as model should be backend. Should not interaction with user (Tirsvad)
    {
        string userChoice;

        do
        {
            Console.Clear();
            Console.WriteLine("--- Brætspilsmenu ---");
            Console.WriteLine("1. Tilføj brætspil");
            Console.WriteLine("2. Vis brætspil");
            Console.WriteLine("3. Søg efter titel");
            Console.WriteLine("4. Fjern brætspil");
            Console.WriteLine("5. Retuner");
            Console.Write("Vælg en mulighed: ");

            userChoice = Console.ReadLine();

            switch (userChoice)
            {

                case "1":
                    AddBoardGame(); //Skal opdateres med varianter sådan at et spil har flere varianter.
                    break;

                case "2":
                    DisplayBoardGames();
                    break;

                case "3":
                    SearchBoardGames();
                    break;

                case "4":
                    Console.WriteLine("Updater nuværende spil"); //Denne funktion mangler
                    break;

                case "5":
                    RemoveBoardGame();
                    break;

                case "6": return;

                default:
                    Console.WriteLine("Ugyldigt valg.");
                    break;
            }
        } while (userChoice != "6");

    }

    /// <summary>
    /// Tilføjer et brætspil.
    /// </summary>
    public void AddBoardGame() //TODO Could be renamed to Add. Should not interaction with user (Tirsvad)
    {
        Console.Write("Titel: ");
        string title = Console.ReadLine();
        Console.Write("Variant: ");
        string variant = Console.ReadLine();


        Console.Write($"Genre ({string.Join(" / ", Enum.GetNames(typeof(Genre)))}): ");
        string genreInput = Console.ReadLine();

        // Forsøg at konvertere genre-input til Genre enum (ignorerer store/små bogstaver)
        bool genreValid = Enum.TryParse(genreInput, true, out Genre genre);
        if (!genreValid)
        {
            Console.WriteLine("Ugyldig genre angivet. Handlingen afbrydes.");
            return;
        }


        // Viser de mulige tilstands-valg
        Console.Write($"Tilstand ({string.Join(" / ", Enum.GetNames(typeof(Type.Condition)))}): ");
        string conditionInput = Console.ReadLine();

        // Forsøg at konvertere tilstands-input til ConditionEnum (ignorerer store/små bogstaver)
        bool conditionValid = Enum.TryParse(conditionInput, true, out Type.Condition conditionEnum);
        if (!conditionValid)
        {
            Console.WriteLine("Ugyldig tilstand angivet. Handlingen afbrydes.");
            return;
        }


        Console.Write("Antal: ");
        string quantityInput = Console.ReadLine();

        // Forsøg at konvertere antal-input til et heltal (int)
        bool quantityValid = int.TryParse(quantityInput, out int quantity);
        if (!quantityValid || quantity < 0) // Antal skal være et positivt heltal
        {
            Console.WriteLine("Ugyldigt antal angivet (skal være et positivt heltal). Handlingen afbrydes.");
            return;
        }

        // --- Price Input ---
        Console.Write("Pris: ");
        string priceInput = Console.ReadLine();

        // Forsøg at konvertere pris-input til et decimaltal
        // Bemærk: TryParse bruger computerens lokale indstillinger for decimaltegn (typisk komma på dansk)
        bool priceValid = decimal.TryParse(priceInput, out decimal price);
        if (!priceValid || price < 0) // Pris skal være et positivt tal
        {
            Console.WriteLine("Ugyldig pris angivet (skal være et positivt tal). Handlingen afbrydes.");
            return; // Gå ud af switch-casen
        }


        // Opret variant-objekt (bruger stadig titel som en del af varianten - kan justeres hvis nødvendigt)
        BoardGameVariant bgVariant = new BoardGameVariant(title, variant);

        // Opret condition-objekt med de indtastede værdier
        Condition bgCondition = new Condition(conditionEnum, quantity, price);

        // Opret det nye brætspil med alle data
        // Bruger _boardGames.Count + 1 som midlertidigt ID
        BoardGame newGame = new BoardGame(_boardGames.Count + 1, title, bgVariant, genre, bgCondition);

        // Tilføj spillet til listen
        _boardGames.Add(newGame);
        Console.WriteLine("Brætspil tilføjet!");

        Console.ReadLine();

    }

    /// <summary>
    /// Vis brætspil
    /// </summary>
    public void DisplayBoardGames() //TODO Could be renamed to ToString and return a string. Should not interaction with user (Tirsvad)
    {
        if (_boardGames.Count == 0)
        {
            Console.WriteLine("Der er ikke noget brætspil på listen endnu.");
        }
        else
        {
            Console.WriteLine("--- Liste over Brætspil ---");
            foreach (var game in _boardGames)
            {
                Console.WriteLine(game);
            }
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Søg efter brætspil
    /// </summary>
    public List<BoardGame> SearchBoardGames() //TODO SearchBoardGames() should take parameters. Rename it to Search. Should not interaction with user (Tirsvad)
    {
        Console.Write("Indtast titel - eller del af den: ");
        string searchTitle = Console.ReadLine();

        //Søg efter indput
        var foundGames = _boardGames.FindAll(g => g.Title().Contains(searchTitle, StringComparison.OrdinalIgnoreCase));
        if (foundGames.Count > 0)
        {
            Console.WriteLine($"--- Fundne Brætspil (Søgning: '{searchTitle}') ---");
            foreach (var game in foundGames)
            {
                Console.WriteLine(game);
            }
            Console.WriteLine("----------------------------------------------");
        }
        else
        {
            Console.WriteLine($"Ingen brætspil med {searchTitle}  fundet.");
        }
        Console.ReadLine();

        return foundGames;

    }

    /// <summary>
    /// Fjern brætspil
    /// </summary>
    public void RemoveBoardGame() //TODO Could be renamed to Remove and take parameter BoardGame. Should not interaction with user (Tirsvad)
    {
        Console.Clear();
        Console.WriteLine("--- Fjern Brætspil ---");

        if (_boardGames.Count == 0)
        {
            Console.WriteLine("Listen er tom. Der er ingen spil at fjerne.");
            return; // Gå tilbage, da der ikke er noget at gøre
        }

        Console.WriteLine("Vælg nummeret på det spil, du vil fjerne:");

        // Vis alle spil med et nummer (startende fra 1)
        for (int i = 0; i < _boardGames.Count; i++)
        {
            // Brug i + 1 for at vise 1-baseret nummerering til brugeren
            Console.WriteLine($"{i + 1}. {_boardGames[i]}"); // Bruger BoardGame.ToString()
        }
        Console.WriteLine("-------------------------");
        Console.Write("Indtast nummer på spil der skal fjernes (eller 0 for at annullere): ");

        string input = Console.ReadLine();

        // Prøv at konvertere input til et tal
        if (int.TryParse(input, out int choice))
        {
            if (choice == 0)
            {
                Console.WriteLine("Fjernelse annulleret.");
                return; // Gå tilbage
            }
            // Tjek om nummeret er inden for det gyldige område (1 til antal spil)
            if (choice >= 1 && choice <= _boardGames.Count)
            {
                // Konverter brugerens 1-baserede valg til 0-baseret index
                int indexToRemove = choice - 1;

                // Få fat i spillet der skal fjernes for at vise det i beskeden
                BoardGame gameToRemove = _boardGames[indexToRemove];

                // Valgfri: Spørg om bekræftelse
                Console.WriteLine($"\nDu er ved at fjerne: {gameToRemove}");
                Console.Write("Er du sikker? (J/N): ");
                string confirmation = Console.ReadLine();

                if (confirmation != null && confirmation.Trim().Equals("J", StringComparison.OrdinalIgnoreCase))
                {
                    // Fjern spillet fra listen ved det valgte index
                    _boardGames.RemoveAt(indexToRemove);
                    Console.WriteLine($"'{gameToRemove.Title()}' er blevet fjernet.");
                }
                else
                {
                    Console.WriteLine("Fjernelse annulleret.");
                }
            }
            else
            {
                // Brugeren indtastede et tal, men det var uden for listen
                Console.WriteLine("Ugyldigt nummer. Indtast venligst et nummer fra listen.");
            }
        }
        else
        {
            // Brugeren indtastede ikke et gyldigt tal
            Console.WriteLine("Ugyldigt input. Indtast venligst et tal.");
        }
        Console.ReadLine();
    }
}
