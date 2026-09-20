using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem
{
    public class Reservation
    {
        public int ReservationId { get; }
        public Guest Guest { get; }
        public Room Room { get; }
        public DateTime CheckInDate { get; }
        public DateTime CheckOutDate { get; }
        public ReservationStatus Status { get; private set; }

        public Reservation(
            int reservationId,
            Guest guest,
            Room room,
            DateTime checkInDate,
            DateTime checkOutDate)
        {
            if (reservationId <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(reservationId),
                    "Reservation ID must be greater than 0.");

            Guest = guest
                ?? throw new ArgumentNullException(nameof(guest));

            Room = room
                ?? throw new ArgumentNullException(nameof(room));

            if (checkOutDate <= checkInDate)
                throw new ArgumentException(
                    "Check-out date must be after check-in date.");

            if (room.IsUnderMaintenance)
                throw new InvalidOperationException(
                    "Cannot reserve a room under maintenance.");

            ReservationId = reservationId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Status = ReservationStatus.Pending;
        }

        public int GetNights()
        {
            return (CheckOutDate - CheckInDate).Days;
        }

        public decimal GetTotalCost()
        {
            return GetNights() * Room.NightlyRate;
        }

        public void Confirm()
        {
            if (Status != ReservationStatus.Pending)
                throw new InvalidOperationException(
                    "Only pending reservations can be confirmed");

            Status = ReservationStatus.Confirmed;
        }

        public void CheckIn()
        {
            if (Status != ReservationStatus.Confirmed)
                throw new InvalidOperationException(
                    "Only confirmed reservations can be checked in.");

            Status = ReservationStatus.CheckedIn;
        }

        public void CheckOut()
        {
            if (Status != ReservationStatus.CheckedIn)
                throw new InvalidOperationException(
                    "Only checked-in reservations can be checked out.");

            Status = ReservationStatus.CheckedOut;
        }

        public void Cancel()
        {
            if (Status != ReservationStatus.Pending &&
                Status != ReservationStatus.Confirmed)
            {
                throw new InvalidOperationException(
                    "Only pending or confirmed reservations can be cancelled.");
            }

            Status = ReservationStatus.Cancelled;
        }
    }
}
