using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaranjAcademicMagazine.Areas.Admin.Models
{
    public class CombinedViewModel
    {
        public IEnumerable<Issue> IssueTable { get; set; }
        public IEnumerable<Topic> TopicTable { get; set; }
    }
}