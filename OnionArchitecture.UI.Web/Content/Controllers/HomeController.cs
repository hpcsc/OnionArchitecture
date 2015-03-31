using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnionArchitecture.Services.Interfaces.Common;

namespace OnionArchitecture.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private IAuthenticateService _authenticateService;

        public HomeController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (_authenticateService.IsValidUser(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return RedirectToAction("Index", "Permission");
            }

            TempData["Message"] = "Invalid username or password";
            return RedirectToAction("Login");
        }
    }
}