using Emina.Models;
using System;
using System.Collections.Generic;
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

        public ActionResult Question()
        {

            return RedirectToAction("Index");
        }

        //
        // POST: /TakeEnquete/Question

        [HttpPost]
        public ActionResult Question(FormCollection collection)
        {
            var keys = collection.AllKeys;
            if (!keys.Contains("EnqueteID") || !keys.Contains("QuestionID"))
            {
                return RedirectToAction("Index");
            }

            var Eid = Convert.ToInt32(collection["EnqueteID"]);
            var Qid = Convert.ToInt32(collection["QuestionID"]);
            var enquetes = db.Enquetes.Find(Eid); 
            Question q;
            if (keys.Contains("Answer") && false)
            {
                
                var answer = new Answer();

                answer.EnqueteID = Eid;
                answer.QuestionID = Qid;
                answer.UserID = 1; //Todo, user stuff
                var tmpQ = db.Questions.Find(Qid);
                if (tmpQ.Type == QuestionType.MultipleChoice)
                {
                    answer.PossibleAnswerID = Convert.ToInt32(collection["Answer"]);
                    q = db.Questions.Find(answer.PossibleAnswerID);
                }
                else
                {
                    answer.TextAnswer = collection["Answer"];
                    q = tmpQ.NextQuestion;
                }
                db.Answers.Add(answer);
                db.SaveChanges();
            }
            else
            {
                q = db.Questions.Find(Qid);
            }
            if (q != null)
            {
                return View(q.Type.ToString()+"Question", q);
            }
            return RedirectToAction("Index");
        }
    }
}
