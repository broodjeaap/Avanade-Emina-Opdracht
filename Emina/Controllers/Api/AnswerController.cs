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
    public class AnswerController : ApiController
    {
        private EminaContext db = new EminaContext();

        // GET api/Answer
        public IEnumerable<Answer> GetAnswers()
        {
            return db.Answers.AsEnumerable();
        }

        // GET api/Answer/5
        public Answer GetAnswer(int id)
        {
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return answer;
        }

        // PUT api/Answer/5
        public HttpResponseMessage PutAnswer(int id, Answer answer)
        {
            if (ModelState.IsValid && id == answer.AnswerID)
            {
                db.Entry(answer).State = EntityState.Modified;

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

        // POST api/Answer
        public HttpResponseMessage PostAnswer(Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Answers.Add(answer);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, answer);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = answer.AnswerID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Answer/5
        public HttpResponseMessage DeleteAnswer(int id)
        {
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Answers.Remove(answer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, answer);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}