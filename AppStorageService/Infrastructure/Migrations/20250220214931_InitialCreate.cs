using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppStorageService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(120)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StoreType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "CreatedAt", "LastUpdatedAt", "Name", "Status", "StoreType", "Url" },
                values: new object[,]
                {
                    { new Guid("a2a23c4d-5e6f-4f8b-9a0c-223456789abc"), new DateTime(2024, 2, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 20, 12, 0, 0, 0, DateTimeKind.Utc), "TikShock", 0, 1, "https://play.google.com/store/apps/details?id=com.musically" },
                    { new Guid("b1a23c4d-5e6f-4f8b-9a0c-123456789abc"), new DateTime(2024, 2, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 20, 12, 0, 0, 0, DateTimeKind.Utc), "TikTok", 1, 1, "https://play.google.com/store/apps/details?id=com.zhiliaoapp.musically" },
                    { new Guid("c3a23c4d-5e6f-4f8b-9a0c-323456789abc"), new DateTime(2024, 2, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Subway Surfers", 1, 0, "https://apps.apple.com/us/app/subway-surfers/id512939461" },
                    { new Guid("d4a23c4d-5e6f-4f8b-9a0c-423456789abc"), new DateTime(2024, 2, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Subway Runners", 0, 0, "https://apps.apple.com/us/app/subway-surfers/id51293941" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");
        }
    }
}
