using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnionArchitecture.Services.Interfaces.Common;

namespace OnionArchitecture.UI.Web.Controllers
{
    [Authorize]
    public class PermissionController : Controller
    {
        private readonly IManagePermissionService _managePermissionService;

        public PermissionController(IManagePermissionService managePermissionService)
        {
            _managePermissionService = managePermissionService;
        }

        public ActionResult Index()
        {
            var users = _managePermissionService.FindAllUsers();

            return View(users);
        }
	}
}