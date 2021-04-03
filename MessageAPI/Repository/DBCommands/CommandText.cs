namespace MessageAPI.Repository.DBCommands
{
    public class CommandText:ICommandText
    {
        public string AddMessage => "INSERT INTO messages(recipient, subject, text, success) VALUES(@recipient, @subject, @text, @success)";
        public string GetAllMessages => "SELECT * FROM messages";
    }
}
