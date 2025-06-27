using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface IContractService
    {
        Task<List<ServiceContractDto>> GetContracts();
        ServiceContractDto GetContract(long Id);
        ServiceContractDto GetContractWithChild(long ChildId);

        long CreateDriverContract(DriverContractCreateDto driverContract);
        bool DeleteDriverContract(long Id);
        bool UpdateDriverContract(DriverContractUpdateDto driverContract);
        long CreateServiceContract(ServiceContractCreateDto serviceContract);
        bool DeleteServiceContract(long Id);
        bool UpdateServiceContract(ServiceContractUpdateDto serviceContract);
    }
}
