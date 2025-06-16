using FluentValidation;
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
                .NotEmpty().WithMessage("نام  الزامی است.")
                .MaximumLength(30).WithMessage("نام نباید بیشتر از 30 کاراکتر باشد.");

            RuleFor(x => x.ManagerName)
                .NotEmpty().WithMessage("نام مدیر الزامی است.")
                .MaximumLength(30).WithMessage("نام خانوادگی نباید بیشتر از 30 کاراکتر باشد.");

        }
    }
    public class SchoolCreateDtoValidator : SchoolDtoValidator<SchoolCreateDto> { }
    public class SchoolUpdateDtoValidator : SchoolDtoValidator<SchoolUpdateDto> { }
}
