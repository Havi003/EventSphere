using Eventsphere.Areas.Identity.Data;
using Eventsphere.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Eventsphere.Areas.Identity.Pages
{
    public class HelpCenterModel : PageModel
    {
        private readonly EventsphereDBContext _context;

        public HelpCenterModel(EventsphereDBContext context)
        {
            _context = context;
        }

        public IList<HelpCenter> HelpCenterList { get; set; }

        public async Task OnGetAsync()
        {
            HelpCenterList = await _context.HelpCenter.ToListAsync();//Fetches Help Center Records
        }
        
    }
}
