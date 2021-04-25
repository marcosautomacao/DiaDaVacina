using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class comit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "DataVacina",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "DataVacina");
        }
    }
}
