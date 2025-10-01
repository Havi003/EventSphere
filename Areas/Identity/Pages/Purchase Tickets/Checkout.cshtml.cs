using Eventsphere.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eventsphere.Areas.Identity.Pages.Purchase_Tickets
{
    public class CheckoutModel : PageModel
    {
        private readonly UserManager<EventsphereUser> _userManager;

        public CheckoutModel(UserManager<EventsphereUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public string EventName { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public decimal TotalAmount { get; set; }

        [BindProperty(SupportsGet = true)]
        public int TicketQuantity { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TicketCategory { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal TicketAmount { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string eventName, decimal total, int quantity, string category, decimal amount)
        {
            eventName = EventName;
            TotalAmount = total;
            TicketQuantity = quantity;
            TicketCategory = category;
            TicketAmount = amount;

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                FirstName = user.FirstName;  // Ensure ApplicationUser has FirstName
                LastName = user.LastName;    // Ensure ApplicationUser has LastName
                Email = user.Email;
            }

            return Page();
        }
    }
}
