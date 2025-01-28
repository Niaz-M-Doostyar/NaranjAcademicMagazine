using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaranjAcademicMagazine.Areas.Admin.Models
{
    public class EditorialBoard
    {
        public int memberId { get; set; }
        public string memberName { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string linkedIn { get; set; }
        public HttpPostedFileBase image { get; set; }
        public string phone { get; set; }
    }
}