using Microsoft.EntityFrameworkCore.Migrations;

namespace TeduCoreApp.Data.EF.Migrations
{
    public partial class advertistment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertistmentPositions_AdvertistmentPages_PageId",
                table: "AdvertistmentPositions");

            migrationBuilder.DropIndex(
                name: "IX_AdvertistmentPositions_PageId",
                table: "AdvertistmentPositions");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "AdvertistmentPositions");

            migrationBuilder.AddColumn<string>(
                name: "PageId",
                table: "Advertistments",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertistments_PageId",
                table: "Advertistments",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertistments_AdvertistmentPages_PageId",
                table: "Advertistments",
                column: "PageId",
                principalTable: "AdvertistmentPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertistments_AdvertistmentPages_PageId",
                table: "Advertistments");

            migrationBuilder.DropIndex(
                name: "IX_Advertistments_PageId",
                table: "Advertistments");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "Advertistments");

            migrationBuilder.AddColumn<string>(
                name: "PageId",
                table: "AdvertistmentPositions",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdvertistmentPositions_PageId",
                table: "AdvertistmentPositions",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertistmentPositions_AdvertistmentPages_PageId",
                table: "AdvertistmentPositions",
                column: "PageId",
                principalTable: "AdvertistmentPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
