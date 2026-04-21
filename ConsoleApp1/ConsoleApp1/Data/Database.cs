using ConsoleApp1.Modules;
namespace ConsoleApp1.Data;

public class Database
{
    public static List<Room> Rooms = new()
    {
        new Room { Id = 1, Name = "room1", BuildingCode = "a", Floor = 1, Capacity = 3, HasProjector = false, IsActive = true },
        new Room { Id = 2, Name = "room2", BuildingCode = "b", Floor = 4, Capacity = 15, HasProjector = true, IsActive = true },
        new Room { Id = 3, Name = "room3", BuildingCode = "b", Floor = 3, Capacity = 9, HasProjector = false, IsActive = true },
        new Room { Id = 4, Name = "room4", BuildingCode = "a", Floor = 2, Capacity = 11, HasProjector = true, IsActive = false }
    };

    public static List<Reservation> Reservations = new()
    {
        new Reservation { Id = 1, RoomId = 1, OrganizerName = "Kamil Kamil", Topic = "cos", 
            Date = new DateOnly(2026, 4, 11), StartTime = new TimeOnly(11, 0), 
            EndTime = new TimeOnly(12, 0), Status = "confirmed" },
        new Reservation { Id = 2, RoomId = 2, OrganizerName = "Adam Adam", 
            Topic = "cos2", Date = new DateOnly(2026, 5, 13), 
            StartTime = new TimeOnly(16, 0), EndTime = new TimeOnly(17, 0), Status = "planned" },
        new Reservation { Id = 3, RoomId = 1, OrganizerName = "Szymon Szymon", 
            Topic = "cos3", Date = new DateOnly(2026, 4, 17), 
            StartTime = new TimeOnly(16, 0), EndTime = new TimeOnly(17, 0), Status = "cancelled" },
        new Reservation { Id = 4, RoomId = 4, OrganizerName = "Tymon Tymon", 
            Topic = "cos4", Date = new DateOnly(2026, 5, 18), 
            StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(10, 0), Status = "planned" },
    };
}