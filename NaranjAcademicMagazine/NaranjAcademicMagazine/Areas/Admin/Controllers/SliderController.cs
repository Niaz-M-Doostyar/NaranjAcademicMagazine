using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using NaranjAcademicMagazine.Areas.Admin.Models;

namespace NaranjAcademicMagazine.Areas.Admin.Controllers
{
    public class SliderController : Controller
    {
        // GET: Admin/Slider
        public ActionResult Index()
        {
            string query = "select * from Slider order by id desc";
            SqlConnectionClass con = new SqlConnectionClass();
            var dt = con.Select(query);
            return View(dt);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Slider slider)
        {
            string Name = Path.GetFileName(slider.image.FileName);

            if (slider.image != null && slider.image.ContentLength > 0)
            {
                string fileName = Path.GetFileName(slider.image.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Areas/Admin/UploadedFiles"), fileName);
                slider.image.SaveAs(filePath);
            }

            string query = string.Format("INSERT INTO Slider(header,body,image) VALUES('{0}','{1}','{2}')", slider.header, slider.body, Name);
            SqlConnectionClass obj = new SqlConnectionClass();
            obj.Insert(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SqlConnectionClass con = new SqlConnectionClass();
            string selectQuery = "select * from Slider where id = " + id;
            var result = con.Select(selectQuery);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(Slider slider, int id)
        {

            string Name = Path.GetFileName(slider.image.FileName);
            if (slider.image != null && slider.image.ContentLength > 0)
            {
                string fileName = Path.GetFileName(slider.image.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Areas/Admin/UploadedFiles"), fileName);
                slider.image.SaveAs(filePath);
            }
            string query = "update Slider set header = '" + slider.header + "', body = '" + slider.body + "', image = '" + Name + "' where id = " + id;
            SqlConnectionClass obj = new SqlConnectionClass();
            obj.Update(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            string query = "Delete from Slider where id = " + id;
            SqlConnectionClass con = new SqlConnectionClass();
            con.Delete(query);

            return View();
        }
    }
}