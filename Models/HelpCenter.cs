using System.ComponentModel.DataAnnotations;

namespace Eventsphere.Models
{
    public class HelpCenter
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string question { get; set; }
        [Required]
        public string answer { get; set; }
    }
}
