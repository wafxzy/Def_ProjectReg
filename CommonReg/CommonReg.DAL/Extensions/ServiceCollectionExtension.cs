using CommonReg.DAL.Repositories.Interfaces;
using CommonReg.DAL.UnitOfWork;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace CommonReg.DAL.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddReposithories(this IServiceCollection services)
        {

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            services.AddSingleton<IConnectionStringResolver, ConnectionStringResolver>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IDataBaseContext, DataBaseContext>();
            services.AddScoped<IUserRepository, Repositories.UserRepository>();
            services.AddScoped<IUserSessionRepository, Repositories.UserSessionRepository>();
            services.AddScoped<IUserRoleRepository, Repositories.UserRoleRepository>();
            return services;
        }
    }
}
