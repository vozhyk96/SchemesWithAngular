using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schemes.Models.DbModels
{
    public class VoteLog
    {
        public int id { get; set; }
        public short SectionId { get; set; }
        public int VoteForId { get; set; }
        public string UserName { get; set; }
        public short Vote { get; set; }
        public bool Active { get; set; }
    }
}