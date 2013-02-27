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
    public class PossibleAnswerController : ApiController
    {
        private EminaContext db = new EminaContext();

        // GET api/PossibleAnswer
        public IEnumerable<PossibleAnswer> GetPossibleAnswers()
        {
            return db.PossibleAnswers.AsEnumerable();
        }

        // GET api/PossibleAnswer/5
        public PossibleAnswer GetPossibleAnswer(int id)
        {
            PossibleAnswer possibleanswer = db.PossibleAnswers.Find(id);
            if (possibleanswer == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return possibleanswer;
        }

        // PUT api/PossibleAnswer/5
        public HttpResponseMessage PutPossibleAnswer(int id, PossibleAnswer possibleanswer)
        {
            if (ModelState.IsValid && id == possibleanswer.PossibleAnswerID)
            {
                db.Entry(possibleanswer).State = EntityState.Modified;

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

        // POST api/PossibleAnswer
        public HttpResponseMessage PostPossibleAnswer(PossibleAnswer possibleanswer)
        {
            if (ModelState.IsValid)
            {
                db.PossibleAnswers.Add(possibleanswer);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, possibleanswer);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = possibleanswer.PossibleAnswerID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/PossibleAnswer/5
        public HttpResponseMessage DeletePossibleAnswer(int id)
        {
            PossibleAnswer possibleanswer = db.PossibleAnswers.Find(id);
            if (possibleanswer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.PossibleAnswers.Remove(possibleanswer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, possibleanswer);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}