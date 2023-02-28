using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CManager.Infrastructure.Migrations.CManagerDB
{
    /// <inheritdoc />
    public partial class initModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anuncio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<int>(type: "int", nullable: false),
                    AnoModelo = table.Column<int>(type: "int", nullable: false),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false),
                    Versao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Portas = table.Column<int>(type: "int", nullable: false),
                    Cambio = table.Column<int>(type: "int", nullable: false),
                    Cor = table.Column<int>(type: "int", nullable: false),
                    Km = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExibirTelefone = table.Column<bool>(type: "bit", nullable: false),
                    ExibirEmail = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anuncio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Caracteristica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caracteristica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opcional",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opcional", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoCombustivel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCombustivel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnuncioCaracteristica",
                columns: table => new
                {
                    AnuncioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaracteristicasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnuncioCaracteristica", x => new { x.AnuncioId, x.CaracteristicasId });
                    table.ForeignKey(
                        name: "FK_AnuncioCaracteristica_Anuncio_AnuncioId",
                        column: x => x.AnuncioId,
                        principalTable: "Anuncio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnuncioCaracteristica_Caracteristica_CaracteristicasId",
                        column: x => x.CaracteristicasId,
                        principalTable: "Caracteristica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnuncioOpcional",
                columns: table => new
                {
                    AnuncioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpcionaisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnuncioOpcional", x => new { x.AnuncioId, x.OpcionaisId });
                    table.ForeignKey(
                        name: "FK_AnuncioOpcional_Anuncio_AnuncioId",
                        column: x => x.AnuncioId,
                        principalTable: "Anuncio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnuncioOpcional_Opcional_OpcionaisId",
                        column: x => x.OpcionaisId,
                        principalTable: "Opcional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnuncioTipoCombustivel",
                columns: table => new
                {
                    AnuncioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TiposCombustiveisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnuncioTipoCombustivel", x => new { x.AnuncioId, x.TiposCombustiveisId });
                    table.ForeignKey(
                        name: "FK_AnuncioTipoCombustivel_Anuncio_AnuncioId",
                        column: x => x.AnuncioId,
                        principalTable: "Anuncio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnuncioTipoCombustivel_TipoCombustivel_TiposCombustiveisId",
                        column: x => x.TiposCombustiveisId,
                        principalTable: "TipoCombustivel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnuncioCaracteristica_CaracteristicasId",
                table: "AnuncioCaracteristica",
                column: "CaracteristicasId");

            migrationBuilder.CreateIndex(
                name: "IX_AnuncioOpcional_OpcionaisId",
                table: "AnuncioOpcional",
                column: "OpcionaisId");

            migrationBuilder.CreateIndex(
                name: "IX_AnuncioTipoCombustivel_TiposCombustiveisId",
                table: "AnuncioTipoCombustivel",
                column: "TiposCombustiveisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnuncioCaracteristica");

            migrationBuilder.DropTable(
                name: "AnuncioOpcional");

            migrationBuilder.DropTable(
                name: "AnuncioTipoCombustivel");

            migrationBuilder.DropTable(
                name: "Caracteristica");

            migrationBuilder.DropTable(
                name: "Opcional");

            migrationBuilder.DropTable(
                name: "Anuncio");

            migrationBuilder.DropTable(
                name: "TipoCombustivel");
        }
    }
}
