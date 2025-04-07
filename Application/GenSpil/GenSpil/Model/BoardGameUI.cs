using GenSpil.Model;
using GenSpil.Type;

// [NEW] Alias-navne så vi undgår konflikt mellem enum og model-klasse
using ConditionModel = GenSpil.Model.Condition;
using ConditionEnum = GenSpil.Type.Condition;

namespace GenSpil.UI;

public static class BoardGameUI
{
    public static void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Tilføj brætspil");
            Console.WriteLine("2. Vis brætspil");
            Console.WriteLine("3. Søg brætspil");
            Console.WriteLine("4. Rediger brætspil");
            Console.WriteLine("5. Fjern brætspil");
            Console.WriteLine("0. Afslut");

            Console.Write("\nValg: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddGameUI(); break;
                case "2": DisplayGamesUI(); break;
                case "3": SearchGamesUI(); break;
                case "4": EditGameUI(); break;
                case "5": RemoveGameUI(); break;
                case "0": return;
                default: Console.WriteLine("Ugyldigt valg."); Console.ReadLine(); break;
            }
        }
    }

    public static void AddGameUI()
    {
        Console.Write("Titel: ");
        string title = Console.ReadLine() ?? string.Empty;

        Console.Write("Variant: ");
        string variant = Console.ReadLine() ?? string.Empty;

        Console.Write($"Genre ({string.Join("/", Enum.GetNames(typeof(Genre)))}): ");
        if (!Enum.TryParse(Console.ReadLine(), true, out Genre genre))
        {
            Console.WriteLine("Ugyldig genre.");
            Console.ReadLine();
            return;
        }

        Console.Write($"Tilstand ({string.Join("/", Enum.GetNames(typeof(ConditionEnum)))}): ");
        if (!Enum.TryParse(Console.ReadLine(), true, out ConditionEnum conditionEnum))
        {
            Console.WriteLine("Ugyldig tilstand.");
            Console.ReadLine();
            return;
        }

        Console.Write("Antal: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
        {
            Console.WriteLine("Ugyldigt antal.");
            Console.ReadLine();
            return;
        }

        Console.Write("Pris: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
        {
            Console.WriteLine("Ugyldig pris.");
            Console.ReadLine();
            return;
        }

        var bgVariant = new BoardGameVariant(title, variant);
        var bgCondition = new ConditionModel(conditionEnum, quantity, price);
        var newGame = new BoardGame(BoardGameList.Instance.GetAllBoardGames().Count + 1, title, bgVariant, genre, bgCondition);

        BoardGameList.Instance.AddBoardGame(newGame);
        Console.WriteLine("Spil tilføjet!");
        Console.ReadLine();
    }

    public static void DisplayGamesUI()
    {
        var grouped = BoardGameList.Instance.GetAllBoardGames().GroupBy(g => g.Title).OrderBy(g => g.Key);
        foreach (var group in grouped)
        {
            Console.WriteLine($"\n{group.Key}:");
            foreach (var game in group.OrderBy(g => g.Variant.Variant))
            {
                Console.WriteLine($" - Variant: {game.Variant.Variant}, Genre: {game.Genre}, Tilstand: {game.Condition}");
            }
        }
        Console.ReadLine();
    }

    public static void SearchGamesUI()
    {
        Console.Write("Søg titel: ");
        var input = Console.ReadLine();
        var results = BoardGameList.Instance.SearchBoardGames(input ?? "");

        foreach (var game in results)
        {
            Console.WriteLine($" - {game.Title} ({game.Variant.Variant})");
        }
        Console.ReadLine();
    }

    public static void RemoveGameUI()
    {
        var games = BoardGameList.Instance.GetAllBoardGames();
        for (int i = 0; i < games.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {games[i]}");
        }

        Console.Write("Vælg spil nr. der skal fjernes: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= games.Count)
        {
            BoardGameList.Instance.RemoveBoardGame(games[index - 1]);
            Console.WriteLine("Spil fjernet.");
        }
        else
        {
            Console.WriteLine("Ugyldigt valg.");
        }
        Console.ReadLine();
    }

    public static void EditGameUI()
    {
        Console.Write("Søg spil til redigering: ");
        var input = Console.ReadLine();
        var results = BoardGameList.Instance.SearchBoardGames(input ?? "");

        for (int i = 0; i < results.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {results[i]}");
        }

        Console.Write("Vælg spil: ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > results.Count)
            return;

        var game = results[index - 1];

        Console.Write($"Ny tilstand ({game.Condition.ConditionType}): ");
        ConditionEnum? newCondition = null;
        var condInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(condInput) && Enum.TryParse(condInput, true, out ConditionEnum parsedCond))
        {
            newCondition = parsedCond;
        }

        Console.Write($"Nyt antal ({game.Condition.Quantity}): ");
        int? newQty = null;
        var qtyInput = Console.ReadLine();
        if (int.TryParse(qtyInput, out int parsedQty) && parsedQty >= 0)
        {
            newQty = parsedQty;
        }

        Console.Write($"Ny pris ({game.Condition.Price}): ");
        decimal? newPrice = null;
        var priceInput = Console.ReadLine();
        if (decimal.TryParse(priceInput, out decimal parsedPrice) && parsedPrice >= 0)
        {
            newPrice = parsedPrice;
        }

        BoardGameList.Instance.EditBoardGame(game, newCondition, newQty, newPrice);

        Console.WriteLine("Spil opdateret.");
        Console.ReadLine();
    }
}
