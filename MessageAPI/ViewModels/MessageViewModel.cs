using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MessageAPI.ViewModels
{
    /// <summary>
    /// Модель сообщения для ответа на POST запрос.
    /// </summary>
    public class MessageViewModel
    {
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public IEnumerable<string> Carbon_copy_recipients { get; set; }
        public bool Success { get; set; }
    }
}
