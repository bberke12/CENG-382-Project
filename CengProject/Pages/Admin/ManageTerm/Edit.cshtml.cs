using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CengProject.Data;
using CengProject.Models;

namespace CengProject.Pages_Admin_ManageTerm
{
    public class EditModel : PageModel
    {
        private readonly CengProject.Data.AppDbContext _context;

        public EditModel(CengProject.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Term Term { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term =  await _context.Terms.FirstOrDefaultAsync(m => m.TermId == id);
            if (term == null)
            {
                return NotFound();
            }
            Term = term;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Term).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TermExists(Term.TermId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TermExists(int id)
        {
            return _context.Terms.Any(e => e.TermId == id);
        }
    }
}
