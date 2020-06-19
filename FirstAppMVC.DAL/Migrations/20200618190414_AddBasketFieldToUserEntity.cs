using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstAppMVC.DAL.Migrations
{
    public partial class AddBasketFieldToUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Basket",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Basket",
                table: "AspNetUsers");
        }
    }
}
