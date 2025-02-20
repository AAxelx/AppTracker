using AppStorageService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppStorageService.Infrastructure.Contexts;

public class MsSqlDbContext : DbContext
{
  public DbSet<ApplicationEntity> Applications { get; set; }

  public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(MsSqlDbContext).Assembly);
  }
}