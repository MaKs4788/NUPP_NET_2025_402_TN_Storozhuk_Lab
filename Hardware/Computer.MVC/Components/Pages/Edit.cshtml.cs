using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Setup.Infrastructure;
using Setup.Infrastructure.Models;

namespace Computer.MVC.Components.Pages
{
    public class EditModel : PageModel
    {
        private readonly Setup.Infrastructure.SetupContext _context;

        public EditModel(Setup.Infrastructure.SetupContext context)
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

            var computermodel =  await _context.Computers.FirstOrDefaultAsync(m => m.Id == id);
            if (computermodel == null)
            {
                return NotFound();
            }
            ComputerModel = computermodel;
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

            _context.Attach(ComputerModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerModelExists(ComputerModel.Id))
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

        private bool ComputerModelExists(Guid id)
        {
            return _context.Computers.Any(e => e.Id == id);
        }
    }
}
