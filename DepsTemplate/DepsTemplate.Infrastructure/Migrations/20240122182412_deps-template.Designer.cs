﻿// <auto-generated />
using System;
using DepsTemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DepsTemplate.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240122182412_deps-template")]
    partial class depstemplate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DepsTemplate.Core.Entities.PerfilAggregate.Perfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean")
                        .HasColumnName("ativo");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("nome");

                    b.Property<int>("Ordem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(999999)
                        .HasColumnName("ordem");

                    b.HasKey("Id")
                        .HasName("pk_perfis");

                    b.ToTable("perfil");
                });

            modelBuilder.Entity("DepsTemplate.Core.Entities.PerfilMetricaAggregate.AgrupadorParametrizacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.HasKey("Id")
                        .HasName("pk_agrupadores_parametrizacao");

                    b.ToTable("agrupador_parametrizacao");
                });

            modelBuilder.Entity("DepsTemplate.Core.Entities.PerfilMetricaAggregate.ParametrizacaoMetrica", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AgrupadorParametrizacaoId")
                        .HasColumnType("uuid")
                        .HasColumnName("agrupador_parametrizacao_id");

                    b.Property<string>("Descricao")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("descricao");

                    b.Property<int?>("Idade")
                        .HasColumnType("integer")
                        .HasColumnName("idade");

                    b.Property<decimal?>("Impacto")
                        .HasColumnType("numeric")
                        .HasColumnName("impacto");

                    b.Property<int>("PerfilMetricaId")
                        .HasColumnType("integer")
                        .HasColumnName("perfil_metrica_id");

                    b.Property<decimal>("Pontuacao")
                        .HasColumnType("numeric")
                        .HasColumnName("pontuacao");

                    b.Property<decimal?>("Pontualidade")
                        .HasColumnType("numeric")
                        .HasColumnName("pontualidade");

                    b.Property<int?>("Quantidade")
                        .HasColumnType("integer")
                        .HasColumnName("quantidade");

                    b.Property<decimal?>("Valor")
                        .HasColumnType("numeric")
                        .HasColumnName("valor");

                    b.HasKey("Id")
                        .HasName("pk_parametrizacao_metricas");

                    b.HasIndex("AgrupadorParametrizacaoId");

                    b.HasIndex("PerfilMetricaId");

                    b.ToTable("parametrizacao_metrica");
                });

            modelBuilder.Entity("DepsTemplate.Core.Entities.PerfilMetricaAggregate.PerfilMetrica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.Property<int>("MetricaId")
                        .HasColumnType("integer")
                        .HasColumnName("metrica_id");

                    b.Property<int>("PerfilId")
                        .HasColumnType("integer")
                        .HasColumnName("perfil_id");

                    b.Property<decimal>("PontuacaoMaxima")
                        .HasColumnType("numeric")
                        .HasColumnName("pontuacao_maxima");

                    b.Property<decimal>("PontuacaoMinima")
                        .HasColumnType("numeric")
                        .HasColumnName("pontuacao_minima");

                    b.Property<int?>("Validade")
                        .HasColumnType("integer")
                        .HasColumnName("validade");

                    b.HasKey("Id")
                        .HasName("pk_perfis_metricas");

                    b.HasIndex("PerfilId");

                    b.HasIndex("MetricaId", "PerfilId")
                        .IsUnique();

                    b.ToTable("perfil_metrica");
                });

            modelBuilder.Entity("DepsTemplate.Core.Entities.PerfilMetricaAggregate.ParametrizacaoMetrica", b =>
                {
                    b.HasOne("DepsTemplate.Core.Entities.PerfilMetricaAggregate.AgrupadorParametrizacao", "AgrupadorParametrizacao")
                        .WithMany("ParametrizacoesMetrica")
                        .HasForeignKey("AgrupadorParametrizacaoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_parametrizacao_metricas_agrupador_parametrizacao_agrupador_pa~");

                    b.HasOne("DepsTemplate.Core.Entities.PerfilMetricaAggregate.PerfilMetrica", "PerfilMetrica")
                        .WithMany("ParametrizacoesMetrica")
                        .HasForeignKey("PerfilMetricaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_parametrizacao_metricas_perfis_metricas_perfil_metrica_id");

                    b.Navigation("AgrupadorParametrizacao");

                    b.Navigation("PerfilMetrica");
                });

            modelBuilder.Entity("DepsTemplate.Core.Entities.PerfilMetricaAggregate.PerfilMetrica", b =>
                {
                    b.HasOne("DepsTemplate.Core.Entities.PerfilAggregate.Perfil", "Perfil")
                        .WithMany("PerfilMetricas")
                        .HasForeignKey("PerfilId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_perfis_metricas_perfil_perfil_id");

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("DepsTemplate.Core.Entities.PerfilAggregate.Perfil", b =>
                {
                    b.Navigation("PerfilMetricas");
                });

            modelBuilder.Entity("DepsTemplate.Core.Entities.PerfilMetricaAggregate.AgrupadorParametrizacao", b =>
                {
                    b.Navigation("ParametrizacoesMetrica");
                });

            modelBuilder.Entity("DepsTemplate.Core.Entities.PerfilMetricaAggregate.PerfilMetrica", b =>
                {
                    b.Navigation("ParametrizacoesMetrica");
                });
#pragma warning restore 612, 618
        }
    }
}
