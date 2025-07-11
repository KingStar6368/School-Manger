using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface IPayBillService
    {
        long CreatePay(PayCreateDto pay);
        //List<PayBillDto> GetAllPays(long BillId);
        //PayBillDto GetPay(long Id);
        //bool DeletePay(long PayId);
    }
}
