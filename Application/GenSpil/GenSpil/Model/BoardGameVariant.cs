namespace GenSpil.Model
{
    public class BoardGameVariant
    {
        public string Title { get; private set; } //> Title of the board game
        public string Variant { get; private set; } //> Variant of the board game
        public Reserve? Reserved { get; private set; } //> Reserve object for the board game

        public BoardGameVariant(string title, string variant)
        {
            Title = title;
            Variant = variant;
        }

        public Reserve? GetReserved()
        {
            return Reserved;
        }

        public void SetReserved(Reserve reserved)
        {
            Reserved = reserved;
        }
    }
}
