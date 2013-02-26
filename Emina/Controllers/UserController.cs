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
            return "Woei "+ WebSecurity.IsAuthenticated;
        }

    }
}
