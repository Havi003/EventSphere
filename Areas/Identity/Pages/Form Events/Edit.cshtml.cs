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

        public List<TicketDetail> TicketDetails { get; set; } = new();

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

            // Get ticket categories for this event
            TicketDetails = await _context.TicketDetails
                .Where(t => t.Id == id)
                .ToListAsync();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingEvent = _context.EventsFormed.Find(FormEvent.Id);

            if (existingEvent == null)
            {
                return NotFound();
            }

            // Update fields
            existingEvent.EventName = FormEvent.EventName;
            existingEvent.Location = FormEvent.Location;
            existingEvent.EventDate = FormEvent.EventDate;
            existingEvent.StartTime = FormEvent.StartTime;
            existingEvent.EndTime = FormEvent.EndTime;
            existingEvent.About = FormEvent.About;

            try
            {
                // If a new image is uploaded, update it
                if (FormEvent.PosterImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        FormEvent.PosterImage.CopyTo(memoryStream);
                        byte[] imageBytes = memoryStream.ToArray();
                        existingEvent.Poster = Convert.ToBase64String(imageBytes);
                    }
                }

                _context.Update(existingEvent);
                _context.SaveChanges();
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
