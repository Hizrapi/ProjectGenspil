using GenSpil.Type;

namespace GenSpil.Model
{
    public class BoardGame
    {
        private int _boardGameID;
        private string _title;
        private BoardGameVariant _variant;
        private Genre _genre;
        private Condition _condition;

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
            _boardGameID = boardGameID;
            _title = title;
            _variant = variant;
            _genre = genre;
            _condition = condition;
        }

        public string Title() => _title;
        public void Title(string title) => _title = title;
        public override string ToString() => $"{_title} ({_variant.Variant}) - Genre: {_genre}, Tilstand: {_condition}";
    }
}
