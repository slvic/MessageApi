using System.ComponentModel.DataAnnotations;

namespace MessageAPI.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Recipient { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public bool Success { get; set; }
    }
}
