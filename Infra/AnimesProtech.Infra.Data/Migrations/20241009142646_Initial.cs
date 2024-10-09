using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimesProtech.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Animes");

            migrationBuilder.CreateTable(
                name: "Anime",
                schema: "Animes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Resumo = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Diretor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DataDeCriacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DataDeAlteracao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anime", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anime",
                schema: "Animes");
        }
    }
}
