using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmesAPI.Migrations
{
    public partial class CorrigindobugnamigrationderelaçãoentreGerenteeCinema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Gerentes",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Nome",
                table: "Gerentes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
