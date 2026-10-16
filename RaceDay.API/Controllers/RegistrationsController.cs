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
    public class RegistrationsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public RegistrationsController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpPost("{id}/register")]
        [Authorize(Roles = "Participant")]
        public async Task<IActionResult> RegisterForEvent(int id, [FromBody] RegisterForEventDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var eventExists = await _context.Events.AnyAsync(e => e.EventId == id);
            if (!eventExists)
            {
                return NotFound(new { message = "Event not found." });
            }

            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == dto.CategoryId);
            if (!categoryExists)
            {
                return BadRequest(new { message = "Invalid category." });
            }

            var isAlreadyRegistered = await _context.Registrations
                .AnyAsync(r => r.EventId == id && r.UserId == userId);

            if (isAlreadyRegistered)
            {
                return Conflict(new { message = "Already registered for this event." });
            }

            var registration = new Registration
            {
                UserId = userId,
                EventId = id,
                CategoryId = dto.CategoryId,
                Status = "Confirmed"
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FindAsync(userId);
            var ev = await _context.Events.FindAsync(id);
            var cat = await _context.Categories.FindAsync(dto.CategoryId);

            return StatusCode(201, new RegistrationResponseDto
            {
                RegistrationId = registration.RegistrationId,
                UserId = userId,
                UserName = user?.Name ?? string.Empty,
                EventId = id,
                EventName = ev?.Name ?? string.Empty,
                CategoryId = dto.CategoryId,
                CategoryName = cat?.Name ?? string.Empty,
                Status = registration.Status
            });
        }

        [HttpGet("{id}/registrations")]
        [Authorize(Roles = "Organiser")]
        public async Task<IActionResult> GetEventRegistrations(int id)
        {
            var registrations = await _context.Registrations
                .Include(r => r.User)
                .Include(r => r.Event)
                .Include(r => r.Category)
                .Where(r => r.EventId == id)
                .Select(r => new RegistrationResponseDto
                {
                    RegistrationId = r.RegistrationId,
                    UserId = r.UserId,
                    UserName = r.User != null ? r.User.Name : string.Empty,
                    EventId = r.EventId,
                    EventName = r.Event != null ? r.Event.Name : string.Empty,
                    CategoryId = r.CategoryId,
                    CategoryName = r.Category != null ? r.Category.Name : string.Empty,
                    Status = r.Status
                })
                .ToListAsync();

            return Ok(registrations);
        }
    }
}