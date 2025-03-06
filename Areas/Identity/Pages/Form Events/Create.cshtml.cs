using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eventsphere.Areas.Identity.Data;
using Eventsphere.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Eventsphere.Areas.Identity.Pages.Form_Events
{
    public class CreateModel : PageModel
    {
        private readonly EventsphereDBContext _context;
        private readonly ILogger<CreateModel> _logger;

        [BindProperty]
        public FormEvent eventData {  get; set; }

        public CreateModel(EventsphereDBContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.

        public IActionResult OnPost()
        {
            byte[] bytes = null;

            if (eventData.PosterImage != null)
            {
                using (Stream fs = eventData.PosterImage.OpenReadStream())
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        bytes = br.ReadBytes((Int32)fs.Length);
                    }
                }
                eventData.Poster = Convert.ToBase64String(bytes, 0, bytes.Length);
            }

            _context.EventsFormed.Add(eventData);
            _context.SaveChanges();

            // Retrieve ticket categories and amounts
            var ticketCategories = Request.Form["ticketCategories[]"].ToArray();
            var amounts = Request.Form["amounts[]"].ToArray();

            if (ticketCategories.Length != amounts.Length || ticketCategories.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Please enter at least one ticket category.");
                return Page();
            }

            // Create and save ticket details
            List<TicketDetail> ticketDetails = new();
            for (int i = 0; i < ticketCategories.Length; i++)
            {
                ticketDetails.Add(new TicketDetail
                {
                    Id = eventData.Id, // Link ticket to the created event
                    Category = ticketCategories[i],
                    Amount = decimal.Parse(amounts[i])
                });
            }

            _context.TicketDetails.AddRange(ticketDetails);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
