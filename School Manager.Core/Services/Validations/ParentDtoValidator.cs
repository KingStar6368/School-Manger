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
    public abstract class ParentDtoValidator<T> : AbstractValidator<T> where T : IParentDto
    {
        protected ParentDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("نام  الزامی است.")
                .MaximumLength(30).WithMessage("نام نباید بیشتر از 30 کاراکتر باشد.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
                .MaximumLength(30).WithMessage("نام خانوادگی نباید بیشتر از 30 کاراکتر باشد.");

            RuleFor(x => x.NationalCode)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
                .MaximumLength(11).WithMessage("نام خانوادگی نباید بیشتر از 30 کاراکتر باشد.");

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
                .WithMessage("کاربر وارد شده قبلاً برای والدین ثبت شده است.");
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
                .WithMessage("کاربر وارد شده قبلاً برای والدین ثبت شده است.");
        }
    }
}
