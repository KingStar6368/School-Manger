using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Enums;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class UserManagementController : Controller
    {
        private static List<LoginUser> _users = new()
    {
        new LoginUser { Id = 1, UserName = "parent1", Password = "123", PhoneNumber = "09123456789", Type = UserType.Parent },
        new LoginUser { Id = 2, UserName = "driver1", Password = "123", PhoneNumber = "09129876543", Type = UserType.Driver }
    };

        public IActionResult Parents()
        {
            var parents = _users.Where(u => u.Type == UserType.Parent).ToList();
            return View(parents);
        }

        public IActionResult Drivers()
        {
            var drivers = _users.Where(u => u.Type == UserType.Driver).ToList();
            return View(drivers);
        }

        public IActionResult Create(UserType type)
        {
            ViewBag.UserType = type;
            return View();
        }

        [HttpPost]
        public IActionResult Create(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                user.Id = _users.Max(u => u.Id) + 1;
                _users.Add(user);
                return RedirectToAction(user.Type == UserType.Parent ? "Parents" : "Drivers");
            }
            ViewBag.UserType = user.Type;
            return View(user);
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
