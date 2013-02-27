using Emina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Emina.Controllers
{
    [Authorize]
    public class MailingController : Controller
    {

        private EminaContext db = new EminaContext();
        
        public ActionResult Index(int EnqueteID)
        {
            ViewBag.EnqueteID = EnqueteID;
            return View(db.Enrollments.Where(e => e.EnqueteID == EnqueteID).Select(e => e.User));
        }

        [HttpPost]
        public ActionResult Add(User user, int EnqueteID)
        {
            if (!db.Enrollments.Where(en => en.UserID == WebSecurity.CurrentUserId && en.role == EnrollmentRole.Owner).Select(en => en.EnqueteID).Contains(EnqueteID))
            {
                return RedirectToAction("Index", "EnqueteBuilder");
            }
            if (!WebSecurity.UserExists(user.Email))
            {
                WebSecurity.CreateUserAndAccount(user.Email, "heelErgGeheimPasswordWatNiemandMagWeten");
                user = db.Users.Where(u => u.Email == user.Email).First();
                user.GUID = Guid.NewGuid().ToString();
            }
            else
            {
                user = db.Users.Where(u => u.Email.Equals(user.Email)).First();
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
            e.role = EnrollmentRole.User;
            db.Enrollments.Add(e);
            db.SaveChanges();
            return RedirectToAction("Index", new { EnqueteID = EnqueteID } );
        }

        private Random r = new Random();

        public string getRandomPassword(int length)
        {
            var sb = new StringBuilder(length);
            for (var a = 0; a < sb.Capacity; ++a)
            {
                sb.Append((char)(r.Next(26) + 65));
            }
            return sb.ToString();
        }

        public ActionResult Delete(int EnqueteID, int UserID)
        {
            if (!db.Enrollments.Where(e => e.UserID == WebSecurity.CurrentUserId && e.role == EnrollmentRole.Owner).Select(e => e.EnqueteID).Contains(EnqueteID))
            {
                return RedirectToAction("Index", "EnqueteBuilder");
            }
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
