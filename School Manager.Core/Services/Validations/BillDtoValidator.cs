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
    public class PreBillDTOValidator : AbstractValidator<CreatePreBillDto>
    {
        IUnitOfWork _unitOfWork;
        public PreBillDTOValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.ChildRef)
                .GreaterThan(0).WithMessage("باید بیشتر از صفر باشد.")
                .MustAsync(HasNoActiveContract)
                .WithMessage("قرارداد فعال برای این فرزند در بازه مشخص شده وجود دارد.");
        }
        private async Task<bool> HasNoActiveContract(CreatePreBillDto dto, long childRef, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<ServiceContract>();
            return !await repo.Query().AnyAsync(
                u => u.ChildRef == childRef &&
                     u.StartTime <= dto.StartTime &&
                     u.EndTime >= dto.StartTime &&
                     u.IsActive,
                cancellationToken);
        }
    }
}
