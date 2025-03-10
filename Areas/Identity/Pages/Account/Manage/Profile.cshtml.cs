using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Eventsphere.Models;
using System.Threading.Tasks;
using Eventsphere.Areas.Identity.Data;

namespace Eventsphere.Areas.Identity.Pages.Account.Manage
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<EventsphereUser> _userManager;

        public ProfileModel(UserManager<EventsphereUser> userManager)
        {
            _userManager = userManager;
        }

        public EventsphereUser CurrentUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);
            if (CurrentUser == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
