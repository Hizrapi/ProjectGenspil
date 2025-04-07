using System.Collections.Generic;
namespace GenSpil.Model;

public class BoardGameVariant
{
    public string Title { get; private set; }
    public string Variant { get; private set; }
    private List<Reserve> Reservations = new List<Reserve>(); // List of reservations for this variant

    public BoardGameVariant(string title, string variant)
    {
        Title = title;
        Variant = variant;
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