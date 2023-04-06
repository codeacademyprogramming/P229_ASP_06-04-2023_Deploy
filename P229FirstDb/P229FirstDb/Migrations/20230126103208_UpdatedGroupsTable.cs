using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P229FirstDb.Migrations
{
    public partial class UpdatedGroupsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Groups",
                newName: "Ad");

            migrationBuilder.AddColumn<int>(
                name: "StudentCount",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentCount",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "Groups",
                newName: "Name");
        }
    }
}
