using FluentValidation;
using School_Manager.Core.ViewModels.FModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Validations
{
    public abstract class PayDtoValidator<T> : AbstractValidator<T> where T : IPayDto
    {
        protected PayDtoValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("مبلغ باید از بیشتر از صفر باشد.");


        }
    }
    public class PayCreateDtoValidator : PayDtoValidator<PayCreateDto> { }
    public class PayUpdateDtoValidator : PayDtoValidator<PayUpdateDto> { }
}
