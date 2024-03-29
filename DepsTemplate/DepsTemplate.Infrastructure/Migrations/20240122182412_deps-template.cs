﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DepsTemplate.Infrastructure.Migrations
{
    public partial class depstemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agrupador_parametrizacao",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agrupadores_parametrizacao", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "perfil",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    ordem = table.Column<int>(type: "integer", nullable: false, defaultValue: 999999)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_perfis", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "perfil_metrica",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    perfil_id = table.Column<int>(type: "integer", nullable: false),
                    metrica_id = table.Column<int>(type: "integer", nullable: false),
                    pontuacao_maxima = table.Column<decimal>(type: "numeric", nullable: false),
                    pontuacao_minima = table.Column<decimal>(type: "numeric", nullable: false),
                    validade = table.Column<int>(type: "integer", nullable: true),
                    descricao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_perfis_metricas", x => x.id);
                    table.ForeignKey(
                        name: "fk_perfis_metricas_perfil_perfil_id",
                        column: x => x.perfil_id,
                        principalTable: "perfil",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "parametrizacao_metrica",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    perfil_metrica_id = table.Column<int>(type: "integer", nullable: false),
                    agrupador_parametrizacao_id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    idade = table.Column<int>(type: "integer", nullable: true),
                    valor = table.Column<decimal>(type: "numeric", nullable: true),
                    quantidade = table.Column<int>(type: "integer", nullable: true),
                    impacto = table.Column<decimal>(type: "numeric", nullable: true),
                    pontualidade = table.Column<decimal>(type: "numeric", nullable: true),
                    pontuacao = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_parametrizacao_metricas", x => x.id);
                    table.ForeignKey(
                        name: "fk_parametrizacao_metricas_agrupador_parametrizacao_agrupador_pa~",
                        column: x => x.agrupador_parametrizacao_id,
                        principalTable: "agrupador_parametrizacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_parametrizacao_metricas_perfis_metricas_perfil_metrica_id",
                        column: x => x.perfil_metrica_id,
                        principalTable: "perfil_metrica",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_parametrizacao_metrica_agrupador_parametrizacao_id",
                table: "parametrizacao_metrica",
                column: "agrupador_parametrizacao_id");

            migrationBuilder.CreateIndex(
                name: "IX_parametrizacao_metrica_perfil_metrica_id",
                table: "parametrizacao_metrica",
                column: "perfil_metrica_id");

            migrationBuilder.CreateIndex(
                name: "IX_perfil_metrica_metrica_id_perfil_id",
                table: "perfil_metrica",
                columns: new[] { "metrica_id", "perfil_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_perfil_metrica_perfil_id",
                table: "perfil_metrica",
                column: "perfil_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "parametrizacao_metrica");

            migrationBuilder.DropTable(
                name: "agrupador_parametrizacao");

            migrationBuilder.DropTable(
                name: "perfil_metrica");

            migrationBuilder.DropTable(
                name: "perfil");
        }
    }
}
