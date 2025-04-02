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

    public List<BoardGame> BoardGames { get; set; }

    private BoardGameList()
    {
        BoardGames = new List<BoardGame>();
    }

    /// <summary>
    /// Tilføjer et brætspil.
    /// </summary>
    public void Add(BoardGame boardGame)
    {
        BoardGames.Add(boardGame);
    }

    public void Clear()
    {
        BoardGames.Clear();
    }

    /// <summary>
    /// Vis brætspil
    /// </summary>
    public override string ToString()
    {
        if (BoardGames.Count == 0)
        {
            return "Ingen brætspil fundet.";
        }
        string result = "--- Brætspil ---\n";
        foreach (var game in BoardGames)
        {
            result += game.ToString();
        }
        return result;
    }

    /// <summary>
    /// Søg efter brætspil
    /// </summary>
    public List<BoardGame> Search(string? title, Type.Genre? genre, string? variant, Type.Condition? condition, string? price)
    {
        lock (_lock)
        {
            var filteredBoardGames = BoardGames.AsEnumerable();

            if (title != null)
            {
                filteredBoardGames = filteredBoardGames.Where(x => x.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }
            if (genre != null)
            {
                filteredBoardGames = filteredBoardGames.Where(x => x.Genre.Contains(genre.Value));
            }
            if (variant != null)
            {
                filteredBoardGames = filteredBoardGames.Where(x => x.Variants.Any(v => v.Title.Contains(variant, StringComparison.OrdinalIgnoreCase)));
            }
            if (condition != null)
            {
                //filteredBoardGames = filteredBoardGames.Where(x => x.Variants.Any(v => v.Conditions.Any(c => c.ToString().Contains(condition.ToString(), StringComparison.OrdinalIgnoreCase))));
            }
            if (price != null)
            {
                if (price.Contains(">="))
                {
                    //filteredBoardGames = filteredBoardGames.Where(x => x.Variants.Any(v => v.Conditions.Any(c => c. >= decimal.Parse(price.Replace(">=", ""))));
                }
                else if (price.Contains("<="))
                {
                    //filteredBoardGames = filteredBoardGames.Where(x => x.Variants.Any(v => v.Price <= decimal.Parse(price.Replace("<=", ""))));
                }
                else
                {
                    //filteredBoardGames = filteredBoardGames.Where(x => x.Variants.Any(v => v.Price == decimal.Parse(price)));
                }
            }
            return filteredBoardGames.ToList();
        }
    }

    /// <summary>
    /// Fjern brætspil
    /// </summary>
    public void Remove(BoardGame boardGame) //TODO Could be renamed to Remove and take parameter BoardGame. Should not interaction with user (Tirsvad)
    {
        BoardGames.Remove(boardGame);
    }
}
