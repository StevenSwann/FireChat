using FireChatCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF2ndFireChat
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {

        SQLUserRepository repository;

        public UserService()
        {
            string connectionString = "Data Source=.;Initial Catalog=FireChat;Integrated Security=True";
            repository = new SQLUserRepository(connectionString);
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
        
        public List<FireChatCore.User> GetUsers()
        {
            return repository.GetUsers();                        
        }

        public void AddUser(FireChatCore.User user)
        {
            repository.AddUser(user);
        }

        public FireChatCore.User GetUserById(int id)
        {
            return repository.GetUserById(id);
        }

        public FireChatCore.User GetUserByUsername(string username)
        {
            return repository.GetUserByUsername(username);
        }

        public void DeleteUser(FireChatCore.User user)
        {
            repository.DeleteUser(user);
        }

        public void EditUser(FireChatCore.User user)
        {
            repository.EditUser(user);
        }

        public bool UserExists(FireChatCore.User user)
        {
            return repository.UserExists(user);
        }

        public bool EmailExists(FireChatCore.User user)
        {
            return repository.EmailExists(user);
        }

        public bool PasswordMatch(FireChatCore.User user)
        {
            return repository.PasswordMatch(user);
        }

        public bool IsAdmin(FireChatCore.User user)
        {
            return repository.IsAdmin(user);
        }
    }
}
