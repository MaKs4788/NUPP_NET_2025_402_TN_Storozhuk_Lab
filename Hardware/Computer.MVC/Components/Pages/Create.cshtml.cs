using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Setup.Infrastructure;
using Setup.Infrastructure.Models;

namespace Computer.MVC.Components.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Setup.Infrastructure.SetupContext _context;

        public CreateModel(Setup.Infrastructure.SetupContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ComputerModel ComputerModel { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Computers.Add(ComputerModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
