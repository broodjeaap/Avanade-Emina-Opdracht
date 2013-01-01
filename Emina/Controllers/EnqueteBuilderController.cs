using Emina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emina.Controllers
{
    public class EnqueteBuilderController : Controller
    {
        private EminaContext db = new EminaContext();

        //
        // GET: /EnqueteBuilder/

        public ActionResult Index()
        {
            return View(db.Enquetes.ToList());
        }

        public ActionResult NewEnquete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewEnquete(Enquete enquete)
        {
            if(ModelState.IsValid)
            {
                db.Enquetes.Add(enquete);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteEnquete(int id)
        {
            var enquete = db.Enquetes.Find(id);
            if (enquete != null)
            {
                db.Enquetes.Remove(enquete);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditEnquete(int id)
        {
            System.Diagnostics.Debug.WriteLine("Test");
            var e = db.Enquetes.Find(id);
            if (e != null)
            {
                return View(db.Enquetes.Find(id));
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditQuestions(int id)
        {
            ViewBag.QuestionTypes = Enum.GetValues(typeof(QuestionType));
            return View(db.Enquetes.Find(id));
        }
    }
}
