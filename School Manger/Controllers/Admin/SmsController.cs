using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Models;
using School_Manger.Models.PageView;
using SMS.Base;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class SmsController : Controller
    {
        private IParentService ParentService;
        private IUserService UserService;
        private ISMSService SMSService;
        private readonly ISmsQueue _smsQueue;
        public SmsController(IParentService parentService,IUserService userService,ISMSService sMSService, ISmsQueue smsQueue)
        {
            ParentService = parentService;
            UserService = userService;
            SMSService = sMSService;
            _smsQueue = smsQueue;
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
        [HttpPost]
        public async Task<IActionResult> SendSms(string mobiles, string Message)
        {
            if (mobiles == "All")
            {
                if (_smsQueue.IsBusy)
                {
                    ControllerExtensions.ShowError(this, "درحال ارسال", "درخواست قبلی هنوز ادامه دارد.");
                }
                else
                {
                    _smsQueue.Enqueue(Message);
                    ControllerExtensions.ShowSuccess(this, "ارسال شروع شد", "پیامک‌ها در پس‌ زمینه ارسال می‌شوند.");
                }
            }
            else
            {
                try
                {
                    var mobileList = mobiles.Split(',');
                    SMSService.Send2All(mobileList, Message);

                    ControllerExtensions.ShowSuccess(this, "موفق", "پیامک‌ها ارسال شدند");
                }
                catch (Exception ex)
                {
                    ControllerExtensions.ShowError(this, "خطا", ex.Message);
                }
            }

            return await Index();
        }
    }
}
