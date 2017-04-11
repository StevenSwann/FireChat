using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FireChatCore;

namespace WCFMessagesService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class MessageService : IMessageService
    {
        SQLMessageRepository repository;

        public MessageService()
        {
            string connectionString = "Data Source=.;Initial Catalog=FireChat;Integrated Security=True";
            repository = new SQLMessageRepository(connectionString);
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public List<FireChatCore.Message> GetMessages()
        {
            return repository.GetMessages();
        }

        public void AddMessage(FireChatCore.Message message)
        {
            repository.AddMessage(message);
        }

        public FireChatCore.Message GetMessageById(int id)
        {
            return repository.GetMessageById(id);
        }

        public void DeleteMessage(FireChatCore.Message message)
        {
            repository.DeleteMessage(message);
        }
        
        public List<MessageLog> GenerateMessageLog(DateTime fromdatetime)
        {
            return repository.GenerateMessageLog(fromdatetime);
        }
    }
}
