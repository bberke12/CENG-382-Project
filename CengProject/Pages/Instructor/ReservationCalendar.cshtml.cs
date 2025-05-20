using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CengProject.Data;
using CengProject.Models;

namespace CengProject.Pages.Instructor
{
    public class ReservationCalendarModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationCalendarModel(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Classroom> Classrooms { get; set; } = new();

        [BindProperty] public int ReservationId { get; set; }
        [BindProperty] public int ClassroomId { get; set; }
        [BindProperty] public string SelectedDate { get; set; } = string.Empty;
        [BindProperty] public string StartTime { get; set; } = string.Empty;
        [BindProperty] public string EndTime { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            Classrooms = await _context.Classrooms.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var instructor = await _userManager.GetUserAsync(User);
            if (instructor == null)
                return RedirectToPage("/Account/Login");

            var term = await _context.Terms.FirstOrDefaultAsync();
            if (term == null)
            {
                ModelState.AddModelError(string.Empty, "No active term found.");
                Classrooms = await _context.Classrooms.ToListAsync();
                return Page();
            }

            if (!DateTime.TryParse($"{SelectedDate} {StartTime}", out var start) ||
                !DateTime.TryParse($"{SelectedDate} {EndTime}", out var end))
            {
                ModelState.AddModelError(string.Empty, "Invalid date or time.");
                Classrooms = await _context.Classrooms.ToListAsync();
                return Page();
            }

            if (ReservationId == 0)
            {
                var newReservation = new Reservation
                {
                    InstructorId = instructor.Id,
                    ClassroomId = ClassroomId,
                    TermId = term.TermId,
                    StartTime = start,
                    EndTime = end,
                    Status = "Pending",
                    IsCancelled = false,
                    CreatedAt = DateTime.Now
                };
                _context.Reservations.Add(newReservation);
            }
            else
            {
                var existing = await _context.Reservations.FindAsync(ReservationId);
                if (existing != null && existing.InstructorId == instructor.Id)
                {
                    existing.ClassroomId = ClassroomId;
                    existing.StartTime = start;
                    existing.EndTime = end;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<JsonResult> OnGetReservationsAsync()
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
                    status = r.Status
                }).ToListAsync();

            return new JsonResult(reservations);
        }
    }
}
