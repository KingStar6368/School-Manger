using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;

namespace School_Manager.Core.Services.Implemetations
{
    public class ContractService : IContractService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContractService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public long CreateDriverContract(DriverContractCreateDto driverContract)
        {
            long result = 0;
            var mdriverContract = _mapper.Map<DriverContract>(driverContract);
            _unitOfWork.GetRepository<DriverContract>().Add(mdriverContract);
            if (_unitOfWork.SaveChanges() > 0)
                result = mdriverContract.Id;
            return result;
        }

        public long CreateServiceContract(ServiceContractCreateDto serviceContract)
        {
            long result = 0;
            var mserviceContract = _mapper.Map<ServiceContract>(serviceContract);
            _unitOfWork.GetRepository<ServiceContract>().Add(mserviceContract);
            if (_unitOfWork.SaveChanges() > 0)
                result = mserviceContract.Id;
            return result;
        }

        public bool DeleteDriverContract(long Id)
        {
            var driverContract = _unitOfWork.GetRepository<DriverContract>()
                        .Query(x => x.Id == Id)
                        .FirstOrDefault();

            if (driverContract == null) return false;

            _unitOfWork.GetRepository<DriverContract>().Remove(driverContract);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool DeleteServiceContract(long Id)
        {
            var serviceContract = _unitOfWork.GetRepository<ServiceContract>()
                        .Query(x => x.Id == Id).Include(x=>x.Bills)
                        .FirstOrDefault();

            if (serviceContract == null) return false;
            if (serviceContract.Bills.Any())
            {
                throw new InvalidOperationException("این قرارداد دارای اطلاعات وابسته است و امکان حذف آن وجود ندارد.");
            }

            _unitOfWork.GetRepository<ServiceContract>().Remove(serviceContract);
            return _unitOfWork.SaveChanges() > 0;
        }

        public ServiceContractDto GetContract(long Id)
        {
            var ds = _unitOfWork.GetRepository<ServiceContract>()
                                .Query(x => x.Id == Id)
                                .Include(x=>x.ChildNavigation).ThenInclude(x => x.ParentNavigation)
                                .Include(x=>x.ServiceContractCheques).ThenInclude(x=>x.ChequeNavigation)
                                .FirstOrDefault();
            if (ds == null)
            {
                return new ServiceContractDto();
            }
            return _mapper.Map<ServiceContractDto>(ds);
        }

        public async Task<List<ServiceContractDto>> GetContracts()
        {
            var ds = await _unitOfWork.GetRepository<ServiceContract>()
                                      .Query(x=>x.IsActive)
                                      .Include(x => x.ChildNavigation).ThenInclude(x => x.ParentNavigation)
                                      .Include(x => x.ServiceContractCheques).ThenInclude(x => x.ChequeNavigation)
                                      .ToListAsync();
            if (ds == null)
            {
                return new List<ServiceContractDto>();
            }
            return _mapper.Map<List<ServiceContractDto>>(ds);
        }

        public ServiceContractDto GetContractWithChild(long ChildId)
        {
            var ds = _unitOfWork.GetRepository<ServiceContract>()
                                .Query(x => x.ChildRef == ChildId && x.IsActive)
                                .Include(x => x.ChildNavigation).ThenInclude(x => x.ParentNavigation)
                                .Include(x=>x.Bills)
                                .Include(x => x.ServiceContractCheques).ThenInclude(x => x.ChequeNavigation)
                                .FirstOrDefault();
            if (ds == null)
            {
                return new ServiceContractDto();
            }
            return _mapper.Map<ServiceContractDto>(ds);
        }

        public bool UpdateDriverContract(DriverContractUpdateDto driverContract)
        {
            var maindriverContract = _unitOfWork.GetRepository<DriverContract>().GetById(driverContract.Id);
            _mapper.Map(driverContract, maindriverContract);
            _unitOfWork.GetRepository<DriverContract>().Update(maindriverContract);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool UpdateServiceContract(ServiceContractUpdateDto serviceContract)
        {
            var mainserviceContract = _unitOfWork.GetRepository<ServiceContract>().GetById(serviceContract.Id);
            _mapper.Map(serviceContract, mainserviceContract);
            _unitOfWork.GetRepository<ServiceContract>().Update(mainserviceContract);
            return _unitOfWork.SaveChanges() > 0;
        }
    }
}
