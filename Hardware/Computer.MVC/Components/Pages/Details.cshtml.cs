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
    public class DetailsModel : PageModel
    {
        private readonly Setup.Infrastructure.SetupContext _context;

        public DetailsModel(Setup.Infrastructure.SetupContext context)
        {
            _context = context;
        }

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
    }
}
