using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeCreate.App.Swagger
{
    /// <summary>
    /// The Swagger options configuration class
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IHostEnvironment _environment;

        /// <summary>
        /// ConfigureSwaggerOptions constructor
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="environment"></param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IHostEnvironment environment)
        {
            _provider = provider;
            _environment = environment;
        }

        /// <summary>
        /// The Configure method
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo
                    {
                        Title = _environment.ApplicationName,
                        Version = description.ApiVersion.ToString(),
                    });
            }
        }
    }
}
