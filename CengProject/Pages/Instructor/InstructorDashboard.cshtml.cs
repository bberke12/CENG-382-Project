using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CengProject.Models;

namespace CengProject.Pages.Instructor
{
    [Authorize(Roles = "Instructor")]
    public class InstructorDashboardModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public InstructorDashboardModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string UserEmail { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            UserEmail = user?.Email ?? "Unknown";
        }
    }
}
