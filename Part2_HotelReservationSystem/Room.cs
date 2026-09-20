using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem
{
    public class Room
    {

        public int RoomNumber { get; }
        public RoomType RoomType { get; }
        public decimal NightlyRate { get; private set; }
        public bool IsUnderMaintenance { get; private set; }
        public Room(int roomNumber, RoomType roomType, decimal nightlyRate)
        {
            if (roomNumber <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(roomNumber),
                    "Room number must be greater than 0.");

            if (nightlyRate <= 0) throw new ArgumentOutOfRangeException(nameof(nightlyRate),"Nightly rate must be greater than 0");
            RoomNumber = roomNumber;
            RoomType = roomType;
            NightlyRate = nightlyRate;
            IsUnderMaintenance = false;
        }
        public void ChangeNightlyRate(decimal newRate)
        {
            if (newRate <= 0) throw new ArgumentOutOfRangeException(
                    nameof(newRate),
                    "Nightly rate must be greater than 0");

            NightlyRate = newRate;
        }
        public void StartMaintenance()
        {
            IsUnderMaintenance = true;
        }

        public void EndMaintenance()
        {
            IsUnderMaintenance = false;
        }
    }
}
