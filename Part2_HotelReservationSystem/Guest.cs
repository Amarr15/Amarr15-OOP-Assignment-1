using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem
{
    public class Guest
    {
        public int GuestId { get; }
        public string FullName { get; }
        public string PhoneNumber { get; }

        private readonly List<Reservation> _reservations = new();

        public IReadOnlyList<Reservation> Reservations => _reservations;

        public Guest(
            int guestId,
            string fullName,
            string phoneNumber)
        {
            if (guestId <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(guestId),
                    "Guest ID must be greater than 0.");

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException(
                    "Full name cannot be empty.",
                    nameof(fullName));

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException(
                    "Phone number cannot be empty.",
                    nameof(phoneNumber));

            GuestId = guestId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }

        internal void AddReservation(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            _reservations.Add(reservation);
        }

    }
}
