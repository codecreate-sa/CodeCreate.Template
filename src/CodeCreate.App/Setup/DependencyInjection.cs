using Asp.Versioning;
using CodeCreate.App.Filters;
using CodeCreate.App.Swagger;
using CodeCreate.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CodeCreate.App.Setup
{
    /// <summary>
    /// IServiceCollection extension methods for registering services in DI container of our API
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adding required services for our API versioning
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthN();
            services.AddVersioning();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins(configuration.GetSection("AllowCors").Get<string[]>() ?? [])
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
            
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);
            
            services
                .AddHealthChecks()
                .AddDbContextCheck<TemplateDbContext>(nameof(TemplateDbContext), HealthStatus.Unhealthy);

            services.AddSwagger();

            return services;
        }

        private static IServiceCollection AddAuthN(this IServiceCollection services)
        {
            services
                // Replace with separate calls to AddAuthentication to select only cookies
                // See https://github.com/dotnet/AspNetCore.Docs.Samples/blob/main/samples/ngIdentity/ngIdentity.Server/Program.cs#L9-L19
                // and https://github.com/dotnet/AspNetCore.Docs.Samples/blob/main/samples/ngIdentity/ngIdentity.Server/Program.cs#L24-L26
                .AddIdentityApiEndpoints<IdentityUser<Guid>>()
                .AddEntityFrameworkStores<TemplateDbContext>();

            return services;
        }

        private static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services
                .AddApiVersioning(x =>
                {
                    x.DefaultApiVersion = new ApiVersion(1.0);
                    x.AssumeDefaultVersionWhenUnspecified = true;
                    x.ReportApiVersions = true;
                    x.ApiVersionReader = new MediaTypeApiVersionReader("api-version");
                })
                .AddMvc()
                .AddApiExplorer();

            return services;
        }

        /// <summary>
        /// Adding required services for Swagger/OpenAPI
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(setupAction => {
                // Collect all referenced projects output XML document file paths  
                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                    .Union([currentAssembly.GetName()])
                    .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location)!, $"{a.Name}.xml"))
                    .Where(f => File.Exists(f)).ToArray();

                foreach (var xmlDoc in xmlDocs)
                {
                    setupAction.IncludeXmlComments(xmlDoc);
                }

                setupAction.OperationFilter<SwaggerDefaultValues>();
            });

            return services;
        }
    }
}