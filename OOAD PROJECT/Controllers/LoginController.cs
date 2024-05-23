using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OOAD_PROJECT.Models;

namespace OOAD_PROJECT.Controllers
{


    public class LoginController : Controller
    {
        ProjectContext db = new ProjectContext();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginAll u, string returnUrl)
        {
            var dataitem = db.LoginAlls.Where(x => x.Username == u.Username && x.Passward == u.Passward).First();
            if (dataitem != null)
            {
                FormsAuthentication.SetAuthCookie(dataitem.Username, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid User/Pass");
                return View();
            }
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }

    }


}