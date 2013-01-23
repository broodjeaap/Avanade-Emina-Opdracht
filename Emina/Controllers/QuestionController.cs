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
    public class QuestionController : Controller
    {
        private EminaContext db = new EminaContext();

        //
        // GET: /Question/

        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.Enquete);
            return View(questions.ToList());
        }

        //
        // GET: /Question/Details/5

        public ActionResult Details(int id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // GET: /Question/Create

        public ActionResult Create()
        {
            ViewBag.EnqueteID = new SelectList(db.Enquetes, "EnqueteID", "Name");
            ViewBag.Type = QuestionType.Binary.ToSelectList();
            ViewBag.NextQuestion = new SelectList(db.Questions, "NextQuestion", "Text");
            return View();
        }

        //
        // POST: /Question/Create

        [HttpPost]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                question.QuestionNumber = db.Enquetes.Find(question.EnqueteID).Questions.Max(q => q.QuestionNumber) + 1;
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnqueteID = new SelectList(db.Enquetes, "EnqueteID", "Name", question.EnqueteID);
            return View(question);
        }

        //
        // GET: /Question/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type = QuestionType.Binary.ToSelectList();
            ViewBag.EnqueteID = new SelectList(db.Enquetes, "EnqueteID", "Name", question.EnqueteID);
            ViewBag.NextQuestion = new SelectList(db.Questions, "NextQuestion", "Text");
            return View(question);
        }

        //
        // POST: /Question/Edit/5

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnqueteID = new SelectList(db.Enquetes, "EnqueteID", "Name", question.EnqueteID);
            return View(question);
        }

        //
        // GET: /Question/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // POST: /Question/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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