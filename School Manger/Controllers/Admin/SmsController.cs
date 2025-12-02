using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Models.PageView;
using SMS.Base;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class SmsController : Controller
    {
        private IParentService ParentService;
        private IUserService UserService;
        private ISMSService SMSService;
        public SmsController(IParentService parentService,IUserService userService,ISMSService sMSService)
        {
            ParentService = parentService;
            UserService = userService;
            SMSService = sMSService;
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

            return View("Index",new AdminSms
            {
                NonChildern = nonChildren,
                NonPaidParent = nonPaidParents,
                Users = users
            });
        }
        private Task<bool> SendAllTask; // to do handel task 
        [HttpPost]
        public async Task<IActionResult> SendSms(string mobiles,string Message)
        {
            if (mobiles == "All" && (SendAllTask == null || SendAllTask.IsCompleted))
            {
                SendAllTask = SendAll(Message);
                SendAllTask.Start();
            }
            else
            {
                string[] mobile = mobiles.Split(',');
                try
                {
                    SMSService.Send2All(mobile, Message);
                }
                catch (Exception ex)
                {
                    ControllerExtensions.ShowError(this, $"خطا در ارسال", $"{ex.Message}");
                }
                ControllerExtensions.ShowSuccess(this, "موفق", "عملیات به پایان رسید");
            }
            return await Index();
        }
        public async Task<bool> SendAll(string Message)
        {
            List<string> Mobiles = UserService.GetAllParents().Select(x=>x.Mobile).ToList();
            try
            {
                SMSService.Send2All(Mobiles.ToArray(), Message);
            }
            catch(Exception ex)
            {
                ControllerExtensions.ShowError(this, $"خطا در ارسال", $"{ex.Message}");
            }
            ControllerExtensions.ShowSuccess(this, "موفق", "عملیات به پایان رسید");
            return true;
        }
    }
}
