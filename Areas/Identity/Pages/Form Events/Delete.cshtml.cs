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
    public class DeleteModel : PageModel
    {
        private readonly Eventsphere.Areas.Identity.Data.EventsphereDBContext _context;

        public DeleteModel(Eventsphere.Areas.Identity.Data.EventsphereDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FormEvent FormEvent { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formevent = await _context.EventsFormed.FirstOrDefaultAsync(m => m.Id == id);

            if (formevent == null)
            {
                return NotFound();
            }
            else
            {
                FormEvent = formevent;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formevent = await _context.EventsFormed.FindAsync(id);
            if (formevent != null)
            {
                FormEvent = formevent;
                _context.EventsFormed.Remove(FormEvent);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
