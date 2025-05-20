using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CengProject.Models;
using CengProject.Data;
using System.ComponentModel.DataAnnotations;

namespace CengProject.Pages.Instructor
{
    public class ClassroomListModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClassroomListModel(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Classroom> Classrooms { get; set; } = new();

        [BindProperty]
        public int FeedbackClassroomId { get; set; }

        [BindProperty]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Comment is required.")]
        public string FeedbackComment { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            Classrooms = await _context.Classrooms.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var instructor = await _userManager.GetUserAsync(User);
            if (instructor == null)
                return RedirectToPage("/Account/Login");

            if (!ModelState.IsValid)
            {
                Classrooms = await _context.Classrooms.ToListAsync();
                return Page();
            }

            var feedback = new Feedback
            {
                ClassroomId = FeedbackClassroomId,
                InstructorId = instructor.Id,
                Rating = Rating,
                Comment = FeedbackComment,
                Date = DateTime.Now
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}