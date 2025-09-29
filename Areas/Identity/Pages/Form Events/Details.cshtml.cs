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
    public class DetailsModel : PageModel
    {
        private readonly Eventsphere.Areas.Identity.Data.EventsphereDBContext _context;

        public DetailsModel(Eventsphere.Areas.Identity.Data.EventsphereDBContext context)
        {
            _context = context;
        }

        public FormEvent FormEvent { get; set; } = default!;

        public List<TicketDetail> TicketDetails { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var formevent = await _context.EventsFormed.FirstOrDefaultAsync(m => m.Id == id);
            if (formevent == null)
            {
                return NotFound();
            }
            else
            {
                FormEvent = formevent;
            }

            // Get ticket categories for this event
            TicketDetails = await _context.TicketDetails
                .Where(t => t.TicketId== id)
                .ToListAsync();

            return Page();
        }
    }
}
