using Emina.Filters;
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
    public class AccountController : Controller
    {
        private EminaContext db = new EminaContext();

        public ActionResult Index()
        {
            return View(db.Users.Find(WebSecurity.CurrentUserId));
        }

        [HttpPost]
        public ActionResult Index(User u)
        {
            if (ModelState.IsValid)
            {
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel lm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.Login(lm.Email, lm.Password, persistCookie: lm.RememberMe);
                return RedirectToLocal(returnUrl);
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel lm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(lm.Email, lm.Password);
                    WebSecurity.Login(lm.Email, lm.Password);
                    return RedirectToAction("Index", "TakeEnquete");
                }
                catch (Exception e)
                {
                    throw new Exception("UserController: "+e.Message);
                }
            }
            return View(lm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
