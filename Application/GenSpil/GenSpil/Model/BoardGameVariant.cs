namespace GenSpil.Model
{
    public class BoardGameVariant
    {
        public string Title { get; private set; }
        public Reserve Reserved { get; private set; } = null!; // Initialize to null to avoid unassigned variable error
        public ConditionList ConditionList { get; private set; }

        public BoardGameVariant(string title, ConditionList conditionList)
        {
            Title = title;
            ConditionList = conditionList;
        }

        public Reserve GetReserved()
        {
            return Reserved;
        }

        public void SetReserved(Reserve reserved)
        {
            Reserved = reserved;
        }
    }
}
