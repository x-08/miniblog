using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miniBlog.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts;
        public Comment Comment { get; set; }

    }
}