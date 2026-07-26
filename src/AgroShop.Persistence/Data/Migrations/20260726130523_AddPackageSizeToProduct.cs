using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroShop.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPackageSizeToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PackageAmount",
                table: "Products",
                type: "numeric(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PackageUnit",
                table: "Products",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageAmount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PackageUnit",
                table: "Products");
        }
    }
}
