using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CManager.Infrastructure.Migrations.CManagerDB
{
    /// <inheritdoc />
    public partial class adjustDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Marca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.Id);
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
                name: "Modelo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnoModelo = table.Column<int>(type: "int", nullable: false),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modelo_Marca_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Versao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Versao_Modelo_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anuncio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false),
                    ModeloId = table.Column<int>(type: "int", nullable: false),
                    VersaoId = table.Column<int>(type: "int", nullable: false),
                    Portas = table.Column<int>(type: "int", nullable: false),
                    Cambio = table.Column<int>(type: "int", nullable: false),
                    Cor = table.Column<int>(type: "int", nullable: false),
                    Km = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExibirTelefone = table.Column<bool>(type: "bit", nullable: false),
                    ExibirEmail = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anuncio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anuncio_Marca_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Anuncio_Modelo_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Anuncio_Versao_VersaoId",
                        column: x => x.VersaoId,
                        principalTable: "Versao",
                        principalColumn: "Id");
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
                name: "IX_Anuncio_MarcaId",
                table: "Anuncio",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Anuncio_ModeloId",
                table: "Anuncio",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Anuncio_VersaoId",
                table: "Anuncio",
                column: "VersaoId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_MarcaId",
                table: "Modelo",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Versao_ModeloId",
                table: "Versao",
                column: "ModeloId");
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

            migrationBuilder.DropTable(
                name: "Versao");

            migrationBuilder.DropTable(
                name: "Modelo");

            migrationBuilder.DropTable(
                name: "Marca");
        }
    }
}
