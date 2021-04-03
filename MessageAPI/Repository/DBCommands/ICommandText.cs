using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageAPI.Repository.DBCommands
{
    public interface ICommandText
    {
        string AddMessage { get; }
        string GetAllMessages { get; }
    }
}
