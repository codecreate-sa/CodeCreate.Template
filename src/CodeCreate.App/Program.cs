using CodeCreate.Data.Contexts;
using CodeCreate.Data.Extensions;
using CodeCreate.Domain.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeCreate.App;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

        var origins = builder.Configuration
            .GetSection("AllowCors").Get<string[]>() ?? Array.Empty<String>();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins(origins)
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
        });

        builder.Services.AddApplicationServices();

        builder.Services.AddAuthentication();

        builder.Services.AddAuthorization();

        // Shouldn't UseStaticFiles be used instead as per the documentation?
        builder.Services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "ClientApp/dist";
        });

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                               ?? string.Empty;

        builder.Services.RegisterDbContext(connectionString);

        builder.Services
            // Replace with separate calls to AddAuthentication to select only cookies
            // See https://github.com/dotnet/AspNetCore.Docs.Samples/blob/main/samples/ngIdentity/ngIdentity.Server/Program.cs#L9-L19
            // and https://github.com/dotnet/AspNetCore.Docs.Samples/blob/main/samples/ngIdentity/ngIdentity.Server/Program.cs#L24-L26
            .AddIdentityApiEndpoints<IdentityUser<Guid>>()
            .AddEntityFrameworkStores<TemplateDbContext>();

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        } else
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

        app.UseCors();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
