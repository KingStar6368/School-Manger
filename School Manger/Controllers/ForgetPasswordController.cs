using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Extension;

namespace School_Manger.Controllers
{
    public class ForgetPasswordController : Controller
    {
        private readonly IUserService _userService;
        public ForgetPasswordController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult OTPConfirmation(string PhoneNumber)
        {
            if(_userService.IsMobileRegistered(PhoneNumber))
            {
                // Generate OTP and send it to the user's phone number
                ControllerExtensions.AddKey(this,"PhoneNumber", PhoneNumber);
                return View("OTPConfirmation");
            }
            else
            {
                ControllerExtensions.ShowError(this,"خطا","این شماره تلفن ثبت نشده است.");
                return View("Index");
            }
        }
        [HttpPost]
        public IActionResult OTPConfirmation(string otpCode,bool post = true)
        {
            // Validate the OTP
            if (true) // Replace with actual OTP validation logic
            {
                ControllerExtensions.ShowSuccess(this, "موفق", "رمز عبور موقت ارسال شد.");
                return RedirectToAction("ResetPassword");
            }
            else
            {
                ControllerExtensions.ShowError(this, "خطا", "کد تایید نادرست است.");
                return View("OTPConfirmation");
            }
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(string confirmPasswordHelp)
        {
            string PhoneNumber = ControllerExtensions.GetKey<string>(this, "PhoneNumber");
            var User = _userService.GetUserByMobile(PhoneNumber);
            if(_userService.UpdateUser(new UserUpdateDTO
            {
                Id = User.Id,
                FirstName = User.FirstName,
                LastName = User.LastName,
                IsActive = true,
                Mobile = User.Mobile,
                UserName = User.UserName,
                PasswordHash = confirmPasswordHelp.ConvertPersianToEnglish()
            }))
            {
                ControllerExtensions.ShowSuccess(this, "موفق", "رمز عبور با موفقیت تغییر کرد.");
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ControllerExtensions.ShowError(this, "خطا", "خطایی در تغییر رمز عبور رخ داد.");
            }
            return View();
        }
    }
}
