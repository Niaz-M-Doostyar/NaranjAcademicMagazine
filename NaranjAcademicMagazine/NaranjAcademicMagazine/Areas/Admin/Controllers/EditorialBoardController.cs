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
    public class EditorialBoardController : Controller
    {
        // GET: EditorialBoard
        public ActionResult Index()
        {
            SqlConnectionClass con = new SqlConnectionClass();
            var dt = con.Select("select * from EditorialBoard");
            return View(dt);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EditorialBoard member)
        {
            string Name = Path.GetFileName(member.image.FileName);

            if (member.image != null && member.image.ContentLength > 0)
            {
                string fileName = Path.GetFileName(member.image.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Areas/Admin/UploadedFiles"), fileName);
                member.image.SaveAs(filePath);
            }
            string query = string.Format("INSERT INTO EditorialBoard(memberName,description,email,facebook,twitter,linkedIn,image,phone) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", member.memberName, member.description, member.email, member.facebook, member.twitter, member.linkedIn, Name, member.phone);
            SqlConnectionClass obj = new SqlConnectionClass();
            obj.Insert(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SqlConnectionClass con = new SqlConnectionClass();
            string selectQuery = "select * from EditorialBoard where memberId = " + id;
            var result = con.Select(selectQuery);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(EditorialBoard member, int id)
        {
            string Name = Path.GetFileName(member.image.FileName);
            if (member.image != null && member.image.ContentLength > 0)
            {
                string fileName = Path.GetFileName(member.image.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Areas/Admin/UploadedFiles"), fileName);
                member.image.SaveAs(filePath);
            }
            string query = "update EditorialBoard set memberName = '" + member.memberName + "', description = '" + member.description + "', email = '" + member.email + "', facebook = '" + member.facebook + "', twitter = '" + member.twitter + "', linkedIn = '" + member.linkedIn + "', image = '" + Name + "', phone = '"+ member.phone +"' where memberId = " + id;
            SqlConnectionClass obj = new SqlConnectionClass();
            obj.Update(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            string query = "Delete from EditorialBoard where memberId = " + id;
            SqlConnectionClass con = new SqlConnectionClass();
            con.Delete(query);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult List(int id)
        {
            string query = "select * from EditorialBoard where memberId = " + id;
            SqlConnectionClass con = new SqlConnectionClass();
            var dt = con.Select(query);
            return View(dt);
        }
    }
}