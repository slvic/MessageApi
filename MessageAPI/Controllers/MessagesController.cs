using MessageAPI.EmailServises;
using MessageAPI.Models;
using MessageAPI.Repository;
using MessageAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MessageAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        //private readonly MessageContext _context;
        private readonly IRepository _messageRepository;
        private readonly IEmailService _emailService;

        public MessagesController(IRepository messageRepository, IEmailService emailService)
        {
            _messageRepository = messageRepository;
            _emailService = emailService;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<Message>> GetAllMessages()
        {
            var messages = await _messageRepository.GetAllAsync();
            return Ok(messages);
        }

        // POST: api/Messages
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
            await _messageRepository.AddAsync(msg);

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
