using Emina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emina.Controllers
{
    public class MailingController : Controller
    {

        private EminaContext db = new EminaContext();
        
        public ActionResult Index(int EnqueteID)
        {
            ViewBag.EnqueteID = EnqueteID;
            return View(db.Enrollments.Where(e => e.EnqueteID == EnqueteID).Select(e => e.User));
        }

        public ActionResult Add(User user, int EnqueteID)
        {
            var users = db.Users.Where(u => u.Email.Equals(user.Email));
            if (users.Count() == 0)
            {
                user.GUID = Guid.NewGuid().ToString();
                user.UserID = (((int?)db.Users.Max(u => u.UserID) ?? 0) + 1); //blegh
                db.Users.Add(user);
                db.SaveChanges();
            }
            if (user.BirthDate != null)
            {
                if (user.BirthDate.Value.Year < 1900)
                {
                    user.BirthDate = null;
                }
            }
            var e = new Enrollment();
            e.EnqueteID = EnqueteID;
            e.UserID = user.UserID;
            db.Enrollments.Add(e);
            db.SaveChanges();
            return RedirectToAction("Index", new { EnqueteID = EnqueteID } );
        }

        public ActionResult Delete(int EnqueteID, int UserID)
        {
            var enrollments = db.Enrollments.Where(e => e.EnqueteID == EnqueteID && e.UserID == UserID);
            if (enrollments.Count() != 0)
            {
                db.Enrollments.Remove(enrollments.First());
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { EnqueteID = EnqueteID });
        }
    }
}
