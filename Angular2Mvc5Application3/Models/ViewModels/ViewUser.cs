using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schemes
{
    public class ViewUser
    {
        public string id { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public bool HasPassword { get; set; }
        public bool isUser { get; set; }
        public Picture picture { get; set; }
        public List<ViewPost> posts { get; set; }

        
    }
}