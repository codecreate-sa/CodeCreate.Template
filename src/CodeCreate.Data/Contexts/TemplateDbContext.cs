using CodeCreate.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeCreate.Data.Contexts;

public class TemplateDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public TemplateDbContext(DbContextOptions<TemplateDbContext> options) : base(options)
    { }

    /// <summary>
    /// 
    /// </summary>
    public DbSet<Customer> Customer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUser<Guid>>()
            .ToTable("User", "identity");

        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .ToTable("UserLogin", "identity");

        modelBuilder.Entity<IdentityUserClaim<Guid>>()
            .ToTable("UserClaim", "identity");

        modelBuilder.Entity<IdentityRoleClaim<Guid>>()
            .ToTable("RoleClaim", "identity");

        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .ToTable("UserRole", "identity");

        modelBuilder.Entity<IdentityRole<Guid>>()
            .ToTable("Role", "identity");

        modelBuilder.Entity<IdentityUserToken<Guid>>()
            .ToTable("UserToken", "identity");

        modelBuilder.Entity<Customer>()
            .HasKey(k => k.Id);
    }
}
