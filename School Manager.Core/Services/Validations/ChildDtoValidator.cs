using FluentValidation;
using School_Manager.Core.Classes;
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
                .NotEmpty().WithMessage(ValidatorMessage.RequireName)
                .MaximumLength(30).WithMessage(string.Format(ValidatorMessage.NameLimitCharacter,30));

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(ValidatorMessage.RequireLastName)
                .MaximumLength(30).WithMessage(string.Format(ValidatorMessage.LastNameLimitCharacter,30));

            RuleFor(x => x.NationalCode)
                .NotEmpty().WithMessage(ValidatorMessage.RequireNationalCode)
                .MaximumLength(11).WithMessage(string.Format(ValidatorMessage.NationalLimitCharacter,11));

        }
    }
    public class ChildCreateDtoValidator : ChildDtoValidator<ChildCreateDto> { }
    public class ChildUpdateDtoValidator : ChildDtoValidator<ChildUpdateDto> { }
}
