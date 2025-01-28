using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using NaranjAcademicMagazine.Areas.Admin.Models;
using NaranjAcademicMagazine.Models;

namespace NaranjAcademicMagazine.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = @"Data Source = (local); Initial Catalog = NaranjKdruDB; Integrated Security=True";
        // GET: Home
        public ActionResult Index()
        {
            DataTable dtIssue = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM ISSUEE",sqlCon);
                sqlDa.Fill(dtIssue);
            }
            return View(dtIssue);
        }

        public ActionResult Homepage()
        {
            SqlConnectionClass con = new SqlConnectionClass();
            var dt = con.Select("select * from ISSUEE");

            return View(dt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Issue());
        }

        [HttpPost]
        public ActionResult Create(Issue issues)
        {
            string Name = Path.GetFileName(issues.image.FileName);

            if (issues.image != null && issues.image.ContentLength > 0)
            {
                string fileName = Path.GetFileName(issues.image.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Areas/Admin/UploadedFiles"), fileName);
                issues.image.SaveAs(filePath);
            }
            string query = string.Format("INSERT INTO ISSUEE(volume,issue,date,image) VALUES({0},{1},'{2}','{3}')", issues.volume, issues.issue, issues.year, Name);
            SqlConnectionClass obj = new SqlConnectionClass();
            obj.Insert(query);
            return RedirectToAction("Homepage");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SqlConnectionClass con = new SqlConnectionClass();
            string selectQuery = "select * from ISSUEE where issueId = " + id;
            var result = con.Select(selectQuery);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(Issue issues, int id)
        {
            

            if (issues.image != null && issues.image.ContentLength > 0)
            {
                string fileName = Path.GetFileName(issues.image.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Areas/Admin/UploadedFiles"), fileName);
                issues.image.SaveAs(filePath);
            }
            string query = "update ISSUEE set volume = " + issues.volume+", issue = "+issues.issue+" where issueId = "+id;
            SqlConnectionClass obj = new SqlConnectionClass();
            obj.Update(query);
            return RedirectToAction("Homepage");
        }

        [HttpGet]
        public ActionResult Detele(int id)
        {
            string query = "Delete from ISSUEE where issueId = " + id;
            SqlConnectionClass con = new SqlConnectionClass();
            con.Delete(query);

            return RedirectToAction("Homepage");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Registration register)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-NJBSP6T;Initial Catalog=NaranjKdruDB;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Registration where username = '" + register.username + "'";
            cmd.Connection = con;

            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                ViewBag.error = "Username already exists";
                return View();

            }
            else
            {
                ConnectionString connection = new ConnectionString();
                string query = string.Format("INSERT INTO Registration(name,profession,email,phone,username,password,confirmPass) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", register.name, register.profession, register.email, register.phone, register.username, register.password, register.confirmPass);
                connection.Insert(query);
                return RedirectToAction("Index");
            }

        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login","Home", new { area = "" });
        }
    }
}