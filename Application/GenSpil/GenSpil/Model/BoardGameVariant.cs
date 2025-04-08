namespace GenSpil.Model;

public class BoardGameVariant
{
    public string Title { get; private set; }
    public string Variant { get; private set; }
    public List<Reserve> Reservations { get; private set; } // List of reservations for this variant


    public BoardGameVariant(string title, string variant, List<Reserve>? reservations)
    {
        Title = title;
        Variant = variant;
        Reservations = reservations ?? [];
    }

    public void AddReservationToList(int customerID, DateTime reservedDate, int quantity)
    {
        Reserve reservation = new Reserve(reservedDate, customerID, quantity);
        SetReserved(reservation);
    }

    public void SetReserved(Reserve reservation)
    {
        Reservations.Add(reservation);
    }

    public List<Reserve> GetReservations()
    {
        return Reservations;
    }

    public void RemoveReservation(Reserve reservation)
    {
        Reservations.Remove(reservation);
    }
}