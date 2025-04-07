using GenSpil.Type;

namespace GenSpil.Model;

/// <summary>
/// Singleton class for handling a list of board games.
/// </summary>
public class BoardGameList
{
    private static BoardGameList? _instance;
    private static readonly object _lock = new object();
    private List<BoardGame> _boardGames = new List<BoardGame>();

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
    }

    public List<BoardGame> GetAllBoardGames() => _boardGames.ToList();

    public void AddBoardGame(BoardGame game) // [NEW]
    {
        _boardGames.Add(game);
    }

    public List<BoardGame> SearchBoardGames(string title) // [NEW]
    {
        return _boardGames
            .Where(g => g.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            .OrderBy(g => g.Title)
            .ToList();
    }

    public void RemoveBoardGame(BoardGame game) // [NEW]
    {
        _boardGames.Remove(game);
    }

    public void EditBoardGame(BoardGame game, Type.Condition? condition, int? quantity, decimal? price) // [NEW]
    {
        if (condition.HasValue)
            game.Condition.ConditionType = condition.Value;

        if (quantity.HasValue)
            game.Condition.Quantity = quantity.Value;

        if (price.HasValue)
            game.Condition.Price = price.Value;

        if (game.Condition.Quantity == 0)
            RemoveBoardGame(game);
    }

    public void RegisterReservation(BoardGame game, int customerID, DateTime date, int quantity)
    {
        game.Variant.AddReservationToList(customerID, date, quantity);
    }

    public void Clear()
    {
        _boardGames.Clear();
    }
}
