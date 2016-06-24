using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schemes.Models.DbModels
{
    public class Temp
    {
        public int id { get; set; }
        public string UserId { get; set; }
        public byte[] Image { get; set; }
        public string json { get; set; }
    }
}