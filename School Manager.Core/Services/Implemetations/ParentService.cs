using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Implemetations
{
    public class ParentService : IParentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ParentService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public ParentDto GetParent(long Id)
        {
            var ds = _unitOfWork.GetRepository<Parent>().Query()
                .Include(x=>x.Children).Where(x=>x.Id == Id);
            return _mapper.Map<ParentDto>(ds);
        }

        public ParentDto GetParentByNationCode(string NationCode)
        {
            var ds = _unitOfWork.GetRepository<Parent>().Query()
                .Include(x => x.Children).Where(x => x.NationalCode == NationCode);
            return _mapper.Map<ParentDto>(ds);
        }

        public ParentDto GetParentByPhone(string Phone)
        {
            var ds = _unitOfWork.GetRepository<Parent>().Query()
                .Include(x => x.Children).Where(x => x.UserNavigation.Mobile == Phone);
            return _mapper.Map<ParentDto>(ds);
        }

        public async Task<List<ParentDto>> GetParents()
        {
            var ds = await _unitOfWork.GetRepository<Parent>()
                .Query()
                .Include(x=>x.Children).ToListAsync();
            return _mapper.Map<List<ParentDto>>(ds);
        }
    }
}
