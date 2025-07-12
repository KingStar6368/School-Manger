using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Enums;
using School_Manager.Domain.Entities.Catalog.Operation;
using School_Manger.Extension;
using School_Manger.Models.PageView;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class UserManagementController : Controller
    {
        private readonly IUserService _userService;
        private readonly IParentService _parentService;
        private readonly IDriverService _driverService;
        public UserManagementController(IUserService userService, IParentService parentService, IDriverService driverService)
        {
            _userService = userService;
            _parentService = parentService;
            _driverService = driverService;
        }

        public async Task<IActionResult> Parents()
        {
            var parents = await _userService.GetAllAsyncParents();
            return View("Parents", parents);
        }

        public async Task<IActionResult> Drivers()
        {
            var drivers = await _userService.GetAllAsyncDrivers();
            return View("_UsersTable", drivers);
        }

        public IActionResult Create(UserType type)
        {
            ViewBag.UserType = type;
            return View();
        }

        [HttpPost]
        public IActionResult Create(AdminUser Data,string BirthDate,
            string Last_Digits,string Third,string PChar,string First)
        {
            try
            {
                var UserRef = _userService.CreateUser(new UserCreateDTO()
                {
                    FirstName = Data.User.FirstName,
                    LastName = Data.User.LastName,
                    UserName = Data.User.UserName,
                    PasswordHash = Data.User.Password,
                    IsActive = true,
                    Mobile = Data.User.Mobile
                });
                switch (Data.Type)
                {
                    case UserType.Parent:
                        _parentService.CreateParent(new ParentCreateDto()
                        {
                            FirstName = Data.User.FirstName,
                            LastName = Data.User.LastName,
                            NationalCode = Data.User.UserName,
                            Active = true,
                            Address = "",
                            UserRef = UserRef
                        });
                        break;
                    case UserType.Driver:
                        _driverService.CreateDriver(new DriverCreateDto()
                        {
                            Name = Data.User.FirstName,
                            LastName = Data.User.LastName,
                            NationCode = Data.User.UserName,
                            UserRef = UserRef,
                            Address = Data.Driver.Address,
                            AvailableSeats = Data.Driver.Car.SeatNumber,
                            BankRef = 0,
                            BankAccountNumber = Data.Driver.BankAccountNumber,
                            Code = Data.Driver.Code,
                            BirthDate = BirthDate.ConvertPersianToEnglish().ToMiladi(),
                            CertificateId = "0",
                            Descriptions = Data.Driver.Descriptions,
                            Education = Data.Driver.Education,
                            FatherName = Data.Driver.FutherName,
                            Rate = Data.Driver.Rate,
                            Warnning = Data.Driver.Warnning,
                            CarCreateDto = new CarCreateDto()
                            {
                                Name = Data.Driver.Car.Name,
                                SeatNumber = Data.Driver.Car.SeatNumber,
                                carType = (int)Data.Driver.Car.carType,
                                ChrPlateNumber = PChar,
                                ColorCode = 0,
                                FirstIntPlateNumber = int.Parse(First),
                                SecondIntPlateNumber = int.Parse(Third),
                                ThirdIntPlateNumber = int.Parse(Last_Digits),
                                IsActive = true,
                            },
                            Latitude = 0,
                            Longitude = 0
                        });
                        break;

                }
                ControllerExtensions.ShowSuccess(this, "موفق", "کاربر ساخته شد");
                ViewBag.UserType = Data.Type;
                return View();
            }
            catch(Exception ex)
            {
                ControllerExtensions.ShowError(this, "خطا", ex.Message);
                ViewBag.UserType = Data.Type;
                return View();
            }
        }

        public async Task<IActionResult> DeleteDriver(long Id)
        {
            try
            {
                if (_driverService.DeleteDriverByUserId(Id))
                    ControllerExtensions.ShowSuccess(this, "موفق", "راننده حذف شد");
                else
                    ControllerExtensions.ShowError(this, "خطا", "مشکلی پیش آمده");
            }
            catch (Exception ex)
            {
                ControllerExtensions.ShowError(this, "خطا", "این راننده اطلاعات وابسته دارد امکان حذف آن ممکن نیست");
            }
            return await Drivers();
        }
        public async Task<IActionResult> DeleteParent(long Id)
        {
            try
            {
                if (_parentService.DeleteParentByUserId(Id))
                    ControllerExtensions.ShowSuccess(this, "موفق", "والد حذف شد");
                else
                    ControllerExtensions.ShowError(this, "خطا", "مشکلی پیش آمده");
            }
            catch (Exception ex)
            {
                ControllerExtensions.ShowError(this, "خطا", "این والد اطلاعات وابسته دارد امکان حذف آن ممکن نیست");
            }
            return await Parents();
        }
    }
}
