using OnionArchitecture.Services.Interfaces.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;
using OnionArchitecture.UI.Web.Helpers;
using OnionArchitecture.UI.Web.Helpers.Alerts;
using System.Web.Mvc;

namespace OnionArchitecture.UI.Web.Controllers
{
	[Authorize]
	public class PermissionController : ControllerBase
	{
		private readonly IManagePermissionService _managePermissionService;

		public PermissionController(IManagePermissionService managePermissionService)
		{
			_managePermissionService = managePermissionService;
		}

		public ActionResult GetInitialIndexModel()
		{
			var model = _managePermissionService.CreateIndexModel();
			return JsonNet(model);
		}

		public ActionResult GetResourceDetail(int id)
		{
			var model = _managePermissionService.GetResourceDetail(id);

			return JsonNet(model);
		}

		[HttpPost]
		[HandleBusinessException(ForAjaxRequest = true)]
		public ActionResult UpdateResource(UpdateResourceInputModel input)
		{
			_managePermissionService.UpdateResource(input);

			return JsonNet(new {Success = true});
		}

		[HttpPost]
		[HandleBusinessException(ForAjaxRequest = true)]
		public ActionResult AddResource(AddResourceInputModel input)
		{
			_managePermissionService.AddResource(input);

			return JsonNet(new { Success = true });
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult GetUserPermission(string username)
		{
			var model = _managePermissionService.GetUserPermission(username);

			return Json(model, JsonRequestBehavior.AllowGet);
		}

		[HandleBusinessException]
		public ActionResult UpdateUserRolesAndPermission()
		{
			_managePermissionService.UpdateUserRolesAndPermission(new UpdateUserRolesAndPermissionInputModel());

			return RedirectToAction("Index").WithSuccess("User updated successfully");
		}
	}
}