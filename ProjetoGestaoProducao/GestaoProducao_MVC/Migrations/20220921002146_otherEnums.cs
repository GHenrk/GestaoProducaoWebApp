using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoProducao_MVC.Migrations
{
    public partial class otherEnums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Operacao",
                table: "Apontamento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operacao",
                table: "Apontamento");
        }
    }
}
