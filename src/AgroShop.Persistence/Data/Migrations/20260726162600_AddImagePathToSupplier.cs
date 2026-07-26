using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroShop.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Suppliers",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Suppliers");
        }
    }
}
