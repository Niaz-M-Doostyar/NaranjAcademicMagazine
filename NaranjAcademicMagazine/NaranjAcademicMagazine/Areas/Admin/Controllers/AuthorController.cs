using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using NaranjAcademicMagazine.Areas.Admin.Models;

namespace NaranjAcademicMagazine.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            SqlConnectionClass con = new SqlConnectionClass();
            var dt = con.Select("select * from AUTHOR");
            return View(dt);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(author authors)
        {
            string Name = Path.GetFileName(authors.image.FileName);

            if (authors.image != null && authors.image.ContentLength > 0)
            {
                string fileName = Path.GetFileName(authors.image.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Areas/Admin/UploadedFiles"), fileName);
                authors.image.SaveAs(filePath);
            }
            string query = string.Format("INSERT INTO AUTHOR(authorName,decription,email,facebook,twitter,image,linkedIn,phone) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", authors.authorName, authors.decription, authors.email, authors.facebook, authors.twitter, Name, authors.linkedIn, authors.phone);
            SqlConnectionClass obj = new SqlConnectionClass();
            obj.Insert(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SqlConnectionClass con = new SqlConnectionClass();
            string selectQuery = "select * from AUTHOR where authorId = " + id;
            var result = con.Select(selectQuery);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(author authors, int id)
        {

            string Name = Path.GetFileName(authors.image.FileName);
            if (authors.image != null && authors.image.ContentLength > 0)
            {
                string fileName = Path.GetFileName(authors.image.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Areas/Admin/UploadedFiles"), fileName);
                authors.image.SaveAs(filePath);
            }
            string query = "update AUTHOR set authorName = '" + authors.authorName + "', decription = '" + authors.decription + "', email = '" + authors.email + "', facebook = '" + authors.facebook + "', twitter = '" + authors.twitter + "', image = '" + Name + "', linkedIn = '" + authors.linkedIn + "', phone = '" + authors.phone + "' where authorId = " + id;
            SqlConnectionClass obj = new SqlConnectionClass();
            obj.Update(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            string query = "Delete from AUTHOR where authorId = " + id;
            SqlConnectionClass con = new SqlConnectionClass();
            con.Delete(query);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult List(int id)
        {
            string query = "select * from AUTHOR where authorId = " + id;
            SqlConnectionClass con = new SqlConnectionClass();
            var dt = con.Select(query);
            return View(dt);
        }
    }
}