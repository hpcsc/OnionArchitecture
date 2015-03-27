using System.Web.Mvc;
using OnionArchitecture.Services.Interfaces.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;
using OnionArchitecture.UI.Web.Helpers;
using OnionArchitecture.UI.Web.Helpers.Alerts;

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

        [HandleBusinessException]
        public ActionResult UpdateUserRolesAndPermission()
        {
            _managePermissionService.UpdateUserRolesAndPermission(new UpdateUserRolesAndPermissionInputModel());

            return RedirectToAction("Index").WithSuccess("User updated successfully");
        }
	}
}