using Microsoft.Extensions.DependencyInjection;
using School_Manager.Core.Services.Implemetations;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Data.Repositories;
using School_Manager.Domain.Base;
using School_Manager.Domain.Entities.Catalog.Operation;

namespace School_Manager.IOC
{
    public class Container
    {
        private static ServiceCollection _services;
        private static ServiceProvider ServiceProvider;

        public ServiceProvider Register(ServiceCollection services = null)
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
            services.AddSingleton<IDateTimeService, SystemDateTimeService>();

            //Add Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRawMaterialService, RawMaterialService>();
            services.AddScoped<ILookupService,LookupService>();
        }
    }
}
