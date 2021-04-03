using MessageAPI.EmailServises;
using MessageAPI.Models;
using MessageAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageContext _context;
        private readonly IEmailService _emailService;

        public MessagesController(MessageContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<MessageViewModel> PostMessage(MessagePostViewModel value)
        {
           var success = await _emailService.SendAsync(value);

            if (value.Carbon_copy_recipients != null)
            {
                foreach (string addres in value.Carbon_copy_recipients)
                {
                    var copy = new MessagePostViewModel
                    {
                        Recipient = addres,
                        Subject = value.Subject,
                        Text = value.Text,
                    };
                    await _emailService.SendAsync(copy);
                }
            }

           var msg = new Message
            {
                Recipient = value.Recipient,
                Subject = value.Subject,
                Text = value.Text,
                Success = success,
            };
            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();

            return new MessageViewModel { 
                Recipient = value.Recipient,
                Subject = value.Subject,
                Text = value.Text,
                Carbon_copy_recipients = value.Carbon_copy_recipients,
                Success = success,
            };
        }
    }
}
