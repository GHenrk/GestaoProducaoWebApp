using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoProducao_MVC.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdemProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantidadeProduto = table.Column<int>(type: "int", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemProduto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoPeca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantidadePeca = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrdemProdutoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processo_OrdemProduto_OrdemProdutoId",
                        column: x => x.OrdemProdutoId,
                        principalTable: "OrdemProduto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Apontamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TempoTotal = table.Column<TimeSpan>(type: "time", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProcessoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apontamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apontamento_Processo_ProcessoId",
                        column: x => x.ProcessoId,
                        principalTable: "Processo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RegistroParada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TempoTotal = table.Column<TimeSpan>(type: "time", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ApontamentoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroParada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroParada_Apontamento_ApontamentoId",
                        column: x => x.ApontamentoId,
                        principalTable: "Apontamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apontamento_ProcessoId",
                table: "Apontamento",
                column: "ProcessoId");

            migrationBuilder.CreateIndex(
                name: "IX_Processo_OrdemProdutoId",
                table: "Processo",
                column: "OrdemProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroParada_ApontamentoId",
                table: "RegistroParada",
                column: "ApontamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroParada");

            migrationBuilder.DropTable(
                name: "Apontamento");

            migrationBuilder.DropTable(
                name: "Processo");

            migrationBuilder.DropTable(
                name: "OrdemProduto");
        }
    }
}
