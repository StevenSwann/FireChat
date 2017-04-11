using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireChatCore
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage (Message message);
        List<Message> GetMessages();
        Message GetMessageById(int id);
        Message GetMessageByUsername(string username);
        Message GetMessageByDate(Nullable<System.DateTime> date);
        List<MessageLog> GenerateMessageLog(DateTime fromdatetime);

    }
}
