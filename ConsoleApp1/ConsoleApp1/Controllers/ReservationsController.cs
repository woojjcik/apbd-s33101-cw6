using ConsoleApp1.Modules;
using ConsoleApp1.Data;
using ConsoleApp1.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApp1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetReservations([FromQuery] DateOnly? date, [FromQuery] string? status, [FromQuery] int? roomId)
    {
        var query = Database.Reservations.AsQueryable();
        
        if (date.HasValue) query = query.Where(r => r.Date == date);
        if (roomId.HasValue) query = query.Where(r => r.RoomId == roomId);
        if (!string.IsNullOrEmpty(status)) query = query.Where(r => r.Status == status);

        return Ok(query.ToList());
    }

    [HttpPost]
    public IActionResult AddReservation([FromBody] Reservation reservation)
    {
        var room = Database.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

        if (room == null) return NotFound("room doesnt exist");
        if (!room.IsActive) return BadRequest("room is inactive");
        
        bool overlap = Database.Reservations.Any(r => 
            r.RoomId == reservation.RoomId && 
            r.Date == reservation.Date &&
            r.Status != "cancelled" &&
            reservation.StartTime < r.EndTime && r.StartTime < reservation.EndTime);

        if (overlap) return Conflict("room is reserved");

        reservation.Id = Database.Reservations.Count > 0 ? Database.Reservations.Max(r => r.Id) + 1 : 1;
        Database.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetReservations), new { roomId = reservation.RoomId }, reservation);
    }
}