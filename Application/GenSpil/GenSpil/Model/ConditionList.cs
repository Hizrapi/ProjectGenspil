namespace GenSpil.Model
{
    public class ConditionList
    {
        List<Condition> _conditions = new List<Condition>();

        public ConditionList()
        {
            foreach (Type.Condition condition in Enum.GetValues(typeof(Type.Condition)))
            {
                _conditions.Add(new Condition(condition, 0, 0));
            }
        }
    }
}
