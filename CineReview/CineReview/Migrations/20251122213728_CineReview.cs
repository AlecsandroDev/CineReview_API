using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineReview.Migrations
{
    /// <inheritdoc />
    public partial class CineReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Midias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sinopse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duracao = table.Column<TimeSpan>(type: "time", nullable: false),
                    ClassificacaoIndicativa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataLancamento = table.Column<DateOnly>(type: "date", nullable: false),
                    TipoMidia = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Midias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUsuario = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Temporadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroTemporada = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sinopse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassificacaoIndicativa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataLancamento = table.Column<DateOnly>(type: "date", nullable: false),
                    SerieId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temporadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temporadas_Midias_SerieId",
                        column: x => x.SerieId,
                        principalTable: "Midias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MidiaUsuario",
                columns: table => new
                {
                    FavoritosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MidiaUsuario", x => new { x.FavoritosId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_MidiaUsuario_Midias_FavoritosId",
                        column: x => x.FavoritosId,
                        principalTable: "Midias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MidiaUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAvaliacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvaliadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotaTrama = table.Column<int>(type: "int", nullable: false),
                    NotaRitmo = table.Column<int>(type: "int", nullable: false),
                    NotaDevPersonagens = table.Column<int>(type: "int", nullable: false),
                    NotaConstrucaoMundo = table.Column<int>(type: "int", nullable: false),
                    NotaTematica = table.Column<int>(type: "int", nullable: false),
                    NotaAtuacao = table.Column<int>(type: "int", nullable: false),
                    NotaEdicao = table.Column<int>(type: "int", nullable: false),
                    NotaDirecao = table.Column<int>(type: "int", nullable: false),
                    NotaArte = table.Column<int>(type: "int", nullable: false),
                    NotaCinematografia = table.Column<int>(type: "int", nullable: false),
                    NotaCenarios = table.Column<int>(type: "int", nullable: false),
                    NotaFigurinos = table.Column<int>(type: "int", nullable: false),
                    NotaEfeitosVisuais = table.Column<int>(type: "int", nullable: false),
                    NotaQualidadeImagem = table.Column<int>(type: "int", nullable: false),
                    NotaScore = table.Column<int>(type: "int", nullable: false),
                    NotaEfeitosSonoros = table.Column<int>(type: "int", nullable: false),
                    FilmeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TemporadaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Midias_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "Midias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Temporadas_TemporadaId",
                        column: x => x.TemporadaId,
                        principalTable: "Temporadas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episodios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroEpisodio = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sinopse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duracao = table.Column<TimeSpan>(type: "time", nullable: false),
                    TemporadaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodios_Temporadas_TemporadaId",
                        column: x => x.TemporadaId,
                        principalTable: "Temporadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Funcoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MidiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TemporadaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TipoMembro = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Papel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipes_Midias_MidiaId",
                        column: x => x.MidiaId,
                        principalTable: "Midias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipes_Temporadas_TemporadaId",
                        column: x => x.TemporadaId,
                        principalTable: "Temporadas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_FilmeId",
                table: "Avaliacoes",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_TemporadaId",
                table: "Avaliacoes",
                column: "TemporadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_UsuarioId",
                table: "Avaliacoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodios_TemporadaId",
                table: "Episodios",
                column: "TemporadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_MidiaId",
                table: "Equipes",
                column: "MidiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_TemporadaId",
                table: "Equipes",
                column: "TemporadaId");

            migrationBuilder.CreateIndex(
                name: "IX_MidiaUsuario_UsuarioId",
                table: "MidiaUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Temporadas_SerieId",
                table: "Temporadas",
                column: "SerieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "Episodios");

            migrationBuilder.DropTable(
                name: "Equipes");

            migrationBuilder.DropTable(
                name: "MidiaUsuario");

            migrationBuilder.DropTable(
                name: "Temporadas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Midias");
        }
    }
}
