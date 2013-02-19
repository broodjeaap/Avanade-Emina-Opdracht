using Emina.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emina.Controllers
{
    public class TakeEnqueteController : Controller
    {
        private EminaContext db = new EminaContext();

        //
        // GET: /TakeEnquete/

        public ActionResult Index()
        {
            return View(db.Enquetes.ToList());//Where(enquete => enquete.StartDate <= DateTime.Now && enquete.EndDate > DateTime.Now)
        }

        //
        // GET: /TakeEnquete/Question

        public ActionResult Question(int EnqueteID,int QuestionId)
        {
            var e = db.Enquetes.Find(EnqueteID);            
            if (e != null)
            {
                var q = (from question in e.Questions where question.QuestionNumber == QuestionId select question).First();
                if (q != null)
                {
                    var userId = 2; //todo
                    var answers = db.Answers.Where(a => a.EnqueteID == EnqueteID && a.QuestionID == QuestionId && a.UserID == userId);
                    Answer answer;
                    if (answers.Count() == 0)
                    {
                        answer = new Answer();
                        answer.EnqueteID = e.EnqueteID;
                        answer.Enquete = e;
                        answer.QuestionID = q.QuestionID;
                        answer.Question = q;
                        answer.UserID = userId;
                        //answer.User = user;
                    }
                    else
                    {
                        answer = answers.First();
                    }
                    //System.Diagnostics.Debug.WriteLine(q.Type.ToString() + "Question" + ", eid = " + EnqueteID + ", qid = " + QuestionId);
                    return View(q.Type.ToString() + "Question", answer);
                }
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /TakeEnquete/Question



        [HttpPost]
        public ActionResult Question(Answer answer)
        {
            if (ModelState.IsValid)
            {
                if (db.Answers.Find(answer.AnswerID) != null)
                {
                    db.Entry(answer).State = EntityState.Modified;
                }
                else
                {
                    db.Answers.Add(answer);
                }
                db.SaveChanges();

                var currentQ = answer.Question;
                if (currentQ.Type == QuestionType.MultipleChoice || currentQ.Type == QuestionType.Checkbox)
                {
                    var nextQ = answer.PossibleAnswer.NextQuestion;
                    if (nextQ != null)
                    {
                        return View(nextQ.Type.ToString() + "Question", nextQ);
                    }
                }
                return View(currentQ.Type.ToString() + "Question", currentQ);                
            }

            return RedirectToAction("Index");
        }
    }
}
