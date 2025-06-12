using FluentValidation;
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
    public class UserCreateDTOValidator : AbstractValidator<UserCreateDTO>
    {
        IUnitOfWork _unitOfWork;
        public UserCreateDTOValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("نام  الزامی است.")
                .MaximumLength(100).WithMessage("نام نباید بیشتر از 30 کاراکتر باشد.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
                .MaximumLength(100).WithMessage("نام خانوادگی نباید بیشتر از 30 کاراکتر باشد.");

            RuleFor(x => x.Mobile)
                .NotEmpty().WithMessage(" موبایل الزامی است.")
                .MaximumLength(11).WithMessage("موبایل نباید بیشتر از 11 کاراکتر باشد.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("نام کاربری الزامی است.")
                .MaximumLength(10).WithMessage("نام کاربری نباید بیشتر از 10 کاراکتر باشد.");

            RuleFor(x => x)
                .Must(x => BeUniqueMobile(x, x.Mobile)).WithMessage("موبایل تکراری است.");

            RuleFor(x => x)
                .Must(x => BeUniqueUserName(x, x.UserName)).WithMessage("نام کاربری تکراری است.");

            
        }
        private bool BeUniqueMobile(UserCreateDTO dTO, string code)
        {
            var repo = _unitOfWork.GetRepository<User>();
            return !repo.GetAll().Any(x => x.Mobile == code);
        }
        private bool BeUniqueUserName(UserCreateDTO dTO, string code)
        {
            var repo = _unitOfWork.GetRepository<User>();
            return !repo.GetAll().Any(x => x.UserName == code);
        }
    }
    public class UserEditDTOValidator : AbstractValidator<UserEditDTO>
    {
        IUnitOfWork _unitOfWork;
        public UserEditDTOValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("نام  الزامی است.")
                .MaximumLength(100).WithMessage("نام نباید بیشتر از 30 کاراکتر باشد.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
                .MaximumLength(100).WithMessage("نام خانوادگی نباید بیشتر از 30 کاراکتر باشد.");

            RuleFor(x => x.Mobile)
                .NotEmpty().WithMessage(" موبایل الزامی است.")
                .MaximumLength(11).WithMessage("نام نباید بیشتر از 11 کاراکتر باشد.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("نام کاربری الزامی است.")
                .MaximumLength(10).WithMessage("نام کاربری نباید بیشتر از 10 کاراکتر باشد.");

            RuleFor(x => x)
                .Must(x => BeUniqueMobile(x, x.Mobile)).WithMessage("موبایل تکراری است.");

            RuleFor(x => x)
                .Must(x => BeUniqueUserName(x, x.UserName)).WithMessage("نام کاربری تکراری است.");

            
        }
        private bool BeUniqueMobile(UserEditDTO dTO, string code)
        {
            var repo = _unitOfWork.GetRepository<User>();
            return !repo.GetAll().Any(x => x.Mobile == code && x.Id != dTO.Id);
        }
        private bool BeUniqueUserName(UserEditDTO dTO, string code)
        {
            var repo = _unitOfWork.GetRepository<User>();
            return !repo.GetAll().Any(x => x.UserName == code && x.Id != dTO.Id);
        }
    }
}
