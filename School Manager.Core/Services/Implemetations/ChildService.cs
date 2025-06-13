using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Implemetations
{
    public class ChildService : IChildService
    {
        public BillDto GetBill(long id)
        {
            throw new NotImplementedException();
        }

        public ChildInfo GetChild(long id)
        {
            throw new NotImplementedException();
        }

        public List<BillDto> GetChildBill(long id)
        {
            throw new NotImplementedException();
        }

        public ChildInfo GetChildByNationCode(string nationCode)
        {
            throw new NotImplementedException();
        }

        public DriverDto GetChildDriver(long DriverId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChildInfo>> GetChildren()
        {
            throw new NotImplementedException();
        }

        public School GetChildSchool(long SchoolId)
        {
            throw new NotImplementedException();
        }
    }
}
