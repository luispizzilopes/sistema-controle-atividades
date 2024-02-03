using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtividadesAPI.Migrations
{
    /// <inheritdoc />
    public partial class fkCategoriaAtividadeFutura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "AtividadesFuturas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AtividadesFuturas_CategoriaId",
                table: "AtividadesFuturas",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AtividadesFuturas_Categorias_CategoriaId",
                table: "AtividadesFuturas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AtividadesFuturas_Categorias_CategoriaId",
                table: "AtividadesFuturas");

            migrationBuilder.DropIndex(
                name: "IX_AtividadesFuturas_CategoriaId",
                table: "AtividadesFuturas");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "AtividadesFuturas");
        }
    }
}
