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
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge(); // Redirect to login if the user is not authenticated
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError(string.Empty, "User information is missing. Please log in again.");
                return Page();
            }

            List<TicketDetail> ticketDetails = new();

            byte[] bytes = null;

            if (eventData.PosterImage != null)
            {
                using (Stream fs = eventData.PosterImage.OpenReadStream())
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        bytes = br.ReadBytes((int)fs.Length);
                    }
                }
                eventData.Poster = Convert.ToBase64String(bytes);
            }

            _logger.LogInformation("User ID Retrieved: {UserId}", userId);

            // **Assign the CreatedBy field to the current user's ID**
            eventData.CreatedBy = userId;

            // **Save the event first to generate FormEventId**
            _context.EventsFormed.Add(eventData);
            _context.SaveChanges();  // Ensures FormEventId is generated

            // **Now that eventData.FormEventId is set, create ticket details**
            var ticketCategories = Request.Form["ticketCategories[]"].ToArray();
            var amounts = Request.Form["amounts[]"].ToArray();

            if (ticketCategories.Length == 0 || amounts.Length == 0)
            {
                // No input provided → Store "FREE" ticket
                ticketDetails.Add(new TicketDetail
                {
                    FormEventId = eventData.FormEventId, // Use the now-existing ID
                    Category = "FREE",
                    Amount = 0
                });
            }
            else
            {
                for (int i = 0; i < ticketCategories.Length; i++)
                {
                    string category = string.IsNullOrWhiteSpace(ticketCategories[i]) ? "FREE" : ticketCategories[i];
                    decimal amount = string.IsNullOrWhiteSpace(amounts[i]) ? 0 : decimal.Parse(amounts[i]);

                    ticketDetails.Add(new TicketDetail
                    {
                        FormEventId = eventData.FormEventId, // Use the now-existing ID
                        Category = category,
                        Amount = amount
                    });
                }
            }

            // **Now add ticket details after ensuring FormEventId exists**
            _context.TicketDetails.AddRange(ticketDetails);
            _context.SaveChanges();  // Save ticket details

            return RedirectToPage("Details", new { id = eventData.FormEventId });
        }


    }
}
