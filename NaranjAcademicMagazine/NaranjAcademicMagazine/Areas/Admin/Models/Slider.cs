using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaranjAcademicMagazine.Areas.Admin.Models
{
    public class Slider
    {
        public int id { get; set; }
        public string header { get; set; }
        public string body { get; set; }
        public HttpPostedFileBase image { get; set; }
        public string imageString { get; set; }
    }
}