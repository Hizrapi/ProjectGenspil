namespace GenSpil.Model
{
    public class Condition
    {
        public Type.Condition ConditionType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Condition() { } // [NEW] ← vigtigt for JSON

        /// <summary>
        /// Constructor til Condition
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        public Condition(Type.Condition condition, int quantity, decimal price)
        {
            ConditionType = condition;
            Quantity = quantity;
            Price = price;
        }

        public override string ToString() => $"{ConditionType} - Antal: {Quantity}, Pris: {Price} kr";
    }
}
