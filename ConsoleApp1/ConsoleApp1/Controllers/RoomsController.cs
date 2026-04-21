
using ConsoleApp1.Modules;
using ConsoleApp1.Data;
using ConsoleApp1.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApp1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRooms([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var rooms = Database.Rooms.AsQueryable();

        if (minCapacity.HasValue) rooms = rooms.Where(r => r.Capacity >= minCapacity);
        if (hasProjector.HasValue) rooms = rooms.Where(r => r.HasProjector == hasProjector);
        if (activeOnly == true) rooms = rooms.Where(r => r.IsActive);

        return Ok(rooms.ToList());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetRoom(int id)
    {
        var room = Database.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound($"no room with id {id}");
        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public IActionResult GetByBuilding(string buildingCode)
    {
        var rooms = Database.Rooms.Where(r =>
            r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase));
        return Ok(rooms.ToList());
    }

    [HttpPost]
    public IActionResult CreateRoom([FromBody] Room room)
    {
        room.Id = Database.Rooms.Max(r => r.Id) + 1;
        Database.Rooms.Add(room);
        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
    {
        var index = Database.Rooms.FindIndex(r => r.Id == id);
        if (index == -1) return NotFound();

        updatedRoom.Id = id;
        Database.Rooms[index] = updatedRoom;
        return Ok(updatedRoom);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteRoom(int id)
    {
        var room = Database.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();
        
        if (Database.Reservations.Any(res => res.RoomId == id))
            return Conflict("cant remove");

        Database.Rooms.Remove(room);
        return NoContent();
    }
}