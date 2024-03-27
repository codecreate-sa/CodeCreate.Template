using CodeCreate.App.Setup;
using CodeCreate.Data.Extensions;
using CodeCreate.Domain.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CodeCreate.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

            builder.Services.AddApiServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            // Shouldn't UseStaticFiles be used instead as per the documentation?
            builder.Services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            builder.Services.RegisterDbContext(connectionString);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.MapIdentityApi<IdentityUser<Guid>>();

            app.UseHttpsRedirection();

            // protection from cross-site request forgery (CSRF/XSRF) attacks with empty body
            // form can't post anything useful so the body is null, the JSON call can pass
            // an empty object {} but doesn't allow cross-site due to CORS.
            app.MapPost("/logout", async (
                SignInManager<IdentityUser<Guid>> signInManager,
                [FromBody] object empty) =>
            {
                if (empty is not null)
                {
                    await signInManager.SignOutAsync();
                    return Results.Ok();
                }

                return Results.NotFound();
            }).RequireAuthorization();

            app.UseSwaggerForApi(app.DescribeApiVersions());
            app.UseHealthChecks();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
