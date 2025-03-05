using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eventsphere.Areas.Identity.Data;
using Eventsphere.Models;

namespace Eventsphere.Areas.Identity.Pages.Form_Events
{
    public class EditModel : PageModel
    {
        private readonly Eventsphere.Areas.Identity.Data.EventsphereDBContext _context;

        public EditModel(Eventsphere.Areas.Identity.Data.EventsphereDBContext context)
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

            var formevent =  await _context.EventsFormed.FirstOrDefaultAsync(m => m.Id == id);
            if (formevent == null)
            {
                return NotFound();
            }
            FormEvent = formevent;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FormEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormEventExists(FormEvent.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FormEventExists(int id)
        {
            return _context.EventsFormed.Any(e => e.Id == id);
        }
    }
}
