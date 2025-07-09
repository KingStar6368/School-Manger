using FluentValidation;
using Microsoft.EntityFrameworkCore;
using School_Manager.Core.Classes;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Identity;
using School_Manager.Domain.Entities.Catalog.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Validations
{
    public abstract class ParentDtoValidator<T> : AbstractValidator<T> where T : IParentDto
    {
        protected ParentDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(ValidatorMessage.RequireName)
                .MaximumLength(30).WithMessage(string.Format(ValidatorMessage.NameLimitCharacter, 30));

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(ValidatorMessage.RequireLastName)
                .MaximumLength(30).WithMessage(string.Format(ValidatorMessage.LastNameLimitCharacter, 30));

            RuleFor(x => x.NationalCode)
                .NotEmpty().WithMessage(ValidatorMessage.RequireNationalCode)
                .MaximumLength(11).WithMessage(string.Format(ValidatorMessage.NationalLimitCharacter, 11));

        }
    }
    public class ParentCreateDtoValidator : ParentDtoValidator<ParentCreateDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParentCreateDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.UserRef)
                .MustAsync(async (userRef, cancellation) =>
                {
                    var repo = _unitOfWork.GetRepository<Parent>();
                    return !await repo.Query().AnyAsync(u => u.UserRef == userRef, cancellation);
                })
                .WithMessage(ValidatorMessage.DuplicatedParent);
        }
    }
    public class ParentUpdateDtoValidator : ParentDtoValidator<ParentUpdateDto> 
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParentUpdateDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.UserRef)
                .MustAsync(async (dto,userRef, cancellation) =>
                {
                    var repo = _unitOfWork.GetRepository<Parent>();
                    return !await repo.Query().AnyAsync(u => u.UserRef == userRef && u.Id != dto.Id, cancellation);
                })
                .WithMessage(ValidatorMessage.DuplicatedParent);
        }
    }
}
