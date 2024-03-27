using Asp.Versioning.ApiExplorer;
using CodeCreate.App.Contracts.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace CodeCreate.App.Setup
{
    /// <summary>
    /// Extension methods for the http request pipeline configuration
    /// </summary>
    public static class HttpRequestPipeline
    {
        /// <summary>
        /// Use Health Checks
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/_health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    var response = new HealthCheckResponse
                    {
                        Status = report.Status.ToString(),
                        Checks = report.Entries.Select(e => new HealthCheck
                        {
                            Component = e.Key,
                            Status = e.Value.Status.ToString(),
                            Description = e.Value.Description!
                        }),
                        Duration = report.TotalDuration
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            });

            return app;
        }

        /// <summary>
        /// Use Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="apiVersionDescriptions"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerForApi(this IApplicationBuilder app, IReadOnlyList<ApiVersionDescription> apiVersionDescriptions)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                foreach (var description in apiVersionDescriptions)
                {
                    x.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName);
                }
            });

            return app;
        }
    }
}