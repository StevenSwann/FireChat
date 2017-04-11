using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireChatCore
{
    public class MessageLog
    {
        public Nullable<System.DateTime> message_datetime { get; set; }
        public string username { get; set; }
        public string message_text { get; set; }        
    }
}
