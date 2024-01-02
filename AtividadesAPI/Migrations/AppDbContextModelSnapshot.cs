﻿// <auto-generated />
using System;
using AtividadesAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AtividadesAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AtividadesAPI.Models.Atividade", b =>
                {
                    b.Property<int>("AtividadeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AtividadeId"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAlteracaoAtividade")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacaoAtividade")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescricaoAtividade")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NomeAtividade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AtividadeId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Atividades");
                });

            modelBuilder.Entity("AtividadesAPI.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoriaId"));

                    b.Property<DateTime?>("DataAlteracaoCategoria")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacaoCategoria")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescricaoCategoria")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NomeCategoria")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("AtividadesAPI.Models.RegistroLog", b =>
                {
                    b.Property<int>("RegistroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegistroId"));

                    b.Property<string>("DescricaoRegistro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegistroId");

                    b.ToTable("RegistroLoges");
                });

            modelBuilder.Entity("AtividadesAPI.Models.Atividade", b =>
                {
                    b.HasOne("AtividadesAPI.Models.Categoria", "Categoria")
                        .WithMany("Atividades")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("AtividadesAPI.Models.Categoria", b =>
                {
                    b.Navigation("Atividades");
                });
#pragma warning restore 612, 618
        }
    }
}
