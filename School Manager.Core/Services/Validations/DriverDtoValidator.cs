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
    public abstract class DriverDtoValidator<T> : AbstractValidator<T> where T : IDriverDto
    {
        protected DriverDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidatorMessage.RequireName)
                .MaximumLength(30).WithMessage(string.Format(ValidatorMessage.NameLimitCharacter,30));

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(ValidatorMessage.RequireLastName)
                .MaximumLength(30).WithMessage(string.Format(ValidatorMessage.LastNameLimitCharacter,30));

            RuleFor(x => x.NationCode)
                .NotEmpty().WithMessage(ValidatorMessage.RequireNationalCode)
                .MaximumLength(11).WithMessage(string.Format(ValidatorMessage.NationalLimitCharacter, 11));

        }
    }
    public class DriverCreateDtoValidator : DriverDtoValidator<DriverCreateDto> 
    {
        private readonly IUnitOfWork _unitOfWork;

        public DriverCreateDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.UserRef)
                .MustAsync(async (userRef, cancellation) =>
                {
                    var repo = _unitOfWork.GetRepository<Driver>();
                    return !await repo.Query().AnyAsync(u => u.UserRef == userRef, cancellation);
                })
                .WithMessage(ValidatorMessage.DuplicatedDriver);
        }
    }
    public class DriverUpdateDtoValidator : DriverDtoValidator<DriverUpdateDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DriverUpdateDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.UserRef)
                .MustAsync(async (dto, userRef, cancellation) =>
                {
                    var repo = _unitOfWork.GetRepository<Driver>();
                    return !await repo.Query().AnyAsync(u => u.UserRef == userRef && u.Id != dto.Id, cancellation);
                })
                .WithMessage(ValidatorMessage.DuplicatedDriver);
        }
    }
}
