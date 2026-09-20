namespace HotelReservationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var guests = new List<Guest>();
            var rooms = new List<Room>();
            var reservations = new List<Reservation>();


            SeedData();

            RunDemo();

            RunInteractiveMenu();


            void SeedData()
            {
                guests.Add(
                    new Guest(
                        1,
                        "Ahmed Ali",
                        "01012345678"));

                guests.Add(
                    new Guest(
                        2,
                        "Omar Hassan",
                        "01198765432"));


                rooms.Add(
                    new Room(
                        101,
                        RoomType.Single,
                        1000m));

                rooms.Add(
                    new Room(
                        102,
                        RoomType.Double,
                        1500m));

                rooms.Add(
                    new Room(
                        201,
                        RoomType.Suite,
                        2500m));
            }


            Guest? FindGuestById(int id)
            {
                return guests.FirstOrDefault(
                    g => g.GuestId == id);
            }


            Room? FindRoomByNumber(int roomNumber)
            {
                return rooms.FirstOrDefault(
                    r => r.RoomNumber == roomNumber);
            }


            Reservation? FindReservationById(int id)
            {
                return reservations.FirstOrDefault(
                    r => r.ReservationId == id);
            }


            bool HasOverlap(
                Room room,
                DateTime checkIn,
                DateTime checkOut)
            {
                foreach (var reservation in reservations)
                {
                    if (reservation.Room != room)
                        continue;

                    if (reservation.Status == ReservationStatus.Cancelled ||
                        reservation.Status == ReservationStatus.CheckedOut)
                    {
                        continue;
                    }

                    bool overlaps =
                        checkIn < reservation.CheckOutDate &&
                        checkOut > reservation.CheckInDate;

                    if (overlaps)
                        return true;
                }

                return false;
            }


            Reservation? CreateReservation(
                int reservationId,
                int guestId,
                int roomNumber,
                DateTime checkIn,
                DateTime checkOut)
            {
                if (FindReservationById(reservationId) != null)
                {
                    Console.WriteLine(
                        "ERROR: Reservation ID already exists.");

                    return null;
                }

                var guest = FindGuestById(guestId);

                if (guest == null)
                {
                    Console.WriteLine(
                        "ERROR: Guest not found.");

                    return null;
                }

                var room = FindRoomByNumber(roomNumber);

                if (room == null)
                {
                    Console.WriteLine(
                        "ERROR: Room not found.");

                    return null;
                }

                if (checkOut <= checkIn)
                {
                    Console.WriteLine(
                        "ERROR: Check-out must be after check-in.");

                    return null;
                }

                if (room.IsUnderMaintenance)
                {
                    Console.WriteLine(
                        "ERROR: Room is under maintenance.");

                    return null;
                }

                if (HasOverlap(
                    room,
                    checkIn,
                    checkOut))
                {
                    Console.WriteLine(
                        "ERROR: Room is already booked for these dates.");

                    return null;
                }

                try
                {
                    var reservation = new Reservation(
                        reservationId,
                        guest,
                        room,
                        checkIn,
                        checkOut);

                    reservations.Add(reservation);

                    AddReservationToGuest(
                        guest,
                        reservation);

                    return reservation;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"ERROR: {ex.Message}");

                    return null;
                }
            }


            void AddReservationToGuest(
                Guest guest,
                Reservation reservation)
            {
                // Guest controls its own reservation list.
                // This method is only used by the program
                // to connect the reservation to the guest.
                guest.AddReservation(reservation);
            }


            void PrintRooms()
            {
                Console.WriteLine("\n=== ROOMS ===");

                foreach (var room in rooms)
                {
                    Console.WriteLine(
                        $"Room #{room.RoomNumber} | " +
                        $"Type: {room.RoomType} | " +
                        $"Rate: {room.NightlyRate:0.00} | " +
                        $"Maintenance: " +
                        $"{(room.IsUnderMaintenance ? "Yes" : "No")}");
                }
            }


            void PrintGuests()
            {
                Console.WriteLine("\n=== GUESTS ===");

                foreach (var guest in guests)
                {
                    Console.WriteLine(
                        $"#{guest.GuestId} | " +
                        $"{guest.FullName} | " +
                        $"{guest.PhoneNumber} | " +
                        $"Reservations: {guest.Reservations.Count}");
                }
            }


            void PrintReservation(
                Reservation reservation)
            {
                Console.WriteLine(
                    $"\n=== RESERVATION #{reservation.ReservationId} ===");

                Console.WriteLine(
                    $"Guest: {reservation.Guest.FullName}");

                Console.WriteLine(
                    $"Room: #{reservation.Room.RoomNumber}");

                Console.WriteLine(
                    $"Check-in: {reservation.CheckInDate:yyyy-MM-dd}");

                Console.WriteLine(
                    $"Check-out: {reservation.CheckOutDate:yyyy-MM-dd}");

                Console.WriteLine(
                    $"Status: {reservation.Status}");

                Console.WriteLine(
                    $"Nights: {reservation.GetNights()}");

                Console.WriteLine(
                    $"Total: {reservation.GetTotalCost():0.00}");
            }


            void PrintAllReservations()
            {
                Console.WriteLine("\n=== RESERVATIONS ===");

                foreach (var reservation in reservations)
                {
                    PrintReservation(reservation);
                }
            }


            void RunDemo()
            {
                var reservation1 = CreateReservation(
                    1001,
                    1,
                    101,
                    new DateTime(2026, 10, 1),
                    new DateTime(2026, 10, 4));

                if (reservation1 != null)
                {
                    reservation1.Confirm();

                    Console.WriteLine(
                        "\nReservation 1001 confirmed.");

                    PrintReservation(reservation1);
                }


                var overlappingReservation = CreateReservation(
                    1002,
                    2,
                    101,
                    new DateTime(2026, 10, 2),
                    new DateTime(2026, 10, 5));

                if (overlappingReservation == null)
                {
                    Console.WriteLine(
                        "Double-booking prevented successfully.");
                }


                var reservation2 = CreateReservation(
                    1003,
                    2,
                    102,
                    new DateTime(2026, 10, 5),
                    new DateTime(2026, 10, 7));

                if (reservation2 != null)
                {
                    reservation2.Confirm();
                    reservation2.CheckIn();

                    Console.WriteLine(
                        "\nReservation 1003 checked in.");

                    PrintReservation(reservation2);

                    reservation2.CheckOut();

                    Console.WriteLine(
                        "\nReservation 1003 checked out.");

                    PrintReservation(reservation2);
                }
            }


            void RunInteractiveMenu()
            {
                int choice = -1;

                while (choice != 0)
                {
                    Console.WriteLine("\n========== HOTEL MENU ==========");
                    Console.WriteLine("1) Print guests");
                    Console.WriteLine("2) Print rooms");
                    Console.WriteLine("3) Print reservations");
                    Console.WriteLine("4) Create reservation");
                    Console.WriteLine("5) Confirm reservation");
                    Console.WriteLine("6) Check in");
                    Console.WriteLine("7) Check out");
                    Console.WriteLine("8) Cancel reservation");
                    Console.WriteLine("9) Start room maintenance");
                    Console.WriteLine("10) End room maintenance");
                    Console.WriteLine("11) Change room nightly rate");
                    Console.WriteLine("0) Exit");

                    Console.Write("Choice: ");

                    if (!int.TryParse(
                        Console.ReadLine(),
                        out choice))
                    {
                        Console.WriteLine(
                            "Invalid choice.");

                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            PrintGuests();
                            break;

                        case 2:
                            PrintRooms();
                            break;

                        case 3:
                            PrintAllReservations();
                            break;

                        case 4:
                            CreateReservationFromInput();
                            break;

                        case 5:
                            ChangeReservationStatus(
                                "confirm");
                            break;

                        case 6:
                            ChangeReservationStatus(
                                "checkin");
                            break;

                        case 7:
                            ChangeReservationStatus(
                                "checkout");
                            break;

                        case 8:
                            ChangeReservationStatus(
                                "cancel");
                            break;

                        case 9:
                            ChangeMaintenance(true);
                            break;

                        case 10:
                            ChangeMaintenance(false);
                            break;

                        case 11:
                            ChangeRoomRate();
                            break;

                        case 0:
                            Console.WriteLine("Bye.");
                            break;

                        default:
                            Console.WriteLine(
                                "Unknown choice.");

                            break;
                    }
                }
            }


            void CreateReservationFromInput()
            {
                Console.Write("Reservation ID: ");

                if (!int.TryParse(
                    Console.ReadLine(),
                    out int reservationId))
                {
                    Console.WriteLine("Invalid ID.");
                    return;
                }

                Console.Write("Guest ID: ");

                if (!int.TryParse(
                    Console.ReadLine(),
                    out int guestId))
                {
                    Console.WriteLine("Invalid guest ID.");
                    return;
                }

                Console.Write("Room number: ");

                if (!int.TryParse(
                    Console.ReadLine(),
                    out int roomNumber))
                {
                    Console.WriteLine("Invalid room number.");
                    return;
                }

                Console.Write("Check-in date (YYYY-MM-DD): ");

                if (!DateTime.TryParse(
                    Console.ReadLine(),
                    out DateTime checkIn))
                {
                    Console.WriteLine("Invalid date.");
                    return;
                }

                Console.Write("Check-out date (YYYY-MM-DD): ");

                if (!DateTime.TryParse(
                    Console.ReadLine(),
                    out DateTime checkOut))
                {
                    Console.WriteLine("Invalid date.");
                    return;
                }

                CreateReservation(
                    reservationId,
                    guestId,
                    roomNumber,
                    checkIn,
                    checkOut);
            }


            void ChangeReservationStatus(
                string action)
            {
                Console.Write("Reservation ID: ");

                if (!int.TryParse(
                    Console.ReadLine(),
                    out int reservationId))
                {
                    Console.WriteLine("Invalid ID.");
                    return;
                }

                var reservation =
                    FindReservationById(reservationId);

                if (reservation == null)
                {
                    Console.WriteLine(
                        "ERROR: Reservation not found.");

                    return;
                }

                try
                {
                    switch (action)
                    {
                        case "confirm":
                            reservation.Confirm();
                            break;

                        case "checkin":
                            reservation.CheckIn();
                            break;

                        case "checkout":
                            reservation.CheckOut();
                            break;

                        case "cancel":
                            reservation.Cancel();
                            break;
                    }

                    Console.WriteLine(
                        $"Reservation status: {reservation.Status}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"ERROR: {ex.Message}");
                }
            }


            void ChangeMaintenance(
                bool start)
            {
                Console.Write("Room number: ");

                if (!int.TryParse(
                    Console.ReadLine(),
                    out int roomNumber))
                {
                    Console.WriteLine(
                        "Invalid room number.");

                    return;
                }

                var room = FindRoomByNumber(roomNumber);

                if (room == null)
                {
                    Console.WriteLine(
                        "ERROR: Room not found.");

                    return;
                }

                if (start)
                {
                    room.StartMaintenance();

                    Console.WriteLine(
                        $"Room {room.RoomNumber} is now under maintenance.");
                }
                else
                {
                    room.EndMaintenance();

                    Console.WriteLine(
                        $"Room {room.RoomNumber} is no longer under maintenance.");
                }
            }


            void ChangeRoomRate()
            {
                Console.Write("Room number: ");

                if (!int.TryParse(
                    Console.ReadLine(),
                    out int roomNumber))
                {
                    Console.WriteLine(
                        "Invalid room number.");

                    return;
                }

                var room = FindRoomByNumber(roomNumber);

                if (room == null)
                {
                    Console.WriteLine(
                        "ERROR: Room not found.");

                    return;
                }

                Console.Write("New nightly rate: ");

                if (!decimal.TryParse(
                    Console.ReadLine(),
                    out decimal newRate))
                {
                    Console.WriteLine(
                        "Invalid rate.");

                    return;
                }

                try
                {
                    room.ChangeNightlyRate(newRate);

                    Console.WriteLine(
                        $"Room rate changed to {room.NightlyRate:0.00}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"ERROR: {ex.Message}");
                }
            }
        }
    }
}
