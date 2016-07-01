using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schemes.Models.DbModels
{
    public class Tag
    {
        public int id { get; set; }
        public string tagName { get; set; }
        public string idsOfPosts { get; set; }
        public int Count { get; set; }
    }
}