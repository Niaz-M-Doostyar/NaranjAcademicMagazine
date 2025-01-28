using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaranjAcademicMagazine.Controllers
{
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            return View();
        }

        // GET: Editor
        public ActionResult Editor()
        {
            return View();
        }

        // GET: Policy
        public ActionResult Policy()
        {
            return View();
        }

        // GET: Guidline
        public ActionResult Guidline()
        {
            return View();
        }

        // GET: Copyright
        public ActionResult Copyright()
        {
            return View();
        }
    }
}