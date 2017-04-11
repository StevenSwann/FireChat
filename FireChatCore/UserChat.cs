using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireChatCore
{
    public class UserChat
    {
        public int user_chat_id { get; set; }
        public int user_chat_user_id { get; set; }
        public int user_chat_chat_id { get; set; }

        public virtual Chat chat { get; set; }
        public virtual User user { get; set; }
    }
}
