using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emina.Models;
using WebMatrix.WebData;
using Emina.Filters;

namespace Emina.Controllers
{
    public class HomeController : Controller
    {

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            System.Diagnostics.Debug.WriteLine(model.Email + ", " + model.Password);
            /*
            if (ModelState.IsValid && WebSecurity.Login(model.Email, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "The email or password provided is incorrect.");
             * */
            return View(model);
        }

    }
}
