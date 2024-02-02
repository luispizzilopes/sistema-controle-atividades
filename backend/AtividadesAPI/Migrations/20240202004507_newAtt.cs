using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtividadesAPI.Migrations
{
    /// <inheritdoc />
    public partial class newAtt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AtividadesFuturas",
                columns: table => new
                {
                    AtividadeFuturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeAtividade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescricaoAtividade = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DataPrevista = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRealizada = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtividadesFuturas", x => x.AtividadeFuturaId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtividadesFuturas");
        }
    }
}
