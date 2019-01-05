using Microsoft.EntityFrameworkCore.Migrations;

namespace TeduCoreApp.Data.EF.Migrations
{
    public partial class changepage2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Pages",
               columns: table => new
               {
                   Id = table.Column<int>(nullable: false),
                   Name = table.Column<string>(maxLength: 255, nullable: false),
                   Alias = table.Column<string>(maxLength: 256, nullable: false),
                   Content = table.Column<string>(nullable: true),
                   Status = table.Column<int>(nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Pages", x => x.Id);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "Pages");
        }
    }
}
