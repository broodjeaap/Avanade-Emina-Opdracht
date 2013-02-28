using Emina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emina.Controllers
{
    public class ResultController : Controller
    {
        private EminaContext db = new EminaContext();

        public ActionResult Index(int EnqueteID)
        {
            var questions = db.Questions.Where(q => q.EnqueteID == EnqueteID).OrderBy(q => q.QuestionNumber).ToList();
            ViewBag.questions = questions;

            var answers = new Dictionary<int, List<OrderByResult>>();
            foreach (var question in questions)
            {
                if (question.Type == QuestionType.Checkbox || question.Type == QuestionType.MultipleChoice)
                {
                    var tmp = new List<OrderByResult>(question.PossibleAnswers.Count());
                    foreach(var pa in question.PossibleAnswers)
                    {
                        tmp.Add(new OrderByResult { name = pa.Text, count = db.Answers.Where(a => a.QuestionID == question.QuestionID && a.PossibleAnswerID == pa.PossibleAnswerID).Count() } );
                    }
                    answers[question.QuestionNumber] = tmp;
                }
                else
                {
                    answers[question.QuestionNumber] = db.Answers.Where(a => a.QuestionID == question.QuestionID).GroupBy(a => a.TextAnswer).Select(n => new OrderByResult { name = n.Key, count = n.Count() }).ToList();
                }
            }
            ViewBag.answers = answers;
            return View(db.Answers.Where(a => a.EnqueteID == EnqueteID));
        }

    }

    public class OrderByResult
    {
        public string name;
        public int count;
    }
}
