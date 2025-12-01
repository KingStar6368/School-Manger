using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Models.PageView;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class SmsController : Controller
    {
        private IParentService ParentService;
        private IUserService UserService;
        public SmsController(IParentService parentService,IUserService userService)
        {
            ParentService = parentService;
            UserService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var nonChildren = await ParentService.GetParentsWithNoChildren();
            var nonPaidParents = await ParentService.GetNonPiadParents(true);

            var parentIds = nonChildren
                .Select(x => x.Id)
                .Concat(nonPaidParents.Select(x => x.Id))
                .Distinct()
                .ToList();

            List<UserDTO> users = new List<UserDTO>();
            foreach (var id in parentIds)
            {
                users.Add(UserService.GetUserByParent(id));
            }

            return View(new AdminSms
            {
                NonChildern = nonChildren,
                NonPaidParent = nonPaidParents,
                Users = users
            });
        }

    }
}
