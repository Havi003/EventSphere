using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Eventsphere.Models
{
    public class TicketDetail
    {
        [Key]
        public int TicketId { get; set; }  // Primary Key

        public string Category { get; set; }
        public decimal Amount { get; set; }

        // Foreign Key - Links to FormEvent
        public int Id { get; set; }
        [ForeignKey("Id")]
        public FormEvent Event { get; set; }
    }
}
