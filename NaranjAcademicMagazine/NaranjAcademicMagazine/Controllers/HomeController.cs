using NaranjAcademicMagazine.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using NaranjAcademicMagazine.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace NaranjAcademicMagazine.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            SqlConnectionClass connection = new SqlConnectionClass();

            DataTable resutlIssue = new DataTable();
            resutlIssue = connection.Select("SELECT TOP 1 * FROM ISSUEE ORDER BY issueId DESC"); 
            List<Issue> issueTable = new List<Issue>();
            issueTable = ConvertToList<Issue>(resutlIssue);

            DataTable dt = new DataTable();
            dt = connection.Select("SELECT * FROM TOPIC");
            List<Topic> topicTable = new List<Topic>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                topicTable.Add(new Topic() { topicId = Convert.ToInt32(dt.Rows[i][0]), title = Convert.ToString(dt.Rows[i][1]), abstraction = Convert.ToString(dt.Rows[i][2]), author1 = Convert.ToString(dt.Rows[i][3]), designation1 = Convert.ToString(dt.Rows[i][4]), author2 = Convert.ToString(dt.Rows[i][5]), designation2 = Convert.ToString(dt.Rows[i][6]), page = Convert.ToString(dt.Rows[i][7]), views = Convert.ToInt32(dt.Rows[i][8]), year = (DateTime)(dt.Rows[i][11]), pdfFileString = dt.Rows[i][12].ToString(), imageString = dt.Rows[i][13].ToString() });
            }

            DataTable dt1 = new DataTable();
            dt1 = connection.Select("SELECT TOP 2 * FROM Slider ORDER BY id DESC");
            List<Slider> sliderTable = new List<Slider>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                sliderTable.Add(new Slider() { id = Convert.ToInt32(dt1.Rows[i][0]), header = Convert.ToString(dt1.Rows[i][1]), body = Convert.ToString(dt1.Rows[i][2]), imageString = dt1.Rows[i][3].ToString() });
            }
            //topicTable = ConvertToList<Topic>(resultTopic);

            ViewBag.IssueTable = issueTable;
            ViewBag.TopicTable = topicTable;
            ViewBag.SliderTable = sliderTable;

            return View();
        }

        [HttpGet]
        public ActionResult Search(string author)
        {
            //SqlConnectionClass connection = new SqlConnectionClass();

            //DataTable resutlIssue = new DataTable();
            //resutlIssue = connection.Select("SELECT * FROM ISSUEE where issueId = " + id);
            //List<Issue> issueTable = new List<Issue>();
            //issueTable = ConvertToList<Issue>(resutlIssue);

            //DataTable resultTopic = new DataTable();
            //resultTopic = connection.Select("SELECT * FROM TOPIC where '"+author+"' IN(author1,author2)");
            //List<Topic> topicTable = new List<Topic>();
            //topicTable = ConvertToList<Topic>(resultTopic);

            //ViewBag.IssueTable = issueTable;
            //ViewBag.TopicTable = topicTable;

            //return View();

            SqlConnectionClass connection = new SqlConnectionClass();
            var dt = connection.Select("SELECT * FROM TOPIC where '" + author + "' IN(author1,author2)");
            return View(dt);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            ConnectionString con = new ConnectionString();

            DataTable dt = new DataTable();
            dt = con.Select("select * from Registration where username = '" + username + "' and password = '" + password + "'");
            List<Registration> RegTable = new List<Registration>();
            RegTable = ConvertToList<Registration>(dt);

            if (RegTable.Count() > 0)
            {
                //add session
                Session["name"] = RegTable.FirstOrDefault().name;
                Session["username"] = RegTable.FirstOrDefault().email;
                return RedirectToAction("Homepage", "Home", new { area = "Admin" });
            }
            else
            {
                ViewBag.error = "Login failed";
                return RedirectToAction("Login");
            }

            return View();
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public ActionResult ChangeLanguage(string lang)
        {
            new LanguageCol().SetLanguage(lang);
            return RedirectToAction("Index", "Home");
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