using MessageAPI.ViewModels;
using System.Threading.Tasks;

namespace MessageAPI.EmailServises
{
    /// <summary>
    /// Описывает функионал по работе с отправкой сообщений по Email.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Асинхронно отправляет сообщение по Email.
        /// </summary>
        /// <param name="emailMessage"></param>
        Task<bool> SendAsync(MessagePostViewModel emailMessage);
    }
}
