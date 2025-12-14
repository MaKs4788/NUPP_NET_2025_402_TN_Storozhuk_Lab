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
    public class IndexModel : PageModel
    {
        private readonly Setup.Infrastructure.SetupContext _context;

        public IndexModel(Setup.Infrastructure.SetupContext context)
        {
            _context = context;
        }

        public IList<ComputerModel> ComputerModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ComputerModel = await _context.Computers.ToListAsync();
        }
    }
}
