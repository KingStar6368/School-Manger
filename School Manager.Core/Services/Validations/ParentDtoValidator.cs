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
    public class ParentCreateDtoValidator : ParentDtoValidator<ParentCreateDto> { }
    public class ParentUpdateDtoValidator : ParentDtoValidator<ParentUpdateDto> { }
}
