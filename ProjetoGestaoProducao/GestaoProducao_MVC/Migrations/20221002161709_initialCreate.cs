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
                name: "CodigoParada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigoParada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maquina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdemProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantidadeProduto = table.Column<int>(type: "int", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpStatus = table.Column<int>(type: "int", nullable: false)
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
                    OrdemProdutoId = table.Column<int>(type: "int", nullable: false),
                    TempoEstimado = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processo_OrdemProduto_OrdemProdutoId",
                        column: x => x.OrdemProdutoId,
                        principalTable: "OrdemProduto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Apontamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TempoTotal = table.Column<long>(type: "bigint", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAtivo = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Operacao = table.Column<int>(type: "int", nullable: true),
                    ProcessoId = table.Column<int>(type: "int", nullable: false),
                    MaquinaId = table.Column<int>(type: "int", nullable: false),
                    FuncionarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apontamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apontamento_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apontamento_Maquina_MaquinaId",
                        column: x => x.MaquinaId,
                        principalTable: "Maquina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apontamento_Processo_ProcessoId",
                        column: x => x.ProcessoId,
                        principalTable: "Processo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistroParada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TempoTotal = table.Column<long>(type: "bigint", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParadaAtiva = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CodigoParadaId = table.Column<int>(type: "int", nullable: false),
                    ApontamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroParada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroParada_Apontamento_ApontamentoId",
                        column: x => x.ApontamentoId,
                        principalTable: "Apontamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroParada_CodigoParada_CodigoParadaId",
                        column: x => x.CodigoParadaId,
                        principalTable: "CodigoParada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apontamento_FuncionarioId",
                table: "Apontamento",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Apontamento_MaquinaId",
                table: "Apontamento",
                column: "MaquinaId");

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

            migrationBuilder.CreateIndex(
                name: "IX_RegistroParada_CodigoParadaId",
                table: "RegistroParada",
                column: "CodigoParadaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroParada");

            migrationBuilder.DropTable(
                name: "Apontamento");

            migrationBuilder.DropTable(
                name: "CodigoParada");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Maquina");

            migrationBuilder.DropTable(
                name: "Processo");

            migrationBuilder.DropTable(
                name: "OrdemProduto");
        }
    }
}
