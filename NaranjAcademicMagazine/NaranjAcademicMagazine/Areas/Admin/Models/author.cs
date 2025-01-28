using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaranjAcademicMagazine.Areas.Admin.Models
{
    public class author
    {
        public int authorId { get; set; }
        public string authorName { get; set; }
        public string decription { get; set; }
        public string email { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public HttpPostedFileBase image { get; set; }
        public string linkedIn { get; set; }
        public string phone { get; set; }

    }
}