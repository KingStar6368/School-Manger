using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using School_Manager.Core.Services.Implemetations;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Domain.Entities.Catalog.Enums;
using School_Manager.Domain.Entities.Catalog.Operation;
using System.Drawing;
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
        private readonly IShiftService _shiftService;
        private readonly IDriverService _driverService;
        private readonly ISchoolService _schoolService;

        public ExcelController(IChildService childService, IParentService parentService, IUserService userService,
                             IShiftService shiftService, IDriverService driverService, ISchoolService schoolService)
        {
            _parentService = parentService;
            _childService = childService;
            _userService = userService;
            _shiftService = shiftService;
            _driverService = driverService;
            _schoolService = schoolService;

            ExcelPackage.License.SetNonCommercialOrganization("Soft");
        }

        #region Helper Methods
        private void ApplyHeaderStyle(ExcelWorksheet worksheet, int row, int columns, Color headerColor)
        {
            var headerRange = worksheet.Cells[row, 1, row, columns];

            // Style for header
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(headerColor);
            headerRange.Style.Font.Color.SetColor(Color.White);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Font.Size = 12;
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            headerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            headerRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            headerRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        }

        private void ApplyDataStyle(ExcelWorksheet worksheet, int startRow, int endRow, int columns, Color rowColor)
        {
            for (int i = startRow; i <= endRow; i++)
            {
                var rowRange = worksheet.Cells[i, 1, i, columns];

                // Alternate row coloring for better readability
                Color cellColor = (i % 2 == 0) ? Color.White : Color.FromArgb(248, 248, 248);

                rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rowRange.Style.Fill.BackgroundColor.SetColor(cellColor);
                rowRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rowRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                rowRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rowRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rowRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rowRange.Style.Font.Size = 11;
            }
        }

        private void ApplyTitleStyle(ExcelWorksheet worksheet, string title, int columns)
        {
            // Merge cells for title - FIXED: Only set value in first cell before merging
            var titleCell = worksheet.Cells[1, 1];
            titleCell.Value = title; // Set value before merging

            var titleRange = worksheet.Cells[1, 1, 1, columns];
            titleRange.Merge = true;

            // Style for title
            titleRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            titleRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(31, 78, 121));
            titleRange.Style.Font.Color.SetColor(Color.White);
            titleRange.Style.Font.Bold = true;
            titleRange.Style.Font.Size = 16;
            titleRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            titleRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            // Set row height for title
            worksheet.Row(1).Height = 25;
        }
        #endregion

        #region Index Pages
        public async Task<IActionResult> ExportParentIndex()
        {
            var Parents = await _parentService.GetParents();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("والدین");

                // Add title
                ApplyTitleStyle(worksheet, "لیست والدین", 4);

                // Headers
                worksheet.Cells[2, 1].Value = "شناسه";
                worksheet.Cells[2, 2].Value = "نام";
                worksheet.Cells[2, 3].Value = "نام خانوادگی";
                worksheet.Cells[2, 4].Value = "کد ملی";

                ApplyHeaderStyle(worksheet, 2, 4, Color.FromArgb(79, 129, 189));

                int i = 3;
                foreach (var Parent in Parents)
                {
                    worksheet.Cells[i, 1].Value = Parent.Id;
                    worksheet.Cells[i, 2].Value = Parent.ParentFirstName;
                    worksheet.Cells[i, 3].Value = Parent.ParentLastName;
                    worksheet.Cells[i, 4].Value = Parent.ParentNationalCode;
                    i++;
                }

                ApplyDataStyle(worksheet, 3, i - 1, 4, Color.White);
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
                var worksheet = package.Workbook.Worksheets.Add("رانندگان");

                // Add title
                ApplyTitleStyle(worksheet, "لیست کاربران راننده", 4);

                // Headers
                worksheet.Cells[2, 1].Value = "شناسه";
                worksheet.Cells[2, 2].Value = "نام کاربری";
                worksheet.Cells[2, 3].Value = "نام و نام خانوادگی";
                worksheet.Cells[2, 4].Value = "موبایل";

                ApplyHeaderStyle(worksheet, 2, 4, Color.FromArgb(155, 194, 230));

                int i = 3;
                foreach (var Driver in Drivers)
                {
                    worksheet.Cells[i, 1].Value = Driver.Id;
                    worksheet.Cells[i, 2].Value = Driver.UserName;
                    worksheet.Cells[i, 3].Value = Driver.FullName;
                    worksheet.Cells[i, 4].Value = Driver.Mobile;
                    i++;
                }

                ApplyDataStyle(worksheet, 3, i - 1, 4, Color.White);
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
                var worksheet = package.Workbook.Worksheets.Add("والدین");

                // Add title
                ApplyTitleStyle(worksheet, "لیست کاربران والدین", 4);

                // Headers
                worksheet.Cells[2, 1].Value = "شناسه";
                worksheet.Cells[2, 2].Value = "نام کاربری";
                worksheet.Cells[2, 3].Value = "نام و نام خانوادگی";
                worksheet.Cells[2, 4].Value = "موبایل";

                ApplyHeaderStyle(worksheet, 2, 4, Color.FromArgb(79, 129, 189));

                int i = 3;
                foreach (var Parent in Parents)
                {
                    worksheet.Cells[i, 1].Value = Parent.Id;
                    worksheet.Cells[i, 2].Value = Parent.UserName;
                    worksheet.Cells[i, 3].Value = Parent.FullName;
                    worksheet.Cells[i, 4].Value = Parent.Mobile;
                    i++;
                }

                ApplyDataStyle(worksheet, 3, i - 1, 4, Color.White);
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
        public async Task<IActionResult> ExportAllDriverPays()
        {
            var Drivers = await _driverService.GetDrivers();
            Dictionary<long, long> DriverShifts = new Dictionary<long, long>();
            foreach (var Driver in Drivers)
            {
                var Shifts = _shiftService.GetDriverShifts(Driver.Id);
                foreach (var Shift in Shifts)
                {
                    var Childern = await _shiftService.GetChildernOfShift(Shift.Id);
                    var NonPayids = Childern.Where(x => x.Bills.Any(b => !b.HasPaid && b.BillExpiredTime < DateTime.Now)).Any();
                    if (NonPayids)
                        continue;
                    else
                        DriverShifts.Add(Shift.Id, Driver.Id);
                }
            }
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("پرداخت شده ها");

                // Add title - FIXED: Set value in single cell before merging
                ApplyTitleStyle(worksheet, "لیست پرداختی رانندگان به تفکیک شیفت", 7);

                // Headers
                worksheet.Cells[2, 1].Value = "ردیف";
                worksheet.Cells[2, 2].Value = "نام";
                worksheet.Cells[2, 3].Value = "نام خانوادگی";
                worksheet.Cells[2, 4].Value = "کد ملی";
                worksheet.Cells[2, 5].Value = "نام شیفت";
                worksheet.Cells[2, 6].Value = "شماره حساب / کارت";
                worksheet.Cells[2, 7].Value = "مبلغ پرداختی ريال";

                ApplyHeaderStyle(worksheet, 2, 7, Color.FromArgb(0, 176, 80)); // Green for paid

                int i = 3;
                foreach (var DriverShift in DriverShifts)
                {
                    var driver = Drivers.FirstOrDefault(x => x.Id == DriverShift.Value);
                    var Childern = await _shiftService.GetChildernOfShift(DriverShift.Key);
                    long TotalPrice = 0;
                    foreach(var Child in Childern)
                    {
                        var TotalPriceOfBill = Child.Bills.Sum(x => x.TotalPrice);
                        if (Child.ClassEnum == ClassNumber.Twelfth)
                            TotalPriceOfBill = TotalPriceOfBill / 7;
                        else
                            TotalPriceOfBill = TotalPriceOfBill / 8;
                        TotalPrice += TotalPriceOfBill;
                    }
                    worksheet.Cells[i, 1].Value = i;
                    worksheet.Cells[i, 2].Value = driver.Name;
                    worksheet.Cells[i, 3].Value = driver.LastName;
                    worksheet.Cells[i, 4].Value = driver.NationCode;
                    worksheet.Cells[i, 5].Value = _shiftService.GetShiftById(DriverShift.Key).Name;
                    worksheet.Cells[i, 6].Value = driver.BankAccountNumber;
                    worksheet.Cells[i, 7].Value = TotalPrice - (TotalPrice / 10);
                    i++;
                }
                ApplyDataStyle(worksheet, 3, i - 1, 7, Color.White);
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);

                stream.Position = 0;
                return File(stream,
                           "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                           $"BankPay_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            }
        }

        public async Task<IActionResult> ExportDriverPays(long DriverID)
        {
            var Childern = await _shiftService.GetAllPassangerOfDriver(DriverID);
            var PayIds = Childern.Where(x => !x.Bills.Any(b => !b.HasPaid && b.BillExpiredTime < DateTime.Now)).ToList();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("پرداخت شده ها");

                // Add title - FIXED: Set value in single cell before merging
                var driver = _driverService.GetDriver(DriverID);
                ApplyTitleStyle(worksheet, "لیست مسافران با پرداخت کامل" + $" راننده {driver.Name} {driver.LastName}", 7);

                // Headers
                worksheet.Cells[2, 1].Value = "ردیف";
                worksheet.Cells[2, 2].Value = "نام";
                worksheet.Cells[2, 3].Value = "نام خانوادگی";
                worksheet.Cells[2, 4].Value = "مقطع تحصیلی";
                worksheet.Cells[2, 5].Value = "نام شیفت";
                worksheet.Cells[2, 6].Value = "کدملی";
                worksheet.Cells[2, 7].Value = "نام مدرسه";

                ApplyHeaderStyle(worksheet, 2, 7, Color.FromArgb(0, 176, 80)); // Green for paid

                int i = 3;
                foreach (var Pays in PayIds)
                {
                    worksheet.Cells[i, 1].Value = Pays.Id;
                    worksheet.Cells[i, 2].Value = Pays.FirstName;
                    worksheet.Cells[i, 3].Value = Pays.LastName;
                    worksheet.Cells[i, 4].Value = Pays.Class;
                    worksheet.Cells[i, 5].Value = _shiftService.GetShiftById(Pays.ShiftId).Name;
                    worksheet.Cells[i, 6].Value = Pays.NationalCode;
                    worksheet.Cells[i, 7].Value = _schoolService.GetSchool(Pays.SchoolId.Value).Name;
                    i++;
                }

                ApplyDataStyle(worksheet, 3, i - 1, 7, Color.White);
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);

                stream.Position = 0;
                return File(stream,
                           "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                           $"DriverPays_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            }
        }

        public async Task<IActionResult> ExportDriverNonPays(long DriverID)
        {
            var Childern = await _shiftService.GetAllPassangerOfDriver(DriverID);
            var NonPayids = Childern.Where(x => x.Bills.Any(b => !b.HasPaid && b.BillExpiredTime < DateTime.Now)).ToList();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("پرداخت نشده ها");

                // Add title - FIXED: Set value in single cell before merging
                var driver = _driverService.GetDriver(DriverID);
                ApplyTitleStyle(worksheet, "لیست مسافران با پرداخت معوق" + $" راننده {driver.Name} {driver.LastName}", 7);

                // Headers
                worksheet.Cells[2, 1].Value = "ردیف";
                worksheet.Cells[2, 2].Value = "نام";
                worksheet.Cells[2, 3].Value = "نام خانوادگی";
                worksheet.Cells[2, 4].Value = "مقطع تحصیلی";
                worksheet.Cells[2, 5].Value = "نام شیفت";
                worksheet.Cells[2, 6].Value = "کدملی";
                worksheet.Cells[2, 7].Value = "نام مدرسه";

                ApplyHeaderStyle(worksheet, 2, 7, Color.FromArgb(255, 0, 0)); // Red for unpaid

                int i = 3;
                foreach (var NPays in NonPayids)
                {
                    worksheet.Cells[i, 1].Value = NPays.Id;
                    worksheet.Cells[i, 2].Value = NPays.FirstName;
                    worksheet.Cells[i, 3].Value = NPays.LastName;
                    worksheet.Cells[i, 4].Value = NPays.Class;
                    worksheet.Cells[i, 5].Value = _shiftService.GetShiftById(NPays.ShiftId).Name;
                    worksheet.Cells[i, 6].Value = NPays.NationalCode;
                    worksheet.Cells[i, 7].Value = _schoolService.GetSchool(NPays.SchoolId.Value).Name;
                    i++;
                }

                ApplyDataStyle(worksheet, 3, i - 1, 7, Color.White);
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);

                stream.Position = 0;
                return File(stream,
                           "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                           $"DriverNonPays_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            }
        }
    }
}