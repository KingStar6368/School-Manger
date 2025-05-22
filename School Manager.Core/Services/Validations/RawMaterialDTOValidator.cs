using FluentValidation;
using School_Manager.Core.ViewModels.RawMaterial;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Validations
{
    public class RawMaterialDTOValidator : AbstractValidator<RawMaterialDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RawMaterialDTOValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("نام ماده اولیه الزامی است.")
                .MaximumLength(100).WithMessage("نام ماده اولیه نباید بیشتر از ۱۰۰ کاراکتر باشد.");

            RuleFor(x => x.MaterialCode)
                .NotEmpty().WithMessage("کد ماده اولیه الزامی است.");

            RuleFor(x => x)
                .Must(x=>BeUniqueMaterialCode(x,x.MaterialCode)).WithMessage("کد ماده اولیه تکراری است.");

            RuleFor(x => x.UnitConversion)
                .GreaterThan(0).WithMessage("باید بیشتر از صفر باشد.");
        }

        private bool BeUniqueMaterialCode(RawMaterialDTO dTO,string code)
        {
            var repo = _unitOfWork.GetRepository<RawMaterial>();
            return !repo.GetAll().Any(x => x.MaterialCode == code && x.Id != dTO.Id);
        }
    }
}
