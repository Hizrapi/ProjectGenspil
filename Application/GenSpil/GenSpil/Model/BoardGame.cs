using GenSpil.Type;

namespace GenSpil.Model
{
    public class BoardGame
    {
        public int BoardGameID { get; set; } // [CHANGED] Gør ID synligt og serialiserbart
        public string Title { get; set; } = string.Empty; // [CHANGED] Public setter + default værdi
        public BoardGameVariant Variant { get; set; } = new(); // [CHANGED]
        public Genre Genre { get; set; } // Enum kan godt serialiseres med converter
        public Condition Condition { get; set; } = new(); // [CHANGED]

        // [NEW] Parameterløs konstruktor til JSON-deserialisering
        public BoardGame() { }

        // Bruges manuelt i programmet
        public BoardGame(int boardGameID, string title, BoardGameVariant variant, Genre genre, Condition condition)
        {
            BoardGameID = boardGameID;
            Title = title;
            Variant = variant;
            Genre = genre;
            Condition = condition;
        }

        public override string ToString() => $"{Title} ({Variant.Variant}) - Genre: {Genre}, Tilstand: {Condition}";
    }
}
