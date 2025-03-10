using Eventsphere.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class TicketDetail
{
    [Key]
    public int TicketId { get; set; }  // Primary Key

    public string Category { get; set; }

    [Column(TypeName = "decimal(18,2)")] // Specify precision for Amount
    public decimal Amount { get; set; }

    // Foreign Key - Links to FormEvent
    public int FormEventId { get; set; }

    [ForeignKey("FormEventId")]
    public FormEvent Event { get; set; }
}
