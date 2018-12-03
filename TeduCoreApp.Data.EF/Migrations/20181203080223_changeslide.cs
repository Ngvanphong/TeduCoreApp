using Microsoft.EntityFrameworkCore.Migrations;

namespace TeduCoreApp.Data.EF.Migrations
{
    public partial class changeslide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupAlias",
                table: "Slides");

            migrationBuilder.AddColumn<bool>(
                name: "OrtherPageHome",
                table: "Slides",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrtherPageHome",
                table: "Slides");

            migrationBuilder.AddColumn<string>(
                name: "GroupAlias",
                table: "Slides",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }
    }
}
