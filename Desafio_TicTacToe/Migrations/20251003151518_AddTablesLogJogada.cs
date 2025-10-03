using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Desafio_TicTacToe.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesLogJogada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogModelId",
                table: "Jogadores",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Vencedor = table.Column<string>(type: "text", nullable: false),
                    DataHoraPartida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jogadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Jogador = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Posicao = table.Column<int>(type: "integer", nullable: false),
                    DataHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LogModelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jogadas_Logs_LogModelId",
                        column: x => x.LogModelId,
                        principalTable: "Logs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_LogModelId",
                table: "Jogadores",
                column: "LogModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogadas_LogModelId",
                table: "Jogadas",
                column: "LogModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogadores_Logs_LogModelId",
                table: "Jogadores",
                column: "LogModelId",
                principalTable: "Logs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogadores_Logs_LogModelId",
                table: "Jogadores");

            migrationBuilder.DropTable(
                name: "Jogadas");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Jogadores_LogModelId",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "LogModelId",
                table: "Jogadores");
        }
    }
}
