using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageAPI.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public bool Success { get; set; }
    }
}
