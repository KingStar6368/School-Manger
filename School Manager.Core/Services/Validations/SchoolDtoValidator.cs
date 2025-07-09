using FluentValidation;
using School_Manager.Core.Classes;
using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Validations
{
    public abstract class SchoolDtoValidator<T> : AbstractValidator<T> where T : ISchoolDto
    {
        protected SchoolDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidatorMessage.RequireName)
                .MaximumLength(30).WithMessage(string.Format(ValidatorMessage.NameLimitCharacter,30));

            RuleFor(x => x.ManagerName)
                .NotEmpty().WithMessage(ValidatorMessage.RequireManagerName)
                .MaximumLength(30).WithMessage(string.Format(ValidatorMessage.NameLimitCharacter, 30));

        }
    }
    public class SchoolCreateDtoValidator : SchoolDtoValidator<SchoolCreateDto> { }
    public class SchoolUpdateDtoValidator : SchoolDtoValidator<SchoolUpdateDto> { }
}
