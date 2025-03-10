using System.Diagnostics;
using Eventsphere.Areas.Identity.Data;
using Eventsphere.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Eventsphere.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<EventsphereUser> _userManager;
        private readonly EventsphereDBContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<EventsphereUser> userManager, EventsphereDBContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewData["UserID"] = user.Id;
                ViewData["FirstName"] = user.FirstName;
            }

            // Fetch events from the database
            var events = await _context.EventsFormed.ToListAsync();
            return View(events);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
