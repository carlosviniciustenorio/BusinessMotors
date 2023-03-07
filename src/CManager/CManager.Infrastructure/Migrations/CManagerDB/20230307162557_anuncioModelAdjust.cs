using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CManager.Infrastructure.Migrations.CManagerDB
{
    /// <inheritdoc />
    public partial class anuncioModelAdjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anuncio_Marca_MarcaId",
                table: "Anuncio");

            migrationBuilder.DropIndex(
                name: "IX_Anuncio_MarcaId",
                table: "Anuncio");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "Anuncio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "Anuncio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Anuncio_MarcaId",
                table: "Anuncio",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Anuncio_Marca_MarcaId",
                table: "Anuncio",
                column: "MarcaId",
                principalTable: "Marca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
