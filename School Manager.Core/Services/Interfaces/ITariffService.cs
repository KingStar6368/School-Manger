using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface ITariffService
    {
        public Task<List<TariffDto>> GetActiveTariff();
        public int CreateTariff(TariffDto tariffDto);
        public bool UpdateTariff(TariffDto tariffDto);
        public bool DeleteTariff(int Id);
    }
}
