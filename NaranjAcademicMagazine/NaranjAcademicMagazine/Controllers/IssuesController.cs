using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using NaranjAcademicMagazine.Areas.Admin.Models;

namespace NaranjAcademicMagazine.Controllers
{
    public class Issuehelper
    {
        public string year { get; set; }
        public List<Issue> issue { get; set; }
    }

    public class Topichelper
    {
        public List<Topic> topic { get; set; }
    }

    public class IssuesController : Controller
    {
        // GET: Issues
        public ActionResult Index()
        {
            SqlConnectionClass connection = new SqlConnectionClass();
            //var dt = connection.Select("select * from ISSUEE");
            //Issuehelper sh = new Issuehelper();
            //sh.issue = new List<Issue>();
           
            List<Issuehelper> shelper = new List<Issuehelper>();
            List<int> years = new List<int>();
            var datesOnly = connection.Select("SELECT distinct Year(date) FROM ISSUEE");
            for (int i = 0; i < datesOnly.Rows.Count; i++)
            {
                years.Add(Convert.ToInt32(datesOnly.Rows[i][0]));
            }
            years.Reverse();
            foreach(int y in years)
            {
                var dt = connection.Select("select * from ISSUEE where YEAR(date) = " + y);
                List<Issue> shlist = new List<Issue>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    shlist.Add(new Issue() { issueId = Convert.ToInt32(dt.Rows[i][0]), year = (DateTime)dt.Rows[i][3], issue = Convert.ToInt32(dt.Rows[i][2]), imageString = dt.Rows[i][4].ToString(), volume = Convert.ToInt32(dt.Rows[i][1]) } );
                }
                shelper.Add(new Issuehelper() { year = y.ToString(), issue = shlist });
            }
           
            
            return View(shelper);
        }

        // GET: ReadContent
        [HttpGet]
        public ActionResult ReadContent(int id)
        {
            SqlConnectionClass connection = new SqlConnectionClass();
            var dt = connection.Select("SELECT * FROM TOPIC where topicId = " + id);
            if (dt == null)
            {
                return HttpNotFound();
            }

            int view = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                view = Convert.ToInt32(dt.Rows[i][8]);
            }
            // Check if the user has a cookie indicating that they've already viewed this article recently
            var viewedCookie = Request.Cookies["ViewedArticle_" + id];
            if (viewedCookie == null)
            {
                // If no cookie is found, track the view
                TrackArticleView(id, ++view);

                // Create a persistent cookie to prevent multiple views within 24 hours
                Response.Cookies.Add(new HttpCookie("ViewedArticle_" + id, "true")
                {
                    Expires = DateTime.Now.AddDays(1) // Cookie expires in 24 hours
                });
            }
            return View(dt);
        }

        private void TrackArticleView(int articleId, int view)
        {
            Topic topic = new Topic();
            // Get the IP address of the viewer
            string ipAddress = Request.UserHostAddress;
            string query = "update TOPIC set ipAddress = '" + ipAddress + "', viewAt = '" + DateTime.Now + "', views = '"+ view +"' where topicId = " + articleId;
            SqlConnectionClass obj = new SqlConnectionClass();
            obj.Update(query);
        }

        // GET: WriterDetails
        public ActionResult WriterDetails()
        {
            SqlConnectionClass connection = new SqlConnectionClass();

            DataTable dt = new DataTable();
            dt = connection.Select("SELECT * FROM TOPIC");
            List<Topic> topicTable = new List<Topic>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                topicTable.Add(new Topic() { topicId = Convert.ToInt32(dt.Rows[i][0]), title = Convert.ToString(dt.Rows[i][1]), abstraction = Convert.ToString(dt.Rows[i][2]), author1 = Convert.ToString(dt.Rows[i][3]), designation1 = Convert.ToString(dt.Rows[i][4]), author2 = Convert.ToString(dt.Rows[i][5]), designation2 = Convert.ToString(dt.Rows[i][6]), page = Convert.ToString(dt.Rows[i][7]), views = Convert.ToInt32(dt.Rows[i][8]), year = (DateTime)(dt.Rows[i][11]), pdfFileString = dt.Rows[i][12].ToString(), imageString = dt.Rows[i][13].ToString() });
            }

            ViewBag.TopicTable = topicTable;

            return View();
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row => {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }
    }
}