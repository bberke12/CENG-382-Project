using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CengProject.Data;
using CengProject.Models;

namespace CengProject.Pages.Instructor
{
    public class InstructorReservationsModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public InstructorReservationsModel(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Reservation> MyReservations { get; set; } = new();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                MyReservations = await _context.Reservations
                    .Include(r => r.Classroom)
                    .Where(r => r.InstructorId == user.Id)
                    .OrderByDescending(r => r.StartTime)
                    .ToListAsync();
            }
        }
    }
}
