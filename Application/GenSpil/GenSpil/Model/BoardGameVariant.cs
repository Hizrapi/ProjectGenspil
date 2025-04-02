namespace GenSpil.Model
{
    public class BoardGameVariant
    {
        public string Title { get; private set; }
        public ConditionList Conditions { get; private set; }

        public BoardGameVariant(string title, ConditionList conditions)
        {
            Title = title;
            Conditions = conditions;
        }
    }
}
