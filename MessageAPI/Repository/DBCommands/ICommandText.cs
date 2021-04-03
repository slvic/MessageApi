using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageAPI.Repository.DBCommands
{
    /// <summary>
    /// Описывает переменные, хранящие в себе SQL запросы к СУБД.
    /// </summary>
    public interface ICommandText
    {
        string AddMessage { get; }
        string GetAllMessages { get; }
    }
}
