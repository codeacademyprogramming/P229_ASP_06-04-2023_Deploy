using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P229MentorHomeWork.Migrations
{
    public partial class AddedPricingOffersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PricingOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricingId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricingOffers_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PricingOffers_Pricings_PricingId",
                        column: x => x.PricingId,
                        principalTable: "Pricings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PricingOffers_OfferId",
                table: "PricingOffers",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_PricingOffers_PricingId",
                table: "PricingOffers",
                column: "PricingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PricingOffers");
        }
    }
}
