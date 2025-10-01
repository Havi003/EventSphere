using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eventsphere.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Eventsphere.Models
{
    public class FormEvent
    {
        [Key]
        public int FormEventId { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public string Location { get; set; }

        public string Poster { get; set; }

        [NotMapped]
        public IFormFile PosterImage { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public string About { get; set; }

        // Track the user who created the event
        [Required]
        [BindNever]
        public string CreatedBy { get; set; } // Store the UserId

        [ForeignKey("CreatedBy")]
        public virtual EventsphereUser Creator { get; set; } // Navigation property

        // Navigation property - An event can have multiple tickets
        public List<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();
    }
}
