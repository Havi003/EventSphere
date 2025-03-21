using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Eventsphere.Areas.Identity.Pages.Purchase_Tickets
{
    public class PurchaseTicketsModel : PageModel
    {
        private readonly Data.EventsphereDBContext _context;

        public PurchaseTicketsModel(Data.EventsphereDBContext context)
        {
            _context = context;
        }

        public List<TicketDetail> TicketDetails { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get ticket categories for this event
            TicketDetails = await _context.TicketDetails
                .Where(t => t.TicketId == id)
                .ToListAsync();

            return Page();
        }
    }
}
