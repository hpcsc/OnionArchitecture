using OnionArchitecture.Services.Interfaces.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;
using OnionArchitecture.UI.Web.Helpers;
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

        public ActionResult SearchUser(string input)
        {
            var users = _managePermissionService.SearchUser(input);
            return JsonNet(users);
        }

        [HttpPost]
        [HandleBusinessException(ForAjaxRequest = true)]
        public ActionResult UpdateResource(UpdateResourceInputModel input)
        {
            input.UserId = CurrentUser.UserId;

            _managePermissionService.UpdateResource(input);

            return JsonNet(new { Success = true });
        }

        [HttpPost]
        [HandleBusinessException(ForAjaxRequest = true)]
        public ActionResult AddResource(AddResourceInputModel input)
        {
            input.UserId = CurrentUser.UserId;

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

            return JsonNet(model);
        }

        [HandleBusinessException(ForAjaxRequest = true)]
        public ActionResult UpdateUserRolesAndPermission(UpdateUserRolesAndPermissionInputModel input)
        {
            input.UserId = CurrentUser.UserId;

            _managePermissionService.UpdateUserRolesAndPermission(input);

            return JsonNet(new
            {
                success = true
            });
        }
    }
}