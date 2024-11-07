using BaseServiceApiRest_Core.Domain.Services;
using BaseServiceApiRest_Core.Entities;
using BaseServiceApiRest_Core.Interfaces.IRepositories;
using BaseServiceApiRest_Core.Interfaces.IServices;
using BaseServiceApiRest_Core.Interfaces.Repositories.Base;
using BaseServiceApiRest_Core.Interfaces.Repositories;
using BaseServiceApiRest_Core.MappingProfiles;
using BaseServiceApiRest_Core.Services;
using BaseServiceApiRest_Core.Transactions;
using BaseServiceRestApi_Infrastructure.Data.Repositories;
using BaseServiceRestApi_Infrastructure.Data.Transactions;
using BaseServiceApiRest_Core.Middleware;

namespace BaseServiceApiRest.Configuration;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddInjectionDependency(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UsersProfile), typeof(PersonProfile), typeof(SchoolProfile), typeof(HelpFriendProfile), typeof(ManagerProfile));

        #region Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IHelpFriendService, HelpFriendService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<ISchoolService, SchoolService>();
        services.AddScoped<IManagerService, ManagerService>();
        services.AddScoped<ITransactionManager, TransactionManager>();
        #endregion

        #region Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<ISchoolRepository, SchoolRepository>();
        services.AddScoped<IHelpFriendRepository, HelpFriendRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        #endregion

        #region AbstractClasses
        services.AddScoped<IRepository<Users>, UserRepository>();
        services.AddScoped<IRepository<HelpFriend>, HelpFriendRepository>();
        services.AddScoped<IRepository<Person>, PersonRepository>();
        services.AddScoped<IRepository<School>, SchoolRepository>();
        services.AddScoped<IRepository<Manager>, ManagerRepository>();
        #endregion

        #region Middleware
        services.AddScoped<ExceptionHandlerMiddleware>();
        #endregion

        #region Authentication
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtProviderService, JwtProviderService>();
        #endregion

        services.AddHttpContextAccessor();

        return services;
    }
}