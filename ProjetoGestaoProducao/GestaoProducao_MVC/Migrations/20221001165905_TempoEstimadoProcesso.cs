using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoProducao_MVC.Migrations
{
    public partial class TempoEstimadoProcesso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TempoEstimado",
                table: "Processo",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempoEstimado",
                table: "Processo");
        }
    }
}
