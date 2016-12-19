using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace miniBlog.Models
{
    public class MessageHelperModel
    {
        public IEnumerable<MessageModel> MessageList { get; set; }
        public MessageModel Messages { get; set; }
    }
}