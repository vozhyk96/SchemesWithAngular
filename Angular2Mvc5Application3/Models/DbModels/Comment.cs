using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schemes.Models.DbModels
{
    public class Comment
    {
        public int id { get; set; }
        public int PostId { get; set; }
        public string EmailAutor { get; set; }
        public string CommentText { get; set; }
        public DateTime time { get; set; }
        public int Likes { get; set; }
        public string LikedUserIds { get; set; }
    }
}