using MessageAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageAPI.Repository
{
    /// <summary>
    /// Описывает функционал по работе с СУБД.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Асинхронно записывает сообщение в БД.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task AddAsync(Message item);
        /// <summary>
        /// Асинхронно запрашивает все сообщения из БД.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Message>> GetAllAsync();
    }
}
