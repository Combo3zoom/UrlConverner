using DataLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Database;

public class InforceContext: IdentityDbContext<User,ApplicationRole,Guid>
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public InforceContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Connection"));
    }

    public InforceContext() { }
    public DbSet<User> Users { get; set; }
    public DbSet<Url> Urls { get; set; }
    public DbSet<Description> Descriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(Url).Assembly);
        
        base.OnModelCreating(builder);
    }
}