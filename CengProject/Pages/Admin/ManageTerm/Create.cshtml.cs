using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CengProject.Data;
using CengProject.Models;

namespace CengProject.Pages_Admin_ManageTerm
{
    public class CreateModel : PageModel
    {
        private readonly CengProject.Data.AppDbContext _context;

        public CreateModel(CengProject.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Term Term { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Terms.Add(Term);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
