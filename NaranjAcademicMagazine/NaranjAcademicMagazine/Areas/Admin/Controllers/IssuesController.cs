using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using NaranjAcademicMagazine.Areas.Admin.Models;

namespace NaranjAcademicMagazine.Areas.Admin.Controllers
{
    public class IssuesController : Controller
    {
        int globeId = 0;
      
        [HttpGet]
        public ActionResult Index()
        
        {
            SqlConnectionClass con = new SqlConnectionClass();
            var dt = con.Select("select * from TOPIC");
            
            return View(dt);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Issue());
        }

        [HttpPost]
        public ActionResult Create(Topic topic)
        {
            var date = DateTime.Now;
            var dateTimeString = date.ToString("yyyy-MM-dd HH:mm:ss");
            var dateTime = dateTimeString.GetHashCode();
            string Name = Path.GetFileName(topic.pdfFile.FileName);
            string image = Path.GetFileName(topic.image.FileName);

            if (topic.pdfFile != null && topic.pdfFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(topic.pdfFile.FileName);
                string file = dateTime + fileName;
                string filePath = Path.Combine(Server.MapPath("~/Areas/Admin/UploadedFiles"), file);
                topic.pdfFile.SaveAs(filePath);
            }

            if (topic.image != null && topic.image.ContentLength > 0)
            {
                string fileName = Path.GetFileName(topic.image.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Areas/Admin/UploadedFiles"), fileName);
                topic.image.SaveAs(filePath);
            }

            string query = string.Format("INSERT INTO TOPIC(title,abstraction,author1,designation1,author2,designation2,page,views,volume,issue,year,pdfFile,image) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7},{8},{9},'{10}','{11}','{12}')", topic.title, topic.abstraction, topic.author1, topic.designation1, topic.author2, topic.designation2, topic.page, topic.views, topic.volume, topic.issue, topic.year, dateTime+Name, image);
            SqlConnectionClass obj = new SqlConnectionClass();
            obj.Insert(query);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            globeId = id;
            SqlConnectionClass con = new SqlConnectionClass();
            string selectQuery = "select * from TOPIC where topicId = " + id;
            var result = con.Select(selectQuery);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(Topic topic, int id)
        {
            var date = DateTime.Now;
            var dateTimeString = date.ToString("yyyy-MM-dd HH:mm:ss");
            var dateTime = dateTimeString.GetHashCode();

            if (topic.image != null && topic.image.ContentLength > 0)
            {
                string fileName = Path.GetFileName(topic.image.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Areas/Admin/UploadedFiles"), fileName);
                topic.image.SaveAs(filePath);
                string query = "update TOPIC set image = '" + fileName + "' where topicId = " + id;
                SqlConnectionClass obj = new SqlConnectionClass();
                obj.Insert(query);
            }

            if (topic.pdfFile != null && topic.pdfFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(topic.pdfFile.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Areas/Admin/UploadedFiles"), fileName);
                topic.pdfFile.SaveAs(filePath);
                string query = "update TOPIC set pdfFile = '" + fileName + "' where topicId = " + id;
                SqlConnectionClass obj = new SqlConnectionClass();
                obj.Insert(query);
            }

            if (topic.year == DateTime.Today)
            {
                string query = "update TOPIC set year = '" + topic.year + "' where topicId = " + id;
                SqlConnectionClass obj = new SqlConnectionClass();
                obj.Insert(query);
            }

            else
            {
                string query = "update TOPIC set title = '" + topic.title + "', abstraction = '" + topic.abstraction + "', author1 = '" + topic.author1 + "', designation1 = '" + topic.designation1 + "', author2 = '" + topic.author2 + "', designation2 = '" + topic.designation2 + "', page = '" + topic.page + "' where topicId = " + id;
                SqlConnectionClass obj = new SqlConnectionClass();
                obj.Insert(query);
            }
            

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            string query = "Delete from TOPIC where topicId = " + id;
            SqlConnectionClass con = new SqlConnectionClass();
            con.Delete(query);

            return RedirectToAction("Index");
        }

    }
}