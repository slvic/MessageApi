using MessageAPI.Models;
using MessageAPI.ViewModels;
using System.Threading.Tasks;

namespace MessageAPI.EmailServises
{
    /// <summary>
    /// Описывает функии по работе с отправкой сообщений по Email
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Отправляет сообщение по Email
        /// </summary>
        /// <param name="emailMessage"></param>
        Task<bool> SendAsync(MessagePostViewModel emailMessage);
    }
}
