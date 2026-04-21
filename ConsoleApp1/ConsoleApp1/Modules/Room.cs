using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Modules;

public class Room
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string BuildingCode { get; set; }
    public int Floor { get; set; }
    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
}
