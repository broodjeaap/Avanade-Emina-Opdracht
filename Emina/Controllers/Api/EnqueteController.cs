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
    public class EnqueteController : ApiController
    {
        private EminaContext db = new EminaContext();

        // GET api/Enquete
        public IEnumerable<Enquete> GetEnquetes()
        {
            return db.Enquetes.AsEnumerable();
        }

        // GET api/Enquete/5
        public Enquete GetEnquete(int id)
        {
            Enquete enquete = db.Enquetes.Find(id);
            if (enquete == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return enquete;
        }

        // PUT api/Enquete/5
        public HttpResponseMessage PutEnquete(int id, Enquete enquete)
        {
            if (ModelState.IsValid && id == enquete.EnqueteID)
            {
                db.Entry(enquete).State = EntityState.Modified;

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

        // POST api/Enquete
        public HttpResponseMessage PostEnquete(Enquete enquete)
        {
            if (ModelState.IsValid)
            {
                db.Enquetes.Add(enquete);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, enquete);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = enquete.EnqueteID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Enquete/5
        public HttpResponseMessage DeleteEnquete(int id)
        {
            Enquete enquete = db.Enquetes.Find(id);
            if (enquete == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Enquetes.Remove(enquete);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, enquete);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}