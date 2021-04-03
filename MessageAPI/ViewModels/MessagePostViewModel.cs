using MessageAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MessageAPI.ViewModels
{
    /// <summary>
    /// Модель представления сообщения для POST запроса.
    /// </summary>
    public class MessagePostViewModel
    {
        [Required]
        [StringLength(50)]
        public string Recipient { get; set; }
        [Required]
        [StringLength(100)]
        public string Subject { get; set; }
        [Required]
        [StringLength(1000)]
        public string Text { get; set; }
        public IEnumerable<string> Carbon_copy_recipients { get; set; }
    }
}
