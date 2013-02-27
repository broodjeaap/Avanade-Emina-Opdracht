using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Emina.Models;

namespace Emina.Controllers.Api
{
    public class QuestionController : ApiController
    {
        private EminaContext db = new EminaContext();

        // GET api/Question
        public IEnumerable<Question> GetQuestions()
        {
            var questions = db.Questions.Include(q => q.Enquete);
            return questions.AsEnumerable();
        }

        // GET api/Question/5
        public Question GetQuestion(int id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return question;
        }

        // PUT api/Question/5
        public HttpResponseMessage PutQuestion(int id, Question question)
        {
            if (ModelState.IsValid && id == question.QuestionID)
            {
                db.Entry(question).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Question
        public HttpResponseMessage PostQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, question);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = question.QuestionID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Question/5
        public HttpResponseMessage DeleteQuestion(int id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Questions.Remove(question);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, question);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}