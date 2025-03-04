using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppStorageService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AT2_UpdateApplicationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Applications",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.UpdateData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: new Guid("a2a23c4d-5e6f-4f8b-9a0c-223456789abc"),
                columns: new[] { "Status", "StoreType" },
                values: new object[] { 1, 2 });

            migrationBuilder.UpdateData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: new Guid("b1a23c4d-5e6f-4f8b-9a0c-123456789abc"),
                columns: new[] { "Status", "StoreType" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: new Guid("c3a23c4d-5e6f-4f8b-9a0c-323456789abc"),
                columns: new[] { "Status", "StoreType" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: new Guid("d4a23c4d-5e6f-4f8b-9a0c-423456789abc"),
                columns: new[] { "Status", "StoreType" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Applications",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: new Guid("a2a23c4d-5e6f-4f8b-9a0c-223456789abc"),
                columns: new[] { "Status", "StoreType" },
                values: new object[] { 0, 1 });

            migrationBuilder.UpdateData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: new Guid("b1a23c4d-5e6f-4f8b-9a0c-123456789abc"),
                columns: new[] { "Status", "StoreType" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: new Guid("c3a23c4d-5e6f-4f8b-9a0c-323456789abc"),
                columns: new[] { "Status", "StoreType" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: new Guid("d4a23c4d-5e6f-4f8b-9a0c-423456789abc"),
                columns: new[] { "Status", "StoreType" },
                values: new object[] { 0, 0 });
        }
    }
}
