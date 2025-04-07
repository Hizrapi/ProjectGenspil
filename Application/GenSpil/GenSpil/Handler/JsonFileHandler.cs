// File: JsonFileHandler.cs
using System.Text.Json;
using GenSpil.Model;

namespace GenSpil.Handler;

public class JsonFileHandler
{
    private static JsonFileHandler? _instance;
    public static JsonFileHandler Instance => _instance ??= new JsonFileHandler();

    private JsonFileHandler() { }

    // [NEW] Gemmer BoardGames til JSON-fil
    public void ExportData(string filePath)
    {
        var games = BoardGameList.Instance.GetAllBoardGames();

        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(directory))
            Directory.CreateDirectory(directory); // [DEBUG] Sikrer at mappen findes

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        string json = JsonSerializer.Serialize(games, options);
        File.WriteAllText(filePath, json);

        Console.WriteLine($"[DEBUG] Eksporterer {games.Count} spil til: {filePath}");
    }

    // [NEW] Indlæser BoardGames fra JSON-fil
    public void ImportData(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("[DEBUG] JSON-fil ikke fundet: " + filePath);
            return;
        }

        string json = File.ReadAllText(filePath);

        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            var games = JsonSerializer.Deserialize<List<BoardGame>>(json, options);
            if (games != null)
            {
                BoardGameList.Instance.Clear();
                foreach (var game in games)
                {
                    BoardGameList.Instance.AddBoardGame(game);
                    Console.WriteLine($"[DEBUG] Indlæst spil: {game.Title}");
                }

                Console.WriteLine($"[DEBUG] I alt indlæst {games.Count} spil fra JSON.");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[FEJL] Kunne ikke læse JSON-data!");
            Console.WriteLine("Type: " + ex.GetType().Name);
            Console.WriteLine("Besked: " + ex.Message);
            Console.WriteLine("StackTrace:\n" + ex.StackTrace);
            Console.ResetColor();
            Console.WriteLine("Tryk en tast for at fortsætte...");
            Console.ReadKey();
        }
    }
}
