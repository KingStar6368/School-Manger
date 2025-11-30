using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Enums;
using SMS.Base;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DebtorController : Controller
    {
        private readonly IParentService _parentService;
        private readonly IChildService _childService;
        private readonly ISchoolService _schoolService;
        private readonly IDriverService _driverService;
        private readonly IBillService _billService;
        private readonly ISMSService _SMSService;
        private readonly ISMSLogService _SMSLogService;
        private readonly IUserService _UserService;
        private readonly IAppConfigService _appConfigService;
        public DebtorController(IParentService parentService,IChildService childService
            ,IDriverService driverService,ISchoolService schoolService,IBillService billService
            ,ISMSService SMSService,ISMSLogService SMSLogService,IUserService UserService
            ,IAppConfigService appConfigService)
        {
            _parentService = parentService;
            _childService = childService;
            _driverService = driverService;
            _schoolService = schoolService;
            _billService = billService;
            _SMSService = SMSService;
            _UserService = UserService;
            _SMSLogService = SMSLogService;
            _appConfigService = appConfigService;
        }
        public async Task<IActionResult> Index()
        {
            return View("Index", await _childService.GetDebtors());
        }
        [HttpPost]
        public async Task<IActionResult> SendWarningSms(string selectedParentIds)
        {
            if (string.IsNullOrEmpty(selectedParentIds))
            {
                ControllerExtensions.ShowError(this, "خطا", "هیچ خانواده‌ای انتخاب نشده است.");
                return await Index();
            }
            var parentIds = selectedParentIds.Split(',')
                .Select(id => long.Parse(id))
                .ToList();

            foreach(long parentId in parentIds)
            {
                try
                {
                    var User = _UserService.GetUserByParent(parentId);
                    if (!_SMSService.Send(User.Mobile, $"خانواد گرامی لطفا قبض فرزند خود را پرداخت کنید \n لینک سامانه {_appConfigService.WebAddress()}"))
                        ControllerExtensions.ShowError(this, "خطا", $"مشکلی در ارسال پیام پیش آمده کد خانواده{parentId}");
                    else
                    {
                        _SMSLogService.CreateSMSLog(new SMSLogDto()
                        {
                            UserId = User.Id,
                            type = SMSType.Warnning,
                            SMSTime = DateTime.Now
                        });
                    }
                }
                catch(Exception ex)
                {
                    ControllerExtensions.ShowWarning(this, $"مشکلی در ارسال پیام پیش آمده کد خانواده{parentId}", ex.Message);
                }
            }

            return await Index();
        }
    }
}
