using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireChatCore
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void EditUser(User user);
        void DeleteUser(User user);
        bool UserExists(User user);
        bool EmailExists(User user);
        bool IsAdmin(User user);
        bool PasswordMatch(User user);
        List<User> GetUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);

    }
}
