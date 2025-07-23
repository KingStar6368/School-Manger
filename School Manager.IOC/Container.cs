using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School_Manager.Core.Mapper;
using School_Manager.Core.Services.Implemetations;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.Services.Validations;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Data.Context;
using School_Manager.Data.Repositories;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;

namespace School_Manager.IOC
{
    public static class Container
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<SchMSDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            // DateTime Service
            services.AddSingleton<IDateTimeService, SystemDateTimeService>();

            // Caching
            services.AddMemoryCache();
            services.AddScoped<ICachService, CachService>();

            // Repositories / Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IChildService, ChildService>();
            services.AddScoped<IParentService, ParentService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IChequeService, ChequeService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IPayBillService, PayBillService>();
            services.AddScoped<IRawMaterialService, RawMaterialService>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<ISMSTempleService,SMSTempleService>();

            // Validators
            services.AddScoped<IValidator<RawMaterialDTO>, RawMaterialDTOValidator>();
            services.AddScoped<IValidator<UserCreateDTO>, UserCreateDTOValidator>();
            services.AddScoped<IValidator<UserUpdateDTO>, UserEditDTOValidator>();
            services.AddScoped<IValidator<BillCreateDto>, BillCreateDtoValidator>();
            services.AddScoped<IValidator<BillUpdateDto>, BillUpdateDtoValidator>();
            services.AddScoped<IValidator<ChildCreateDto>, ChildCreateDtoValidator>();
            services.AddScoped<IValidator<ChildUpdateDto>, ChildUpdateDtoValidator>();
            services.AddScoped<IValidator<DriverCreateDto>, DriverCreateDtoValidator>();
            services.AddScoped<IValidator<DriverUpdateDto>, DriverUpdateDtoValidator>();
            services.AddScoped<IValidator<ParentCreateDto>, ParentCreateDtoValidator>();
            services.AddScoped<IValidator<ParentUpdateDto>, ParentUpdateDtoValidator>();
            services.AddScoped<IValidator<SchoolCreateDto>, SchoolCreateDtoValidator>();
            services.AddScoped<IValidator<SchoolUpdateDto>, SchoolUpdateDtoValidator>();
            services.AddScoped<IValidator<CreatePreBillDto>, PreBillDTOValidator>();
            services.AddScoped<IValidator<PayCreateDto>, PayCreateDtoValidator>();
            services.AddScoped<IValidator<PayUpdateDto>, PayUpdateDtoValidator>();

            // Value Resolvers
            services.AddTransient<BankNameResolver>();
            services.AddTransient<ActiveDriverIdResolver>();
            services.AddTransient<BankAccountResolver>();
            services.AddTransient<ColorResolver>();
            services.AddTransient<HasPaidResolver>();
            services.AddTransient<PaidPriceResolver>();
            services.AddTransient<PaidTimeResolver>();
            services.AddTransient<StatusResolver>();

            return services;
        }
    }

}
