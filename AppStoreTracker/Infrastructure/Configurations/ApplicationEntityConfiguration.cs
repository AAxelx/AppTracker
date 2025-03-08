using AppStoreTracker.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppStoreTracker.Infrastructure.Configurations;

public class ApplicationEntityConfiguration : IEntityTypeConfiguration<ApplicationEntity>
{
  public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
  {
    builder.HasKey(a => a.Id);
    builder.Property(a => a.Url).IsRequired().HasColumnType("nvarchar(120)");
    builder.Property(a => a.Status).IsRequired().HasColumnType("int");
    builder.Property(a => a.LastUpdatedAt).IsRequired().HasColumnType("datetime2");
  }
}

