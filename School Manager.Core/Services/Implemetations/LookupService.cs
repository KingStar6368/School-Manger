using AutoMapper;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.Lookup;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;

namespace School_Manager.Core.Services.Implemetations
{
    public class LookupService : ILookupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LookupService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public List<LookupComboViewModel> GetLookupCombo(string pType)
        {
            var ds = _unitOfWork.GetRepository<Lookup>().Query
                (
                    predicate: p=>p.Type == pType
                );
            return _mapper.Map<List<LookupComboViewModel>>(ds);
        }
    }
}
