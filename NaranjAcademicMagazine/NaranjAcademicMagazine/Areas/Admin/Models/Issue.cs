using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaranjAcademicMagazine.Areas.Admin.Models
{
    public class Issue
    {
        public int issueId { get; set; }
        public int volume { get; set; }
        public int issue { get; set; }
        public DateTime year { get; set; }
        public HttpPostedFileBase image { get; set; }
        public string imageString { get; set; }
    }
}