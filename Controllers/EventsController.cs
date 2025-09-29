using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Eventsphere.Areas.Identity.Data; // Your DbContext namespace
using Eventsphere.Models; // Your Event model namespace

namespace Eventsphere.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventsphereDBContext _context;

        public EventsController(EventsphereDBContext context)
        {
            _context = context;
        }

        // Load Events Page
        public IActionResult Index()
        {
            var events = _context.EventsFormed.ToList(); // Fetch all events
            return View(events);
        }

        // Search Events - AJAX Call
        [HttpGet]
        public IActionResult SearchEvents(string query)
        {
            var filteredEvents = _context.EventsFormed
                .Where(e => e.EventName.Contains(query) || e.Location.Contains(query))
                .ToList();

            return PartialView("_EventListPartial", filteredEvents);
        }
    }
}
