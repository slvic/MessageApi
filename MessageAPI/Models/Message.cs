using System.ComponentModel.DataAnnotations;

namespace MessageAPI.Models
{
    /// <summary>
    /// Модель сообщения, которая будет храниться в БД.
    /// </summary>
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
