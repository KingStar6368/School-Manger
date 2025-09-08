using Microsoft.AspNetCore.Mvc;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manger.Extension;
using School_Manger.Models.PageView;
using SMS.TempLinkService;
using System.Text;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace School_Manger.Controllers
{
    public class DirectLinkController : Controller
    {
        private readonly IParentService _PService;
        private readonly IChildService _CService;
        private readonly IBillService _BillService;
        private readonly ITempLink _TService;
        public DirectLinkController(IParentService parentService,IChildService childService
            ,IBillService billService,ITempLink tempLink)
        {
            _PService = parentService;
            _CService = childService;
            _BillService = billService;
            _TService = tempLink;
        }
        public IActionResult RequstTimeOut()
        {
            return View();
        }
        /// <summary>
        /// برسی لینک مستقیم
        /// </summary>
        /// <param name="CodeData">داده ورودی در لینک</param>
        /// <returns></returns>
        /// success https://localhost:7298/DirectLink/DirectBill?CodeData=49,44,50,44,57,47,54,47,50,48,50,53,32,52,58,50,53,58,52,51,32,80,77
        /// Timeout https://localhost:7298/DirectLink/DirectBill?CodeData=49,44,50,44,57,47,51,47,50,48,50,53,32,53,58,50,55,58,48,48,32,80,77
        public async Task<IActionResult> DirectBill(string CodeData)
        {
            try
            {
                string link = _TService.GenerateBillTempLink(1, 2);
                //DeCode Data
                string[] TempData = CodeData.Split(',');
                List<byte> ByteDecodedData = new List<byte>();
                foreach (string st in TempData)
                    ByteDecodedData.Add(byte.Parse(st));
                string DeCodedData = Encoding.UTF8.GetString(ByteDecodedData.ToArray());
                string[] Datas = DeCodedData.Split(',');
                //Map values
                long ParentId = long.Parse(Datas[0]);
                long ChildId = long.Parse(Datas[1]);
                DateTime LinkDate = DateTime.Parse(Datas[2]);
                //Autorize Link
                if (LinkDate.AddDays(2) < DateTime.Now)
                    return View("RequstTimeOut", (LinkDate.ToPersianString(),LinkDate.AddDays(2).ToPersianString()));

                ParentDto parent = _PService.GetParent(ParentId);
                ChildInfo child = _CService.GetChild(ChildId);
                List<BillDto> bills = await _BillService.GetChildBills(ChildId);
                BillDashbord dashbord = new BillDashbord()
                {
                    bills = bills,
                    child = child,
                    parent = parent,
                };
                return View("Bills", dashbord);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
