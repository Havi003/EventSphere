using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Eventsphere.Areas.Identity.Data;
using Eventsphere.Models;

namespace Eventsphere.Areas.Identity.Pages.Form_Events
{
    public class IndexModel : PageModel
    {
        private readonly Eventsphere.Areas.Identity.Data.EventsphereDBContext _context;

        public IndexModel(Eventsphere.Areas.Identity.Data.EventsphereDBContext context)
        {
            _context = context;
        }

        public IList<FormEvent> FormEvent { get;set; } = default!;

        public async Task OnGetAsync()
        {
            FormEvent = await _context.EventsFormed.ToListAsync();
        }
    }
}
