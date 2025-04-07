namespace GenSpil.Model
{
    public class BoardGameVariant
    {
        public string Title { get; private set; }
        public string Variant { get; private set; }
        private Reserve _reserved; //> Reserve object for the board game

        public BoardGameVariant(string title, string variant)
        {
            Title = title;
            Variant = variant;
        }

        public Reserve GetReserved()
        {
            return _reserved;
        }

        public void SetReserved(Reserve reserved)
        {
            _reserved = reserved;
        }
    }
}
