using NaranjAcademicMagazine.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaranjAcademicMagazine.Areas.Admin.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            SqlConnectionClass con = new SqlConnectionClass();
            var dt = con.Select("select * from About");

            return View(dt);
        }

        public ActionResult Create()
        {
            return View(new About());
        }

        [HttpPost]
        public ActionResult Create(About about)
        {
            string query = string.Format("INSERT INTO About(title,description) VALUES('{0}','{1}')", about.title, about.description);
            SqlConnectionClass obj = new SqlConnectionClass();
            obj.Insert(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SqlConnectionClass con = new SqlConnectionClass();
            string selectQuery = "select * from About where id = " + id;
            var result = con.Select(selectQuery);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(About about, int id)
        {
            string query = "update About set title = '" + about.title + "', description = '" + about.description + "' where id = " + id;
            SqlConnectionClass obj = new SqlConnectionClass();
            obj.Update(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            string query = "Delete from About where id = " + id;
            SqlConnectionClass con = new SqlConnectionClass();
            con.Delete(query);

            return RedirectToAction("Index");
        }

        // GET: Aim
        public ActionResult Aim()
        {
            return View();
        }

        //GET: Indexing
        public ActionResult Indexing()
        {
            return View();
        }

        //GET: Copyright
        public ActionResult Copyright()
        {
            return View();
        }

        //GET: Editorial
        public ActionResult Editorial()
        {
            return View();
        }
    }
}