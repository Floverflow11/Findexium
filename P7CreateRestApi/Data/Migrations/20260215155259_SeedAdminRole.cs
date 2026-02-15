using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P7CreateRestApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ea0023f9-337c-4b61-a9dd-4dc4d32bdb92", "acb5c744-96cf-40ae-8d3d-49d3c48463f9", "Admin", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea0023f9-337c-4b61-a9dd-4dc4d32bdb92");
        }
    }
}
