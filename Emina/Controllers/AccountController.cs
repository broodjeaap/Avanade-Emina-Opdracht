using Emina.Filters;
using Emina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Emina.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
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
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    throw new Exception("UserController "+e.Message);
                }
            }
            return View(lm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index");
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
