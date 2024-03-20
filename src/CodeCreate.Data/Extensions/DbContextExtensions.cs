using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeCreate.Data.Extensions;

public static class DbContextExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterDbContext(
        this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<Contexts.TemplateDbContext>(
            options => options.UseSqlServer(connectionString));

        return services;
    }
}
