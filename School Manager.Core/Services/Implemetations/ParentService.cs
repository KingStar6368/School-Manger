using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<ParentCreateDto> _createValidator;
        private readonly IValidator<ParentUpdateDto> _UpdateValidator;
        public ParentService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<ParentCreateDto> createValidator,
                             IValidator<ParentUpdateDto> updateValidator )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _UpdateValidator = updateValidator;
        }
        public ParentDto GetParent(long Id)
        {
            var ds = _unitOfWork.GetRepository<Parent>().Query()
                .Include(x=>x.Children).ThenInclude(x=>x.LocationPairs).ThenInclude(x=>x.Locations).FirstOrDefault(x=>x.Id == Id);
            return _mapper.Map<ParentDto>(ds);
        }

        public ParentDto GetParentByNationCode(string NationCode)
        {
            var ds = _unitOfWork.GetRepository<Parent>().Query()
                .Include(x => x.Children).ThenInclude(x => x.LocationPairs).ThenInclude(x => x.Locations).FirstOrDefault(x => x.NationalCode == NationCode);
            return _mapper.Map<ParentDto>(ds);
        }

        public ParentDto GetParentByPhone(string Phone)
        {
            var ds = _unitOfWork.GetRepository<Parent>().Query()
                .Include(x => x.Children).ThenInclude(x => x.LocationPairs).ThenInclude(x => x.Locations).FirstOrDefault(x => x.UserNavigation.Mobile == Phone);
            return _mapper.Map<ParentDto>(ds);
        }

        public async Task<List<ParentDto>> GetParents()
        {
            var ds = await _unitOfWork.GetRepository<Parent>()
                .Query()
                .Include(x=>x.Children).ThenInclude(x => x.LocationPairs).ThenInclude(x => x.Locations).ToListAsync();
            return _mapper.Map<List<ParentDto>>(ds);
        }
        public async Task<long> CreateParentAsync(ParentCreateDto parent)
        {
            long result = 0;
            var validationResult = await _createValidator.ValidateAsync(parent);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            var mParent = _mapper.Map<Parent>(parent);
            _unitOfWork.GetRepository<Parent>().Add(mParent);
            if (_unitOfWork.SaveChanges() > 0)
                result = mParent.Id;
            return result;
        }
        public async Task<bool> UpdateParentAsync(ParentUpdateDto parent)
        {
            var mainParent = _unitOfWork.GetRepository<Parent>().GetById(parent.Id);
            var validationResult =await _UpdateValidator.ValidateAsync(parent);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            _mapper.Map(parent, mainParent);
            _unitOfWork.GetRepository<Parent>().Update(mainParent);
            return _unitOfWork.SaveChanges() > 0;
        }
        public bool DeleteParent(long id)
        {
            var Parent = _unitOfWork.GetRepository<Parent>()
                        .Query(x => x.Id == id)
                        .Include(x => x.Children)
                        .FirstOrDefault();

            if (Parent == null) return false;

            // بررسی وجود اطلاعات وابسته
            if (Parent.Children?.Any() ?? false)
            {
                throw new InvalidOperationException("این والد دارای اطلاعات وابسته است و امکان حذف آن وجود ندارد.");
            }
            _unitOfWork.GetRepository<Parent>().Remove(Parent);
            return _unitOfWork.SaveChanges() > 0;
        }

        public long CreateParent(ParentCreateDto parent)
        {
            return CreateParentAsync(parent).Result;    
        }

        public bool UpdateParent(ParentUpdateDto parent)
        {
            return UpdateParentAsync(parent).Result;
        }
    }
}
