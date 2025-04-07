using System;
using System.Collections.Generic;

namespace GenSpil.Model
{
    public class BoardGameVariant
    {
        public string Title { get; set; } = string.Empty;       // [CHANGED] gjort public set + default
        public string Variant { get; set; } = string.Empty;     // [CHANGED] gjort public set + default
        public List<Reserve> Reservations { get; set; } = new(); // [CHANGED] gjort public + default

        public BoardGameVariant() { } // [NEW] parameterløs constructor til JSON

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
}
