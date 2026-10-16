using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.DTOs;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EventsController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events
                .Include(e => e.Organizer)
                .Select(e => new EventDto
                {
                    EventId = e.EventId,
                    Name = e.Name,
                    Description = e.Description,
                    Date = e.Date,
                    Location = e.Location,
                    Distance = e.Distance,
                    EventType = e.EventType,
                    OrganizerId = e.OrganizerId,
                    OrganizerName = e.Organizer != null ? e.Organizer.Name : string.Empty
                })
                .ToListAsync();

            return Ok(events);
        }

        [HttpPost]
        [Authorize(Roles = "Organiser")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto dto)
        {
            var organizerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var newEvent = new Event
            {
                Name = dto.Name,
                Description = dto.Description,
                Date = dto.Date,
                Location = dto.Location,
                Distance = dto.Distance,
                EventType = dto.EventType,
                OrganizerId = organizerId
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return StatusCode(201, new EventDto
            {
                EventId = newEvent.EventId,
                Name = newEvent.Name,
                Description = newEvent.Description,
                Date = newEvent.Date,
                Location = newEvent.Location,
                Distance = newEvent.Distance,
                EventType = newEvent.EventType,
                OrganizerId = organizerId,
                OrganizerName = User.FindFirstValue(ClaimTypes.Name) ?? ""
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Organiser")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] CreateEventDto dto)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
            {
                return NotFound(new { message = "Event missing" });
            }

            existingEvent.Name = dto.Name;
            existingEvent.Description = dto.Description;
            existingEvent.Date = dto.Date;
            existingEvent.Location = dto.Location;
            existingEvent.Distance = dto.Distance;
            existingEvent.EventType = dto.EventType;

            await _context.SaveChangesAsync();
            return Ok(existingEvent);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Organiser")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
            {
                return NotFound(new { message = "Event missing" });
            }

            _context.Events.Remove(existingEvent);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Event deleted successfully." });
        }
    }
}