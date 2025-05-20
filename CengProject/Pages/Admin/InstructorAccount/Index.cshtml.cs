using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using CengProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CengProject.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class InstructorAccountManagerModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public InstructorAccountManagerModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public List<ApplicationUser> Instructors { get; set; } = new();

        [BindProperty]
        public string NewInstructorEmail { get; set; } = string.Empty;

        [BindProperty]
        public string NewInstructorPassword { get; set; } = string.Empty;

        [BindProperty]
        public string InstructorId { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            Instructors = (await _userManager.GetUsersInRoleAsync("Instructor")).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            var user = new ApplicationUser
            {
                UserName = NewInstructorEmail,
                Email = NewInstructorEmail,
                IsActive = true,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, NewInstructorPassword);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Instructor");
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeactivateAsync()
        {
            var user = await _userManager.FindByIdAsync(InstructorId);
            if (user != null)
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);
            }

            return RedirectToPage();
        }
    }
}
