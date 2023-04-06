using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P229FirstApi.Migrations
{
    public partial class AddedCategoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "System"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 5, 14, 32, 9, 9, DateTimeKind.Utc).AddTicks(1320), "System", null, null, false, "Apple", null, null },
                    { 2, new DateTime(2023, 4, 5, 14, 32, 9, 9, DateTimeKind.Utc).AddTicks(1329), "System", null, null, false, "Asus", null, null },
                    { 3, new DateTime(2023, 4, 5, 14, 32, 9, 9, DateTimeKind.Utc).AddTicks(1331), "System", null, null, false, "Lenovo", null, null },
                    { 4, new DateTime(2023, 4, 5, 14, 32, 9, 9, DateTimeKind.Utc).AddTicks(1331), "System", null, null, false, "Dell", null, null },
                    { 5, new DateTime(2023, 4, 5, 14, 32, 9, 9, DateTimeKind.Utc).AddTicks(1332), "System", null, null, false, "Acer", null, null },
                    { 6, new DateTime(2023, 4, 5, 14, 32, 9, 9, DateTimeKind.Utc).AddTicks(1333), "System", null, null, false, "Samsung", null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
