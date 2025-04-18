namespace GenSpil.Model
{
    public class ConditionList
    {
        public ICollection<Condition> Conditions { get; private set; }

        public ConditionList()
        {
            Conditions = new List<Condition>();
            foreach (Type.Condition condition in Enum.GetValues(typeof(Type.Condition)))
            {
                Conditions.Add(new Condition(condition, 0, 0));
            }
        }


    }
}