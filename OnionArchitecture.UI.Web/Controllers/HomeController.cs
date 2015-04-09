using OnionArchitecture.Services.Interfaces.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

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
                var model = _authenticateService.GetUserSerializationModelByUsername(username);
                StoreCustomPrincipalInCookie(model, false);

                return RedirectToAction("Index", "Permission");
            }

            TempData["Message"] = "Invalid username or password";
            return RedirectToAction("Login");
        }

        private void StoreCustomPrincipalInCookie(CustomPrincipalSerializationModel customPrincipal, bool rememberMe)
        {
            var serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(customPrincipal);

            var authTicket = new FormsAuthenticationTicket(1, customPrincipal.Username, DateTime.Now, DateTime.Now.AddMinutes(30), rememberMe, userData);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            Response.Cookies.Add(authCookie);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}