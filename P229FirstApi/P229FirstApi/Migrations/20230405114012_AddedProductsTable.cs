using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P229FirstApi.Migrations
{
    public partial class AddedProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6573));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6581));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6582));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6583));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6584));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6585));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6680), "System", null, null, false, null, null },
                    { 2, 1, new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6682), "System", null, null, false, null, null },
                    { 3, 1, new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6683), "System", null, null, false, null, null },
                    { 4, 2, new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6683), "System", null, null, false, null, null },
                    { 5, 2, new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6685), "System", null, null, false, null, null },
                    { 6, 3, new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6687), "System", null, null, false, null, null },
                    { 7, 3, new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6687), "System", null, null, false, null, null },
                    { 8, 3, new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6688), "System", null, null, false, null, null },
                    { 9, 3, new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6689), "System", null, null, false, null, null },
                    { 10, 4, new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6692), "System", null, null, false, null, null },
                    { 11, 5, new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6694), "System", null, null, false, null, null },
                    { 12, 5, new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6695), "System", null, null, false, null, null },
                    { 13, 5, new DateTime(2023, 4, 5, 15, 40, 12, 85, DateTimeKind.Utc).AddTicks(6696), "System", null, null, false, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 5, 14, 32, 9, 9, DateTimeKind.Utc).AddTicks(1320));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 5, 14, 32, 9, 9, DateTimeKind.Utc).AddTicks(1329));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 5, 14, 32, 9, 9, DateTimeKind.Utc).AddTicks(1331));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 5, 14, 32, 9, 9, DateTimeKind.Utc).AddTicks(1331));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 5, 14, 32, 9, 9, DateTimeKind.Utc).AddTicks(1332));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 5, 14, 32, 9, 9, DateTimeKind.Utc).AddTicks(1333));
        }
    }
}
