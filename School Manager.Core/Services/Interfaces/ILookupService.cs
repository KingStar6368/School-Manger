using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Core.ViewModels.Lookup;

namespace School_Manager.Core.Services.Interfaces
{
    public interface ILookupService
    {
        List<LookupComboViewModel> GetLookupCombo(string Type);
    }
}
