using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CengProject.Data;
using CengProject.Models;

namespace CengProject.Pages_Admin_Classrooms
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public Classroom Classroom { get; set; } = default!;
        public List<Feedback> Feedbacks { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Classrooms == null)
                return NotFound();

            var classroom = await _context.Classrooms
                .FirstOrDefaultAsync(m => m.ClassroomId == id);

            if (classroom == null)
                return NotFound();

            Classroom = classroom;

            Feedbacks = await _context.Feedbacks
                .Where(f => f.ClassroomId == id)
                .ToListAsync();

            return Page();
        }
    }
}
