using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CengProject.Data;
using CengProject.Models;

namespace CengProject.Pages_Admin_ManageTerm
{
    public class DetailsModel : PageModel
    {
        private readonly CengProject.Data.AppDbContext _context;

        public DetailsModel(CengProject.Data.AppDbContext context)
        {
            _context = context;
        }

        public Term Term { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term = await _context.Terms.FirstOrDefaultAsync(m => m.TermId == id);

            if (term is not null)
            {
                Term = term;

                return Page();
            }

            return NotFound();
        }
    }
}
