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
                if (question.Type == QuestionType.MultipleChoice)
                {
                    var tmp = new List<OrderByResult>(question.PossibleAnswers.Count());
                    foreach(var pa in question.PossibleAnswers)
                    {
                        tmp.Add(new OrderByResult { name = pa.Text, count = db.Answers.Where(a => a.QuestionID == question.QuestionID && a.PossibleAnswerID == pa.PossibleAnswerID).Count() } );
                    }
                    answers[question.QuestionNumber] = tmp;
                }
                else if (question.Type == QuestionType.Checkbox)
                {
                    var dic = new Dictionary<string, string>();
                    var dic2 = new Dictionary<string, int>();
                    
                    foreach (var pa in question.PossibleAnswers)
                    {
                        dic[pa.PossibleAnswerID.ToString()] = pa.Text;
                        dic2[pa.Text] = 0;
                    }
                    var tmp = db.Answers.Where(q => q.EnqueteID == EnqueteID && q.QuestionID == question.QuestionID).Select(q => q.TextAnswer).ToList();
                    foreach (var a in tmp)
                    {
                        var split = a.Split(';');
                        foreach (var s in split)
                        {
                            dic2[dic[s]]++;
                        }
                    }
                    var list = new List<OrderByResult>();
                    foreach (var k in dic2.Keys)
                    {
                        list.Add(new OrderByResult { name = k, count = dic2[k] });
                    }
                    answers[question.QuestionNumber] = list;
                }
                else
                {
                    answers[question.QuestionNumber] = db.Answers.Where(a => a.QuestionID == question.QuestionID).GroupBy(a => a.TextAnswer).Select(n => new OrderByResult { name = n.Key, count = n.Count() }).ToList();
                }
            }
            ViewBag.answers = answers;
            return View();
        }

    }

    public class OrderByResult
    {
        public string name;
        public int count;
    }
}
