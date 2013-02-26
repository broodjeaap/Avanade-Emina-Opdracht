using Emina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Emina.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel lm)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.Login(lm.Email, lm.Password, persistCookie: lm.RememberMe);
                return RedirectToAction("LoginResult");
            }
            return RedirectToAction("Index");
        }

        public string LoginResult()
        {
            return "Woei "+ WebSecurity.CurrentUserName;
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
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount("test@test.com", "password");
                    WebSecurity.Login(lm.Email, lm.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    throw new Exception("UserController "+e.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            //return View(lm);
            return RedirectToAction("RegisterResult");
        }

        public string RegisterResult()
        {
            return "Woei " + WebSecurity.CurrentUserName;
        }
    }
}
