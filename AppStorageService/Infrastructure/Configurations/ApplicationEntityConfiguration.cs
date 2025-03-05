using AppStorageService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppStorageService.Infrastructure.Configurations;

public class ApplicationEntityConfiguration : IEntityTypeConfiguration<ApplicationEntity>
{
    public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired().HasColumnType("nvarchar(50)");
        builder.Property(a => a.Url).IsRequired().HasColumnType("nvarchar(120)");
        builder.Property(a => a.Status).IsRequired().HasColumnType("int");
        builder.Property(a => a.StoreType).IsRequired().HasColumnType("int");
        builder.Property(a => a.CreatedAt).IsRequired().HasColumnType("datetime2");
        builder.Property(a => a.LastUpdatedAt).IsRequired().HasColumnType("datetime2");

        builder.HasData(
            new ApplicationEntity
            {
                Id = new Guid("b1a23c4d-5e6f-4f8b-9a0c-123456789abc"),
                Name = "TikTok",
                Url = "https://play.google.com/store/apps/details?id=com.zhiliaoapp.musically",
                Status = 2,
                StoreType = 2,
                CreatedAt = new DateTime(2024, 2, 20, 12, 0, 0, DateTimeKind.Utc),
                LastUpdatedAt = new DateTime(2024, 2, 20, 12, 0, 0, DateTimeKind.Utc)
            },
            new ApplicationEntity
            {
                Id = new Guid("a2a23c4d-5e6f-4f8b-9a0c-223456789abc"),
                Name = "TikShock",
                Url = "https://play.google.com/store/apps/details?id=com.musically",
                Status = 1,
                StoreType = 2,
                CreatedAt = new DateTime(2024, 2, 20, 12, 0, 0, DateTimeKind.Utc),
                LastUpdatedAt = new DateTime(2024, 2, 20, 12, 0, 0, DateTimeKind.Utc)
            },
            new ApplicationEntity
            {
                Id = new Guid("c3a23c4d-5e6f-4f8b-9a0c-323456789abc"),
                Name = "Subway Surfers",
                Url = "https://apps.apple.com/us/app/subway-surfers/id512939461",
                Status = 2,
                StoreType = 1,
                CreatedAt = new DateTime(2024, 2, 20, 12, 0, 0, DateTimeKind.Utc),
                LastUpdatedAt = new DateTime(2024, 2, 20, 12, 0, 0, DateTimeKind.Utc)
            },
            new ApplicationEntity
            {
                Id = new Guid("d4a23c4d-5e6f-4f8b-9a0c-423456789abc"),
                Name = "Subway Runners",
                Url = "https://apps.apple.com/us/app/subway-surfers/id51293941",
                Status = 1,
                StoreType = 1,
                CreatedAt = new DateTime(2024, 2, 20, 12, 0, 0, DateTimeKind.Utc),
                LastUpdatedAt = new DateTime(2024, 2, 20, 12, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}