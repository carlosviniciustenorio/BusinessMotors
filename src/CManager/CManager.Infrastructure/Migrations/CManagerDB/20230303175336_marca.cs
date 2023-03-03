using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CManager.Infrastructure.Migrations.CManagerDB
{
    /// <inheritdoc />
    public partial class marca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Marca",
                table: "Anuncio",
                newName: "MarcaId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anuncio_Marca_MarcaId",
                table: "Anuncio");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropIndex(
                name: "IX_Anuncio_MarcaId",
                table: "Anuncio");

            migrationBuilder.RenameColumn(
                name: "MarcaId",
                table: "Anuncio",
                newName: "Marca");
        }
    }
}
