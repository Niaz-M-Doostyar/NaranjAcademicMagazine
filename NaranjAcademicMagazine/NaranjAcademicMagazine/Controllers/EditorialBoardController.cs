using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaranjAcademicMagazine.Areas.Admin.Models;

namespace NaranjAcademicMagazine.Controllers
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
    }
}