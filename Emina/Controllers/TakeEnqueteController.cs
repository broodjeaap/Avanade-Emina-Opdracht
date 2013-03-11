using Emina.Models;
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
            var enrolledEnquetes = db.Enrollments.Where(e => e.UserID == WebSecurity.CurrentUserId && e.role == EnrollmentRole.User).Select(e => e.EnqueteID);
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
                    
                    var userId = WebSecurity.CurrentUserId;
                    var answers = db.Answers.Where(a => a.EnqueteID == EnqueteID && a.QuestionID == q.QuestionID && a.UserID == userId);
                    Answer answer;
                    List<int> answered = new List<int>();
                    if (answers.Count() == 0)
                    {
                        answer = new Answer();
                        answer.EnqueteID = e.EnqueteID;
                        answer.Enquete = e;
                        answer.QuestionID = q.QuestionID;
                        answer.UserID = userId;
                    }
                    else
                    {
                        answer = answers.First();
                        answer.EnqueteID = e.EnqueteID; //entity framework \o/
                        answer.Enquete = e;
                        answer.QuestionID = q.QuestionID;
                        answer.UserID = userId;
                        if (q.Type == QuestionType.Checkbox)
                        {
                            var split = answer.TextAnswer.Split(';');
                            foreach (string s in split)
                            {
                                answered.Add(int.Parse(s));
                            }
                        }
                    }
                    ViewBag.answered = answered;
                    ViewBag.Question = q;
                    
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
                var answers = db.Answers.Where(a => a.EnqueteID == answer.EnqueteID && a.QuestionID == answer.QuestionID && a.UserID == WebSecurity.CurrentUserId);
                var currentQ = db.Questions.Find(answer.QuestionID);
                if (answers.Count() == 0)
                {
                    answer.AnswerID = ((db.Answers.Max(a => (int?)a.AnswerID) ?? 0) + 1); //soooo pretty
                    db.Answers.Add(answer);
                }
                else
                {
                    var an = answers.First();
                    if (currentQ.Type == QuestionType.MultipleChoice)
                    {
                        an.PossibleAnswerID = answer.PossibleAnswerID;
                    }
                    else
                    {
                        an.TextAnswer = answer.TextAnswer;
                    }
                }
                db.SaveChanges();
                
                if (currentQ.Type == QuestionType.MultipleChoice)
                {
                    var pa = db.PossibleAnswers.Find(answer.PossibleAnswerID);
                    if (pa.NextQuestionID == 0) //what
                    {
                        if(pa.NextQuestion != null) //the 
                        {
                            pa.NextQuestionID = pa.NextQuestion.QuestionID; //fuck
                        }
                    }
                    var nextQ = db.Questions.Find(pa.NextQuestionID); //entity framework \o/
                    if (nextQ.QuestionID == currentQ.QuestionID) //:(
                    {
                        if (nextQ != null)
                        {
                            nextQ.QuestionNumber++;
                        }
                    }
                    if (nextQ != null)
                    {
                        return RedirectToAction("Question", new { EnqueteID = nextQ.EnqueteID, QuestionNumber = nextQ.QuestionNumber });
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

        [AllowAnonymous]
        public ActionResult UrlLogin(string guid)
        {
            if (WebSecurity.IsAuthenticated)
            {
                WebSecurity.Logout();
                return RedirectToAction("UrlLogin");
            }
            var users = db.Users.Where(u => u.GUID.Equals(guid));
            if (users.Count() != 0)
            {
                var lm = new RegisterModel();
                var u = users.First();
                lm.Email = u.Email;
                return View(lm);
            }
            return RedirectToAction("Index","Account");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UrlLogin(LoginModel lm)
        {
            if (WebSecurity.UserExists(lm.Email))
            {
                WebSecurity.ChangePassword(lm.Email, "heelErgGeheimPasswordWatNiemandMagWeten", lm.Password);
                WebSecurity.Login(lm.Email, lm.Password);
                return RedirectToAction("RemoveGuid", "TakeEnquete");
            }
            return RedirectToAction("Index", "Account");
        }

        public ActionResult RemoveGuid()
        {
            var u = db.Users.Find(WebSecurity.CurrentUserId);
            u.GUID = null;
            db.SaveChanges();
            return RedirectToAction("Index", "TakeEnquete");
        }
    }
}
