using System.Diagnostics;
using Eventsphere.Areas.Identity.Data;
using Eventsphere.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
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

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewData["UserID"] = user.Id;
                ViewData["FirstName"] = user.FirstName;
            }

            // Fetch paginated events
            var events = await _context.EventsFormed
                .AsNoTracking()
                
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalEvents = await _context.EventsFormed.CountAsync();
            var totalPages = (int)Math.Ceiling(totalEvents / (double)pageSize);

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

            _logger.LogInformation($"Total Events: {totalEvents}, Page {pageNumber}/{totalPages}, Fetched {events.Count} events");

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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SearchSuggestions(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Json(new string[0]);

            var suggestions = await _context.EventsFormed
                .Where(e => e.EventName.StartsWith(term))
                .OrderBy(e => e.EventName)
                .Select(e => e.EventName)
                .Distinct()
                .Take(10)
                .ToListAsync();

            return Json(suggestions);
        }
    }
}
