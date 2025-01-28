using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaranjAcademicMagazine.Areas.Admin.Models;

namespace NaranjAcademicMagazine.Controllers
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