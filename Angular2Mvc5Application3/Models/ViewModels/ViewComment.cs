using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schemes.Models.ViewModels
{
    public class ViewComment
    {
        public int id { get; set; }
        public int PostId { get; set; }
        public string EmailAutor { get; set; }
        public string CommentText { get; set; }
        public string time { get; set; }
        public int Likes { get; set; }
    }
}