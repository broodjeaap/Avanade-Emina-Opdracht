﻿using Emina.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emina.Controllers
{
    [Authorize]
    public class EnqueteBuilderController : Controller
    {
        private EminaContext db = new EminaContext();

        //
        // GET: /EnqueteBuilder/

        public ActionResult Index()
        {
            return View(db.Enquetes.ToList());
        }

        public ActionResult NewEnquete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewEnquete(Enquete enquete)
        {
            if(ModelState.IsValid)
            {
                db.Enquetes.Add(enquete);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteEnquete(int id)
        {
            var enquete = db.Enquetes.Find(id);
            if (enquete != null)
            {
                db.Enquetes.Remove(enquete);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditEnquete(int id)
        {
            var e = db.Enquetes.Find(id);
            if (e != null)
            {
                return View(db.Enquetes.Find(id));
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditQuestions(int id)
        {
            var Enquete = db.Enquetes.Find(id);
            if (Enquete == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.QuestionTypes = Enum.GetValues(typeof(QuestionType));
            Dictionary<int, IEnumerable<PossibleAnswer>> possibleAnswers = new Dictionary<int, IEnumerable<PossibleAnswer>>();
            foreach (var q in Enquete.Questions)
            {
                if (q.Type == QuestionType.MultipleChoice || q.Type == QuestionType.Checkbox)
                {
                    possibleAnswers[q.QuestionNumber] = q.PossibleAnswers;
                }
            }
            ViewBag.PossibleAnswers = possibleAnswers;
            Enquete.Questions = Enquete.Questions.OrderBy(q => q.QuestionNumber).ToList();
            return View(Enquete);
        }

        [HttpPost]
        public ActionResult EditQuestions(FormCollection collection)
        {
            Enquete e = db.Enquetes.Find(int.Parse(collection["Enquete_id"]));
            foreach(Question q in e.Questions)
            {
                if (q.Type == QuestionType.Checkbox || q.Type == QuestionType.MultipleChoice)
                {
                    for (int a = q.PossibleAnswers.Count - 1; a >= 0;--a)
                    {
                        db.PossibleAnswers.Remove(q.PossibleAnswers.ElementAt(a));
                    }
                }
            }
            db.SaveChanges();
            var questionCount = int.Parse(collection["questionCount"]);
            questionCount--;
            var questions = new Dictionary<string,Question>();
            for (var a = 1;a <= questionCount;++a)
            {
                var c = e.Questions.Where(qu => qu.QuestionNumber == a);
                Question q;
                if (c.Count() == 0)
                {
                    q = new Question();
                }
                else
                {
                    q = c.First();
                    db.Entry(q).State = EntityState.Modified;
                }
                q.EnqueteID = e.EnqueteID;
                q.Enquete = e;
                q.QuestionNumber = a;
                q.Text = collection["Question_" + a + "_Text"]; //Question_1_Text
                q.Type = (QuestionType)Enum.Parse(typeof(QuestionType),collection["Question_" + a + "_Type"]); //Question_1_Type
                questions.Add(a.ToString(), q);
            }
            var allPossibleAnswers = new Dictionary<int, ICollection<PossibleAnswer>>();
            for (var a = 1; a <= questionCount; ++a)//Question_1_Next
            {
                var q = questions[a.ToString()];
                int tmp;
                if (int.TryParse(collection["Question_" + a + "_Next"],out tmp))
                {
                    q.NextQuestionID = questions[tmp+""].QuestionID;
                    q.NextQuestion = questions[tmp+""];
                }
                if (q.Type == QuestionType.MultipleChoice || q.Type == QuestionType.Checkbox) // type == multiplechoice || type == checkbox
                {
                    var answerCount = int.Parse(collection["Question_" + a + "_AnswerCount"]);
                    List<PossibleAnswer> possibleAnswers = new List<PossibleAnswer>();
                    for (var b = 1; b <= answerCount; ++b) //Question_1_AnswerCount
                    {
                        var possibleAnswer = new PossibleAnswer();
                        possibleAnswer.Text = collection["Question_" + a + "_Answer_" + b + "_Text"]; //Question_1_Answer_1_Text
                        if (int.TryParse(collection["Question_" + a + "_Answer_" + b + "_Next"], out tmp))
                        {
                            possibleAnswer.NextQuestionID = questions[tmp + ""].QuestionID;
                            possibleAnswer.NextQuestion = questions[tmp + ""];
                        }
                        else
                        {
                            if (int.TryParse(collection["Question_" + a + "_Next"], out tmp))
                            {
                                possibleAnswer.NextQuestionID = questions[a + ""].QuestionID;
                                possibleAnswer.NextQuestion = questions[a + ""];
                            }
                            
                        }
                        possibleAnswer.QuestionID = questions[a.ToString()].QuestionID;
                        possibleAnswer.Question = questions[a.ToString()];
                        possibleAnswers.Add(possibleAnswer);
                    }
                    q.PossibleAnswers = possibleAnswers;
                    allPossibleAnswers[q.QuestionNumber] = possibleAnswers;
                }
            }
            var questionList = new List<Question>(questionCount);
            for (var a = 1; a <= questionCount; ++a)
            {
                questionList.Insert(a - 1, questions[a.ToString()]);
                e.Questions = questionList;
            }
            if (ModelState.IsValid)
           
            {
                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.PossibleAnswers = allPossibleAnswers;
            ViewBag.QuestionTypes = Enum.GetValues(typeof(QuestionType));
            return View(e);
        }
    }
}
