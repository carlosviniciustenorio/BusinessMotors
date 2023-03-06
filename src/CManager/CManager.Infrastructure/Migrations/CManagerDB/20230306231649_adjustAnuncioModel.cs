using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CManager.Infrastructure.Migrations.CManagerDB
{
    /// <inheritdoc />
    public partial class adjustAnuncioModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnoFabricacao",
                table: "Anuncio");

            migrationBuilder.DropColumn(
                name: "Versao",
                table: "Anuncio");

            migrationBuilder.RenameColumn(
                name: "AnoModelo",
                table: "Anuncio",
                newName: "ModeloId");

            migrationBuilder.CreateTable(
                name: "Versao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modelo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VersaoId = table.Column<int>(type: "int", nullable: false),
                    AnoModelo = table.Column<int>(type: "int", nullable: false),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modelo_Versao_VersaoId",
                        column: x => x.VersaoId,
                        principalTable: "Versao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anuncio_ModeloId",
                table: "Anuncio",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_VersaoId",
                table: "Modelo",
                column: "VersaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Anuncio_Modelo_ModeloId",
                table: "Anuncio",
                column: "ModeloId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anuncio_Modelo_ModeloId",
                table: "Anuncio");

            migrationBuilder.DropTable(
                name: "Modelo");

            migrationBuilder.DropTable(
                name: "Versao");

            migrationBuilder.DropIndex(
                name: "IX_Anuncio_ModeloId",
                table: "Anuncio");

            migrationBuilder.RenameColumn(
                name: "ModeloId",
                table: "Anuncio",
                newName: "AnoModelo");

            migrationBuilder.AddColumn<int>(
                name: "AnoFabricacao",
                table: "Anuncio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Versao",
                table: "Anuncio",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
