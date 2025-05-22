using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using School_Manager.Core.Services.Implemetations;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.Services.Validations;
using School_Manager.Core.ViewModels.FModels;
using School_Manager.Data.Repositories;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;

namespace School_Manager.IOC
{
    public class Container
    {
        private static IServiceCollection _services;
        private static ServiceProvider ServiceProvider;

        public ServiceProvider Register(IServiceCollection services = null)
        {
            if (_services == null)
                _services = new ServiceCollection();

            if (services != null)
                _services = services;

            ConfigureServices(_services);
            ServiceProvider = _services.BuildServiceProvider();

            return ServiceProvider;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Add Services
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IDateTimeService, SystemDateTimeService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            //Add Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRawMaterialService, RawMaterialService>();
            services.AddScoped<ILookupService,LookupService>();
            services.AddScoped<ISchoolService,SchoolService>();
            //services.AddScoped<IDateTimeService,SystemDateTimeService>();
            services.AddMemoryCache();
            services.AddScoped<ICachService, CachService>(); 
            services.AddScoped<IValidator<RawMaterialDTO>, RawMaterialDTOValidator>();
        }
    }
}
