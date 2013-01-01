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
    public class PossibleAnswerController : Controller
    {
        private EminaContext db = new EminaContext();

        //
        // GET: /PossibleAnswer/

        public ActionResult Index()
        {
            var possibleanswers = db.PossibleAnswers.Include(p => p.Question);
            return View(possibleanswers.ToList());
        }

        //
        // GET: /PossibleAnswer/Details/5

        public ActionResult Details(int id = 0)
        {
            PossibleAnswer possibleanswer = db.PossibleAnswers.Find(id);
            if (possibleanswer == null)
            {
                return HttpNotFound();
            }
            return View(possibleanswer);
        }

        //
        // GET: /PossibleAnswer/Create

        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Text");
            return View();
        }

        //
        // POST: /PossibleAnswer/Create

        [HttpPost]
        public ActionResult Create(PossibleAnswer possibleanswer)
        {
            if (ModelState.IsValid)
            {
                db.PossibleAnswers.Add(possibleanswer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Text", possibleanswer.QuestionID);
            return View(possibleanswer);
        }

        //
        // GET: /PossibleAnswer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PossibleAnswer possibleanswer = db.PossibleAnswers.Find(id);
            if (possibleanswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Text", possibleanswer.QuestionID);
            return View(possibleanswer);
        }

        //
        // POST: /PossibleAnswer/Edit/5

        [HttpPost]
        public ActionResult Edit(PossibleAnswer possibleanswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(possibleanswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Text", possibleanswer.QuestionID);
            return View(possibleanswer);
        }

        //
        // GET: /PossibleAnswer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PossibleAnswer possibleanswer = db.PossibleAnswers.Find(id);
            if (possibleanswer == null)
            {
                return HttpNotFound();
            }
            return View(possibleanswer);
        }

        //
        // POST: /PossibleAnswer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PossibleAnswer possibleanswer = db.PossibleAnswers.Find(id);
            db.PossibleAnswers.Remove(possibleanswer);
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