using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P229FirstDb.Migrations
{
    public partial class UpdatedGroupsTableChangedColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "Groups",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Groups",
                newName: "Ad");
        }
    }
}
