using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Implemetations
{
    public class ParentService : IParentService
    {
        public ParentDto GetParent(long Id)
        {
            throw new NotImplementedException();
        }

        public ParentDto GetParentByNationCode(string NationCode)
        {
            throw new NotImplementedException();
        }

        public ParentDto GetParentByPhone(string Phone)
        {
            throw new NotImplementedException();
        }

        public Task<List<ParentDto>> GetParents()
        {
            throw new NotImplementedException();
        }
    }
}
