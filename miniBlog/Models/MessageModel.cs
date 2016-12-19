using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miniBlog.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Time { get; set; }
        public string Body { get; set; }
    }
}