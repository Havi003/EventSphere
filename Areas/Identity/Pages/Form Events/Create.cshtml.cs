﻿using System;
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
            byte[] bytes = null;

            if (eventData.PosterImage != null)
            {
                using (Stream fs = eventData.PosterImage.OpenReadStream())
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        bytes = br.ReadBytes((Int32)fs.Length);
                    }
                }
                eventData.Poster = Convert.ToBase64String(bytes, 0, bytes.Length);
            }

            _context.EventsFormed.Add(eventData);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
