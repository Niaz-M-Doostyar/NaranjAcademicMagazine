using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaranjAcademicMagazine.Areas.Admin.Models
{
    public class Topic
    {
        public int topicId { get; set; }
        public string title { get; set; }
        public string abstraction { get; set; }
        public string author1 { get; set; }
        public string designation1 { get; set; }
        public string author2 { get; set; }
        public string designation2 { get; set; }
        public string page { get; set; }
        public int views { get; set; }
        public int volume { get; set; }
        public int issue { get; set; }
        public DateTime year { get; set; }
        public HttpPostedFileBase pdfFile { get; set; }
        public string pdfFileString { get; set; }
        public HttpPostedFileBase image { get; set; }
        public string imageString { get; set; }
        public string ipAddress { get; set; }
        public DateTime viewAt { get; set; }
    }
}