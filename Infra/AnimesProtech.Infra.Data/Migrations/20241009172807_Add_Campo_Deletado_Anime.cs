using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimesProtech.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Campo_Deletado_Anime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                schema: "Animes",
                table: "Anime",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deletado",
                schema: "Animes",
                table: "Anime");
        }
    }
}
