using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Modules;

public class Reservation
{
    public int Id { get; set; }
    [Required]
    public int RoomId { get; set; }
    [Required]
    public string OrganizerName { get; set; }
    [Required]
    public string Topic { get; set; }
    [Required]
    public DateOnly Date { get; set; }
    [Required]
    public TimeOnly StartTime { get; set; }
    [Required]
    public TimeOnly EndTime { get; set; }
    public string Status { get; set; } = "planned";
}