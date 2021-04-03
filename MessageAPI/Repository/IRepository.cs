using MessageAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageAPI.Repository
{
    public interface IRepository
    {
        Task AddAsync(Message item);
        Task<IEnumerable<Message>> GetAllAsync();
    }
}
