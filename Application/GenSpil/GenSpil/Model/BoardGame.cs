using GenSpil.Type;

namespace GenSpil.Model
{
    public class BoardGame
    {
        public int BoardGameID { get; private set; }
        public string Title { get; private set; }
        public BoardGameVariant Variant { get; private set; }
        public Genre Genre { get; private set; }
        public Condition Condition { get; private set; }

        /// <summary>
        /// Constructor til at at samle al data, i en.
        /// </summary>
        /// <param name="boardGameID"></param>
        /// <param name="title"></param>
        /// <param name="variant"></param>
        /// <param name="genre"></param>
        /// <param name="condition"></param>
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