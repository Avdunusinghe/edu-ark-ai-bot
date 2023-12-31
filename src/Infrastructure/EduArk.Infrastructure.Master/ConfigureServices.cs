using EduArk.Application.Common.Interfaces;
using EduArk.Domain.Repositories.Command.Base;
using EduArk.Domain.Repositories.Command.Master;
using EduArk.Domain.Repositories.Query.Base;
using EduArk.Domain.Repositories.Query.Master;
using EduArk.Infrastructure.Master.Data;
using EduArk.Infrastructure.Master.Repositories.Command;
using EduArk.Infrastructure.Master.Repositories.Command.Base;
using EduArk.Infrastructure.Master.Repositories.Query;
using EduArk.Infrastructure.Master.Repositories.Query.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddEduArkInfrastructureMasterServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<MasterDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("MasterConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IMasterDbContext>(provider => provider.GetRequiredService<MasterDbContext>());

            services.AddTransient<MasterDbContextInitialiser>();

            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));

            services.AddTransient<IAppSettingQueryRepository, AppSettingQueryRepository>();
            services.AddTransient<IAppSettingCommandRepository, AppSettingCommandRepository>();

            services.AddTransient<IMasterUserQueryRepository, MasterUserQueryRepository>();
            services.AddTransient<IMasterUserCommandRepository, MasterUserCommandRepository>();

            services.AddTransient<ITenantCompanyQueryRepository, TenantCompanyQueryRepository>();
            services.AddTransient<ITenantCompanyCommandRepository, TenantCompanyCommandRepository>();

            return services;
        }
    }
}
