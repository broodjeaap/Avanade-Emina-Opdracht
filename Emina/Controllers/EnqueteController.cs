using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emina.Models;

namespace Emina.Controllers
{
    public class EnqueteController : Controller
    {
        private EminaContext db = new EminaContext();

        //
        // GET: /Enquete/

        public ActionResult Index()
        {
            return View(db.Enquetes.ToList());
        }

        //
        // GET: /Enquete/Details/5

        public ActionResult Details(int id = 0)
        {
            Enquete enquete = db.Enquetes.Find(id);
            if (enquete == null)
            {
                return HttpNotFound();
            }
            return View(enquete);
        }

        //
        // GET: /Enquete/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Enquete/Create

        [HttpPost]
        public ActionResult Create(Enquete enquete)
        {
            if (ModelState.IsValid)
            {
                db.Enquetes.Add(enquete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enquete);
        }

        //
        // GET: /Enquete/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Enquete enquete = db.Enquetes.Find(id);
            if (enquete == null)
            {
                return HttpNotFound();
            }
            return View(enquete);
        }

        //
        // POST: /Enquete/Edit/5

        [HttpPost]
        public ActionResult Edit(Enquete enquete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enquete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enquete);
        }

        //
        // GET: /Enquete/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Enquete enquete = db.Enquetes.Find(id);
            if (enquete == null)
            {
                return HttpNotFound();
            }
            return View(enquete);
        }

        //
        // POST: /Enquete/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Enquete enquete = db.Enquetes.Find(id);
            db.Enquetes.Remove(enquete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}