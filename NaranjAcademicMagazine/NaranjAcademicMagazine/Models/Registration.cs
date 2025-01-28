using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaranjAcademicMagazine.Models
{
    public class Registration
    {
        public int id { get; set; }
        public string name { get; set; }
        public string profession { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string confirmPass { get; set; }
    }
}