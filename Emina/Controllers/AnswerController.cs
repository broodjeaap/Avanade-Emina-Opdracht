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
    public class AnswerController : Controller
    {
        private EminaContext db = new EminaContext();

        //
        // GET: /Answer/

        public ActionResult Index()
        {
            var answers = db.Answers.Include(a => a.Enquete).Include(a => a.Question).Include(a => a.PossibleAnswer);
            return View(answers.ToList());
        }

        //
        // GET: /Answer/Details/5

        public ActionResult Details(int id = 0)
        {
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        //
        // GET: /Answer/Create

        public ActionResult Create()
        {
            ViewBag.EnqueteID = new SelectList(db.Enquetes, "EnqueteID", "Name");
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Text");
            ViewBag.PossibleAnswerID = new SelectList(db.PossibleAnswers, "PossibleAnswerID", "Text");
            return View();
        }

        //
        // POST: /Answer/Create

        [HttpPost]
        public ActionResult Create(Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnqueteID = new SelectList(db.Enquetes, "EnqueteID", "Name", answer.EnqueteID);
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Text", answer.QuestionID);
            ViewBag.PossibleAnswerID = new SelectList(db.PossibleAnswers, "PossibleAnswerID", "Text", answer.PossibleAnswerID);
            return View(answer);
        }

        //
        // GET: /Answer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnqueteID = new SelectList(db.Enquetes, "EnqueteID", "Name", answer.EnqueteID);
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Text", answer.QuestionID);
            ViewBag.PossibleAnswerID = new SelectList(db.PossibleAnswers, "PossibleAnswerID", "Text", answer.PossibleAnswerID);
            return View(answer);
        }

        //
        // POST: /Answer/Edit/5

        [HttpPost]
        public ActionResult Edit(Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnqueteID = new SelectList(db.Enquetes, "EnqueteID", "Name", answer.EnqueteID);
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Text", answer.QuestionID);
            ViewBag.PossibleAnswerID = new SelectList(db.PossibleAnswers, "PossibleAnswerID", "Text", answer.PossibleAnswerID);
            return View(answer);
        }

        //
        // GET: /Answer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        //
        // POST: /Answer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
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