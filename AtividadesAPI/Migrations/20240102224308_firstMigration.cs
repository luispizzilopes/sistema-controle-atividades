using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtividadesAPI.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCategoria = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescricaoCategoria = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DataCriacaoCategoria = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracaoCategoria = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "RegistroLoges",
                columns: table => new
                {
                    RegistroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoRegistro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroLoges", x => x.RegistroId);
                });

            migrationBuilder.CreateTable(
                name: "Atividades",
                columns: table => new
                {
                    AtividadeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeAtividade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescricaoAtividade = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DataCriacaoAtividade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracaoAtividade = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.AtividadeId);
                    table.ForeignKey(
                        name: "FK_Atividades_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_CategoriaId",
                table: "Atividades",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "RegistroLoges");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
