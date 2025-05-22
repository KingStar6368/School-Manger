using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Manger.Models;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class ParentsController : Controller
    {
        List<Parent> TestModel = new List<Parent>()
{
    new Parent()
    {
        Id = 1,
        ParentFirstName = "محمد",
        ParentLastName = "احمدی",
        ParentNationalCode = "1234567890",
        Children = new List<ChildInfo>
        {
            new ChildInfo
            {
                Id = 1,
                FirstName = "علی",
                LastName = "احمدی",
                NationalCode = "1234567891",
                BirthDate = new DateTime(2015, 3, 10),
                Class = "اول دبستان",
                HasPaid = true,
                Path = new LocationPairModel
                {
                    Location1 = new LocationData
                    {
                        Address = "تهران، خیابان آزادی، کوچه گلستان، پلاک 12"
                    },
                    Location2 = new LocationData
                    {
                        Address = "مدرسه نمونه دولتی نیکان"
                    }
                }
            }
        }
    },
    new Parent()
    {
        Id = 2,
        ParentFirstName = "فاطمه",
        ParentLastName = "رضایی",
        ParentNationalCode = "0987654321",
        Children = new List<ChildInfo>
        {
            new ChildInfo
            {
                Id = 2,
                FirstName = "سارا",
                LastName = "رضایی",
                NationalCode = "0987654322",
                BirthDate = new DateTime(2013, 7, 15),
                Class = "سوم دبستان",
                HasPaid = true,
                Path = new LocationPairModel
                {
                    Location1 = new LocationData
                    {
                        Address = "تهران، خیابان ولیعصر، پلاک 345"
                    },
                    Location2 = new LocationData
                    {
                        Address = "مدرسه غیرانتفاعی مهر"
                    }
                }
            },
            new ChildInfo
            {
                Id = 3,
                FirstName = "امیر",
                LastName = "رضایی",
                NationalCode = "0987654323",
                BirthDate = new DateTime(2017, 11, 5),
                Class = "پیش دبستانی",
                HasPaid = false,
                Path = new LocationPairModel
                {
                    Location1 = new LocationData
                    {
                        Address = "تهران، خیابان ولیعصر، پلاک 345"
                    },
                    Location2 = new LocationData
                    {
                        Address = "مهدکودک و پیش دبستانی آفتاب"
                    }
                }
            }
        }
    }
};
        public IActionResult Index()
        {
            return View(TestModel);
        }
        public IActionResult Details(int id)
        {
            var Model = TestModel.FirstOrDefault(x => x.Id == id);
            return View(Model);
        }
    }
}
