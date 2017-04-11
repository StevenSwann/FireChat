using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireChatCore
{
    public class Message
        {
            public int message_id { get; set; }
            public int message_user_id { get; set; }
            public int message_chat_id { get; set; }
            public string message_text { get; set; }
            public Nullable<System.DateTime> message_datetime { get; set; }

            public virtual Chat chat { get; set; }
            public virtual User user { get; set; }
        }
}

