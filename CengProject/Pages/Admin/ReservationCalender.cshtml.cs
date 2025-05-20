using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CengProject.Data;
using CengProject.Models;

namespace CengProject.Pages.Admin
{
    public class ReservationCalendarModel : PageModel
    {
        private readonly AppDbContext _context;

        public ReservationCalendarModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Reservation> Reservations { get; set; } = new();

        [BindProperty]
        public int ReservationId { get; set; }

        public async Task OnGetAsync()
        {
            Reservations = await _context.Reservations
                .Include(r => r.Classroom)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostApproveAsync()
        {
            var reservation = await _context.Reservations.FindAsync(ReservationId);
            if (reservation != null)
            {
                reservation.Status = "Approved";
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejectAsync()
        {
            var reservation = await _context.Reservations.FindAsync(ReservationId);
            if (reservation != null)
            {
                reservation.Status = "Rejected";
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var reservation = await _context.Reservations.FindAsync(ReservationId);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
