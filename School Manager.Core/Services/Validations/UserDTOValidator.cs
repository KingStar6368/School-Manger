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
                .MaximumLength(11).WithMessage("موبایل نباید بیشتر از 11 کاراکتر باشد.")
                .MustAsync(async (mobile, cancellation) =>
                {
                    var repo = _unitOfWork.GetRepository<User>();
                    return !await repo.Query().AnyAsync(u => u.Mobile == mobile);
                }).WithMessage("شماره موبایل تکراری است.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("نام کاربری الزامی است.")
                .MaximumLength(10).WithMessage("نام کاربری نباید بیشتر از 10 کاراکتر باشد.")
                .MustAsync(async (userName, cancellation) =>
                {
                    var repo = _unitOfWork.GetRepository<User>();
                    return !await repo.Query().AnyAsync(u => u.UserName == userName);
                }).WithMessage("نام کاربری تکراری است.");
        }
    }
    public class UserEditDTOValidator : AbstractValidator<UserUpdateDTO>
    {
        IUnitOfWork _unitOfWork;
        public UserEditDTOValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("نام  الزامی است.")
                .MaximumLength(30).WithMessage("نام نباید بیشتر از 30 کاراکتر باشد.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
                .MaximumLength(30).WithMessage("نام خانوادگی نباید بیشتر از 30 کاراکتر باشد.");

            RuleFor(x => x.Mobile)
                .NotEmpty().WithMessage("موبایل الزامی است.")
                .MaximumLength(11).WithMessage("موبایل نباید بیشتر از 11 کاراکتر باشد.")
                .MustAsync(async (dto, mobile, cancellation) =>
                {
                    var repo = _unitOfWork.GetRepository<User>();
                    return !await repo.Query().AnyAsync(u => u.Mobile == mobile && u.Id != dto.Id);
                }).WithMessage("شماره موبایل تکراری است.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("نام کاربری الزامی است.")
                .MaximumLength(10).WithMessage("نام کاربری نباید بیشتر از 10 کاراکتر باشد.")
                .MustAsync(async (dto, userName, cancellation) =>
                {
                    var repo = _unitOfWork.GetRepository<User>();
                    return !await repo.Query().AnyAsync(u => u.UserName == userName && u.Id != dto.Id);
                }).WithMessage("نام کاربری تکراری است.");
        }
    }
}
