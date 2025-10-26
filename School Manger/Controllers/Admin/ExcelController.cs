using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using School_Manager.Core.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace School_Manger.Controllers.Admin
{
    [Area("Admin")]
    public class ExcelController : Controller
    {
        private readonly IParentService _parentService;
        private readonly IChildService _childService;
        private readonly IUserService _userService;
        public ExcelController(IChildService childService,IParentService parentService, IUserService userService)
        {
            _parentService = parentService;
            _childService = childService;
            _userService = userService;

            ExcelPackage.License.SetNonCommercialOrganization("Soft");
        }

        #region Index Pages
        public async Task<IActionResult> ExportParentIndex()
        {
            var Parents = await _parentService.GetParents();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Parents");

                worksheet.Cells[1, 1].Value = "شناسه";
                worksheet.Cells[1, 2].Value = "نام";
                worksheet.Cells[1, 3].Value = "نام خانوادگی";
                worksheet.Cells[1, 4].Value = "کد ملی";
                int i = 2;
                foreach (var Parent in Parents)
                {
                    worksheet.Cells[1, i].Value = Parent.Id;
                    worksheet.Cells[1, i].Value = Parent.ParentFirstName;
                    worksheet.Cells[1, i].Value = Parent.ParentLastName;
                    worksheet.Cells[1, i].Value = Parent.ParentNationalCode;
                    i++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);

                stream.Position = 0;
                return File(stream,
                           "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                           "Parents.xlsx");
            }
        }
        public async Task<IActionResult> ExportDriverUserIndex()
        {
            var Drivers = await _userService.GetAllAsyncDrivers();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DriverUsers");

                worksheet.Cells[1, 1].Value = "شناسه";
                worksheet.Cells[1, 2].Value = "نام کاربری";
                worksheet.Cells[1, 3].Value = "نام نام خانوادگی";
                worksheet.Cells[1, 4].Value = "موبایل";
                int i = 2;
                foreach (var Driver in Drivers)
                {
                    worksheet.Cells[1, i].Value = Driver.Id;
                    worksheet.Cells[1, i].Value = Driver.UserName;
                    worksheet.Cells[1, i].Value = Driver.FullName;
                    worksheet.Cells[1, i].Value = Driver.Mobile;
                    i++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);

                stream.Position = 0;
                return File(stream,
                           "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                           "DriverUsers.xlsx");
            }
        }
        public IActionResult ExportParentUserIndex()
        {
            var Parents = _userService.GetAllParents();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ParentsUsers");

                worksheet.Cells[1, 1].Value = "شناسه";
                worksheet.Cells[1, 2].Value = "نام کاربری";
                worksheet.Cells[1, 3].Value = "نام نام خانوادگی";
                worksheet.Cells[1, 4].Value = "موبایل";
                int i = 2;
                foreach (var Parent in Parents)
                {
                    worksheet.Cells[1, i].Value = Parent.Id;
                    worksheet.Cells[1, i].Value = Parent.UserName;
                    worksheet.Cells[1, i].Value = Parent.FullName;
                    worksheet.Cells[1, i].Value = Parent.Mobile;
                    i++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);

                stream.Position = 0;
                return File(stream,
                           "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                           "ParentsUsers.xlsx");
            }
        } 
        #endregion
    }
}
