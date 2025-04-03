namespace GenSpil.Model
{
    public class Condition
    {
        private Type.Condition _condition;
        private int _quantity;
        private decimal _price;

        /// <summary>
        /// Constructor til Condition
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        public Condition(Type.Condition condition, int quantity, decimal price)
        {
            _condition = condition;
            _quantity = quantity;
            _price = price;
        }

        public override string ToString() => $"{_condition} - Antal: {_quantity}, Pris: {_price} kr";
    }
}
