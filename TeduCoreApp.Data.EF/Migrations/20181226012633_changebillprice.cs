using Microsoft.EntityFrameworkCore.Migrations;

namespace TeduCoreApp.Data.EF.Migrations
{
    public partial class changebillprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FeeShipping",
                table: "Bills",
                type: "decimal(12,3)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMoneyOrder",
                table: "Bills",
                type: "decimal(12,3)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMoneyPayment",
                table: "Bills",
                type: "decimal(12,3)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeeShipping",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TotalMoneyOrder",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TotalMoneyPayment",
                table: "Bills");
        }
    }
}
