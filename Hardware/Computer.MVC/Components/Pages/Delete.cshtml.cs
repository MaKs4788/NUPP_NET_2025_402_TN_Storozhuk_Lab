using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Setup.Infrastructure;
using Setup.Infrastructure.Models;

namespace Computer.MVC.Components.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly Setup.Infrastructure.SetupContext _context;

        public DeleteModel(Setup.Infrastructure.SetupContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ComputerModel ComputerModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computermodel = await _context.Computers.FirstOrDefaultAsync(m => m.Id == id);

            if (computermodel == null)
            {
                return NotFound();
            }
            else
            {
                ComputerModel = computermodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computermodel = await _context.Computers.FindAsync(id);
            if (computermodel != null)
            {
                ComputerModel = computermodel;
                _context.Computers.Remove(ComputerModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
