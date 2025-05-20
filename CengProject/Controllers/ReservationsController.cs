using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CengProject.Data;

namespace CengProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservations()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Classroom)
                .Select(r => new
                {
                    id = r.Id,
                    title = r.Classroom.RoomName,
                    start = r.StartTime,
                    end = r.EndTime,
                    classroomId = r.ClassroomId,
                    color = r.Status == "Approved" ? "green" :
                            r.Status == "Rejected" ? "red" : "gold"
                })
                .ToListAsync();

            return Ok(reservations);
        }
    }
}
