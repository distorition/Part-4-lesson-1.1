using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.DAL.Migrations
{
    public partial class Description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutMySelf",
                table: "buyers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "AboutMySelf",
                table: "buyers");
        }
    }
}
