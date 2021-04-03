using Dapper;
using MessageAPI.Models;
using MessageAPI.Repository.DBCommands;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageAPI.Repository
{
    /// <summary>
    /// Предоставляет функционал для работы с СУБД.
    /// </summary>
    public class MessageRepository : BaseRepository, IRepository
    {
        private readonly ICommandText _commandText;
        /// <summary>
        /// Получает параметры из конфигурационного файла для работы с СУБД.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="commandText"></param>
        public MessageRepository(IConfiguration configuration, ICommandText commandText) 
            : base(configuration, new NpgsqlConnection(configuration["DBInfo:ConnectionString"]))
        {
            _commandText = commandText;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await WithConnection(async conn =>
            {
                var query = await conn.QueryAsync<Message>(_commandText.GetAllMessages);
                return query;
            });
        }

        public async Task AddAsync(Message item)
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.AddMessage,
                    new { Recipient = item.Recipient, Subject = item.Subject, Text = item.Text, Success = item.Success });
            });
        }
    }
}
