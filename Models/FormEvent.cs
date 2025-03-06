using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventsphere.Models
{
    public class FormEvent
    {
        [Key]
        public int Id { get; set; }

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

        // Navigation property - An event can have multiple tickets
        public List<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();
    }
}
