using MessageAPI.EmailServises;
using MessageAPI.Models;
using MessageAPI.Repository;
using MessageAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MessageAPI.Controllers
{
    /// <summary>
    /// Контроллер для сообщений.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IRepository _messageRepository;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Получает параметры из конфигурацонного файла для работы с сообщениями.
        /// </summary>
        /// <param name="messageRepository"></param>
        /// <param name="emailService"></param>
        public MessagesController(IRepository messageRepository, IEmailService emailService)
        {
            _messageRepository = messageRepository;
            _emailService = emailService;
        }

        /// <summary>
        /// Обрабатывает GET запрос - возвращает все сообщения из БД.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Message>> GetAllMessages()
        {
            var messages = await _messageRepository.GetAllAsync();
            return Ok(messages);
        }

        /// <summary>
        /// Обрабатывает POST запрос - отправляет сообщения по Email и сохраняет их в БД.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageViewModel> PostMessage(MessagePostViewModel value)
        {
            var success = await _emailService.SendAsync(value);

            var msg = new Message
            {
                Recipient = value.Recipient,
                Subject = value.Subject,
                Text = value.Text,
                Success = success,
            };
            await _messageRepository.AddAsync(msg);
            if (value.Carbon_copy_recipients != null)
            {
                foreach (string addr in value.Carbon_copy_recipients)
                {
                    await _messageRepository.AddAsync(
                        new Message
                        {
                            Recipient = addr,
                            Subject = value.Subject,
                            Text = value.Text,
                            Success = success,
                        });
                }
            }

            return new MessageViewModel
            {
                Recipient = value.Recipient,
                Subject = value.Subject,
                Text = value.Text,
                Carbon_copy_recipients = value.Carbon_copy_recipients,
                Success = success,
            };
        }
    }
}
