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
    public class IndexModel : PageModel
    {
        private readonly CengProject.Data.AppDbContext _context;

        public IndexModel(CengProject.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Term> Term { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Term = await _context.Terms.ToListAsync();
        }
    }
}
