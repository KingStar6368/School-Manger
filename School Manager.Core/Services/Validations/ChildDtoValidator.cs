using FluentValidation;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Domain.Entities.Catalog.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Validations
{
    public abstract class ChildDtoValidator<T> : AbstractValidator<T> where T : IChildDto
    {
        protected ChildDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("نام  الزامی است.")
                .MaximumLength(30).WithMessage("نام نباید بیشتر از 30 کاراکتر باشد.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
                .MaximumLength(30).WithMessage("نام خانوادگی نباید بیشتر از 30 کاراکتر باشد.");

            RuleFor(x => x.NationalCode)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است.")
                .MaximumLength(11).WithMessage("کد ملی نباید بیشتر از 11 کاراکتر باشد.");

        }
    }
    public class ChildCreateDtoValidator : ChildDtoValidator<ChildCreateDto> { }
    public class ChildUpdateDtoValidator : ChildDtoValidator<ChildUpdateDto> { }
}
