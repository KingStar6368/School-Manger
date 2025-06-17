using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface IChequeService
    {
        long CreateCheque(ChequeCreateDto cheque);
        bool DeleteCheque(long id);
        bool UpdateCheque(ChequeUpdateDto cheque);
    }
}
