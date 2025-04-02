namespace GenSpil.Model
{
    public class BoardGameVariant
    {
        public string Title { get; private set; }
        public string Variant { get; private set; }

        public BoardGameVariant(string title, string variant)
        {
            Title = title;
            Variant = variant;
        }
    }
}
