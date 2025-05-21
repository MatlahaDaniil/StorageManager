using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageManager.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Castomers_CastomerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Histories_HistoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_HistoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Histories_ShopId",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "Shops");

            migrationBuilder.RenameColumn(
                name: "CastomerId",
                table: "Products",
                newName: "ShopEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CastomerId",
                table: "Products",
                newName: "IX_Products_ShopEntityId");

            migrationBuilder.AlterColumn<Guid>(
                name: "HistoryId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShopId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CastomerId",
                table: "Histories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Histories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Histories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Histories_CastomerId",
                table: "Histories",
                column: "CastomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_ProductId",
                table: "Histories",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Histories_ShopId",
                table: "Histories",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Castomers_CastomerId",
                table: "Histories",
                column: "CastomerId",
                principalTable: "Castomers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Products_ProductId",
                table: "Histories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shops_ShopEntityId",
                table: "Products",
                column: "ShopEntityId",
                principalTable: "Shops",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Castomers_CastomerId",
                table: "Histories");

            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Products_ProductId",
                table: "Histories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shops_ShopEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Histories_CastomerId",
                table: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Histories_ProductId",
                table: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Histories_ShopId",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CastomerId",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Histories");

            migrationBuilder.RenameColumn(
                name: "ShopEntityId",
                table: "Products",
                newName: "CastomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ShopEntityId",
                table: "Products",
                newName: "IX_Products_CastomerId");

            migrationBuilder.AddColumn<Guid>(
                name: "HistoryId",
                table: "Shops",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "HistoryId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Products_HistoryId",
                table: "Products",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_ShopId",
                table: "Histories",
                column: "ShopId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Castomers_CastomerId",
                table: "Products",
                column: "CastomerId",
                principalTable: "Castomers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Histories_HistoryId",
                table: "Products",
                column: "HistoryId",
                principalTable: "Histories",
                principalColumn: "Id");
        }
    }
}
