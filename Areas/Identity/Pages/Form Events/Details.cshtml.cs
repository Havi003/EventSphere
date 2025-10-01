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

        [BindProperty]
        public int TicketQuantity { get; set; }//The amount of tickets purchased by the user

        [BindProperty]
        public int TicketType { get; set; }//The category of ticket purchased by user

        public decimal TotalAmount { get; set; }//Amount of tickets multiplied by category to get the total

        //Retreiving details from the database 
        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var formevent = await _context.EventsFormed.FirstOrDefaultAsync(m => m.FormEventId == id);
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
                .Where(t => t.FormEventId == id)
                .ToListAsync();

            return Page();
        }

        //Send information to purchase tickets page
        public string TicketCategory { get; set; }

        public decimal TicketAmount { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Retrieve the selected ticket's price from the database
            var ticket = await _context.TicketDetails
                .FirstOrDefaultAsync(t => t.TicketId == TicketType);

            if (ticket == null)
            {
                return NotFound();
            }

            var eventDetails = await _context.EventsFormed.FirstOrDefaultAsync(e => e.FormEventId == ticket.FormEventId);

            // Calculate the total amount
            TotalAmount = ticket.Amount * TicketQuantity;

            TicketCategory = ticket.Category;

            TicketAmount = ticket.Amount;

            // Redirect to checkout page with total amount
            return RedirectToPage("Checkout", new { 
                eventName = eventDetails.EventName, 
                total = TotalAmount, 
                quantity = TicketQuantity,
                category = TicketCategory, 
                amount = TicketAmount });
        }

    }
}
