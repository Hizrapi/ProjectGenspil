
namespace GenSpil.Model;

public class Reserve
{

    /// <summary>
    /// Egenskaber for Reserve class 
    /// </summary>
    DateTime _reservedDate { get; set; }
    int _quantity { get; set; }
    int _customerID { get; set; }

    /// <summary>
    /// Constructor for Reserve class
    /// </summary>
    /// <param name="reservedDate"></param>
    /// <param name="quantity"></param>
    /// <param name="customerID"></param>
    public Reserve(DateTime reservedDate, int quantity, int customerID, int boardGameID)
    {
        _reservedDate = reservedDate;
        _quantity = quantity;
        _customerID = customerID;
        
    }

}
