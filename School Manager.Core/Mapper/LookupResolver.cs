using AutoMapper;
using School_Manager.Core.Classes;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;

namespace School_Manager.Core.Mapper
{
    public class BankAccountResolver : IValueResolver<Driver, DriverDto, LookupComboViewModel>
    {
        private readonly ILookupService _lookUp;

        public BankAccountResolver(ILookupService unitOfWork)
        {
            _lookUp = unitOfWork;
        }
        public LookupComboViewModel Resolve(Driver source, DriverDto destination, LookupComboViewModel destMember, ResolutionContext context)
        {
            var lookup = _lookUp.GetLookUp(StaticString.LookUpBankType, source.BankRef);

            return new LookupComboViewModel
            {
                Code = source.BankRef,
                Name = lookup?.Name ?? "نامشخص"
            };
        }
    }
    public class ColorResolver : IValueResolver<Car, CarInfoDto, string>
    {
        private readonly ILookupService _lookUp;

        public ColorResolver(ILookupService unitOfWork)
        {
            _lookUp = unitOfWork;
        }

        public string Resolve(Car source, CarInfoDto destination, string destMember, ResolutionContext context)
        {
            var lookup = _lookUp.GetLookUp(StaticString.LookUpColorType, source.ColorCode);

            return  lookup?.Name ?? "نامشخص";
        }
    }
    public class BankNameResolver : IValueResolver<ServiceContractCheque, CheckDto, LookupComboViewModel>
    {
        private readonly ILookupService _lookUp;

        public BankNameResolver(ILookupService unitOfWork)
        {
            _lookUp = unitOfWork;
        }
        public LookupComboViewModel Resolve(ServiceContractCheque source, CheckDto destination, LookupComboViewModel destMember, ResolutionContext context)
        {
            var lookup = _lookUp.GetLookUp(StaticString.LookUpBankType, source.ChequeNavigation.BankId);

            return new LookupComboViewModel
            {
                Code = source.ChequeNavigation.BankId,
                Name = lookup?.Name ?? "نامشخص"
            };
        }
    }
}
