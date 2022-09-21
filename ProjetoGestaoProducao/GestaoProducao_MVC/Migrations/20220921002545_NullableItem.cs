using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoProducao_MVC.Migrations
{
    public partial class NullableItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processo_OrdemProduto_OrdemProdutoId",
                table: "Processo");

            migrationBuilder.AlterColumn<int>(
                name: "OrdemProdutoId",
                table: "Processo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Processo_OrdemProduto_OrdemProdutoId",
                table: "Processo",
                column: "OrdemProdutoId",
                principalTable: "OrdemProduto",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processo_OrdemProduto_OrdemProdutoId",
                table: "Processo");

            migrationBuilder.AlterColumn<int>(
                name: "OrdemProdutoId",
                table: "Processo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Processo_OrdemProduto_OrdemProdutoId",
                table: "Processo",
                column: "OrdemProdutoId",
                principalTable: "OrdemProduto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
