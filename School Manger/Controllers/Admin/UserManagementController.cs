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
        private static List<LoginUser> _users = new()
    {
        new LoginUser { Id = 1, UserName = "parent1", Password = "123", PhoneNumber = "09123456789", Type = UserType.Parent },
        new LoginUser { Id = 2, UserName = "driver1", Password = "123", PhoneNumber = "09129876543", Type = UserType.Driver }
    };

        public async Task<IActionResult> Parents()
        {
            var parents = await _userService.GetAllAsyncParents();
            return View(parents);
        }

        public async Task<IActionResult> Drivers()
        {
            var drivers = await _userService.GetAllAsyncDrivers();
            return View(drivers);
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

        public IActionResult Edit(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var existing = _users.FirstOrDefault(u => u.Id == user.Id);
                if (existing != null)
                {
                    existing.UserName = user.UserName;
                    existing.Password = user.Password;
                    existing.PhoneNumber = user.PhoneNumber;
                }
                return RedirectToAction(user.Type == UserType.Parent ? "Parents" : "Drivers");
            }
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
            }
            return RedirectToAction(user.Type == UserType.Parent ? "Parents" : "Drivers");
        }
    }
}
