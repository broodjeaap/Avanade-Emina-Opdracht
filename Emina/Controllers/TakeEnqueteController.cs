﻿using Emina.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Emina.Controllers
{
    [Authorize]
    public class TakeEnqueteController : Controller
    {
        private EminaContext db = new EminaContext();

        //
        // GET: /TakeEnquete/

        public ActionResult Index()
        {
            var enrolledEnquetes = db.Enrollments.Where(e => e.UserID == WebSecurity.CurrentUserId).Select(e => e.UserID);
            return View(db.Enquetes.Where(enquete => enquete.StartDate <= DateTime.Now && enquete.EndDate > DateTime.Now && enrolledEnquetes.Contains(enquete.EnqueteID)).ToList());
        }

        //
        // GET: /TakeEnquete/Question

        public ActionResult Question(int EnqueteID,int QuestionNumber)
        {
            var e = db.Enquetes.Find(EnqueteID);            
            if (e != null)
            {
                var q = (from question in e.Questions where question.QuestionNumber == QuestionNumber select question).First();
                if (q != null)
                {
                    var userId = 1; //todo
                    var answers = db.Answers.Where(a => a.EnqueteID == EnqueteID && a.QuestionID == q.QuestionID && a.UserID == userId);
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
                        answer.EnqueteID = e.EnqueteID; //entity framework \o/
                        answer.Enquete = e;
                        answer.QuestionID = q.QuestionID;
                        answer.Question = q;
                        answer.UserID = userId;
                        //answer.User = user;
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
                if (db.Answers.Find(answer.AnswerID) == null)
                {
                    answer.AnswerID = ((db.Answers.Max(a => (int?)a.AnswerID) ?? 0) + 1); //soooo pretty
                    db.Answers.Add(answer);
                }
                db.SaveChanges();

                var currentQ = db.Questions.Find(answer.QuestionID);
                if (currentQ.Type == QuestionType.MultipleChoice || currentQ.Type == QuestionType.Checkbox)
                {
                    var nextQ = db.Questions.Find(answer.PossibleAnswer.NextQuestionID);
                    if (nextQ != null)
                    {
                        return RedirectToAction("Question", new { EnqueteID = nextQ.EnqueteID, QuestionNumber = nextQ.NextQuestion.QuestionNumber });
                    }
                }
                if (currentQ.NextQuestion != null)
                {
                    return RedirectToAction("Question", new { EnqueteID = currentQ.EnqueteID, QuestionNumber = currentQ.NextQuestion.QuestionNumber });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult UrlLogin(string guid)
        {
            //verification & login stuff
            return RedirectToAction("Index");
        }
    }
}
