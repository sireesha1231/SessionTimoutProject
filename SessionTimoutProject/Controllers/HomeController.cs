using Microsoft.AspNet.Identity;
using SessionTimoutProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SessionTimoutProject.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string getSessionTimeOutDuration()
        {
            var claimsIdentity = base.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                Claim providerKeyClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if (providerKeyClaim != null)
                {
                    var date = claimsIdentity.FindFirst("IssueDate");
                    DateTime dt = DateTime.ParseExact(date.Value.ToString(), "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    if (DateTime.Compare(dt.AddMinutes(Constant.SessionTimeOutDuration), DateTime.Now) <= 0)
                    {
                        base.HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        return "LOGOUT";
                    }
                    else
                    {
                        return dt.AddMinutes(Constant.SessionTimeOutDuration).Subtract(DateTime.Now).TotalMinutes.ToString();
                    }
                }
                else
                {
                    return "NOSESSION";
                }
            }
            else
            {
                return "NOSESSION";
            }
        }
    }
}