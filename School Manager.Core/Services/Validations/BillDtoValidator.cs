using FluentValidation;
using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Validations
{
    public abstract class BillDtoValidator<T> : AbstractValidator<T> where T : IBillDto
    {
        protected BillDtoValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("باید بیشتر از صفر باشد.");
        }
    }
    public class BillCreateDtoValidator : BillDtoValidator<BillCreateDto> { }
    public class BillUpdateDtoValidator : BillDtoValidator<BillUpdateDto> { }
}
