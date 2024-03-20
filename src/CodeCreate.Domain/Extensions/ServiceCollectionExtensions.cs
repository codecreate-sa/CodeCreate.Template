using CodeCreate.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CodeCreate.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterServices(
        this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}
