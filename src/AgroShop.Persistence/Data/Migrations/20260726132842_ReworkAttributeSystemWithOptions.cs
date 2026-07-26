using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroShop.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReworkAttributeSystemWithOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductAttributeValues_ProductId",
                table: "ProductAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_SubCategoryId",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ProductAttributeValues");

            migrationBuilder.AddColumn<Guid>(
                name: "AttributeOptionId",
                table: "ProductAttributeValues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Attributes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)");

            migrationBuilder.AddColumn<string>(
                name: "ValueType",
                table: "Attributes",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AttributeOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeOptions_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_AttributeOptionId",
                table: "ProductAttributeValues",
                column: "AttributeOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductId_AttributeOptionId",
                table: "ProductAttributeValues",
                columns: new[] { "ProductId", "AttributeOptionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_SubCategoryId_AttributeId",
                table: "ProductAttributes",
                columns: new[] { "SubCategoryId", "AttributeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_Name",
                table: "Attributes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeOptions_AttributeId",
                table: "AttributeOptions",
                column: "AttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValues_AttributeOptions_AttributeOptionId",
                table: "ProductAttributeValues",
                column: "AttributeOptionId",
                principalTable: "AttributeOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValues_AttributeOptions_AttributeOptionId",
                table: "ProductAttributeValues");

            migrationBuilder.DropTable(
                name: "AttributeOptions");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributeValues_AttributeOptionId",
                table: "ProductAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributeValues_ProductId_AttributeOptionId",
                table: "ProductAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_SubCategoryId_AttributeId",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_Name",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "AttributeOptionId",
                table: "ProductAttributeValues");

            migrationBuilder.DropColumn(
                name: "ValueType",
                table: "Attributes");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "ProductAttributeValues",
                type: "VARCHAR(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Attributes",
                type: "VARCHAR(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductId",
                table: "ProductAttributeValues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_SubCategoryId",
                table: "ProductAttributes",
                column: "SubCategoryId");
        }
    }
}
