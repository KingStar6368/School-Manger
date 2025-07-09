using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
                .NotEmpty().WithMessage("نام  الزامی است.")
                .MaximumLength(30).WithMessage("نام نباید بیشتر از 30 کاراکتر باشد.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
                .MaximumLength(30).WithMessage("نام خانوادگی نباید بیشتر از 30 کاراکتر باشد.");

            RuleFor(x => x.NationCode)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
                .MaximumLength(11).WithMessage("نام خانوادگی نباید بیشتر از 30 کاراکتر باشد.");

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
                    var repo = _unitOfWork.GetRepository<Parent>();
                    return !await repo.Query().AnyAsync(u => u.UserRef == userRef, cancellation);
                })
                .WithMessage("کاربر وارد شده قبلاً برای راننده ثبت شده است.");
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
                    var repo = _unitOfWork.GetRepository<Parent>();
                    return !await repo.Query().AnyAsync(u => u.UserRef == userRef && u.Id != dto.Id, cancellation);
                })
                .WithMessage("کاربر وارد شده قبلاً برای راننده ثبت شده است.");
        }
    }
}
