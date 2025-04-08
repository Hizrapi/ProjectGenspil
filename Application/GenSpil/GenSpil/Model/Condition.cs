namespace GenSpil.Model
{
    public class Condition
    {
        public Type.Condition ConditionType { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        /// <summary>
        /// Constructor til Condition
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        public Condition(Type.Condition conditionType, int quantity, decimal price)
        {
            ConditionType = conditionType;
            Quantity = quantity;
            Price = price;
        }

        public void SetCondition(Type.Condition conditionType)
        {
            ConditionType = conditionType;
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public void SetPrice(decimal price)
        {
            Price = price;
        }

        public override string ToString() => $"{ConditionType} - Antal: {Quantity}, Pris: {Price} kr";
    }
}