using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaranjAcademicMagazine.Areas.Admin.Models;

namespace NaranjAcademicMagazine.Controllers
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
    }
}