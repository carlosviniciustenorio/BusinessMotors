﻿// <auto-generated />
using System;
using CManager.Infrastructure.Context.CManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CManager.Infrastructure.Migrations.CManagerDB
{
    [DbContext(typeof(CManagerDBContext))]
    partial class CManagerDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AnuncioCaracteristica", b =>
                {
                    b.Property<Guid>("AnuncioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CaracteristicasId")
                        .HasColumnType("int");

                    b.HasKey("AnuncioId", "CaracteristicasId");

                    b.HasIndex("CaracteristicasId");

                    b.ToTable("AnuncioCaracteristica");
                });

            modelBuilder.Entity("AnuncioOpcional", b =>
                {
                    b.Property<Guid>("AnuncioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OpcionaisId")
                        .HasColumnType("int");

                    b.HasKey("AnuncioId", "OpcionaisId");

                    b.HasIndex("OpcionaisId");

                    b.ToTable("AnuncioOpcional");
                });

            modelBuilder.Entity("AnuncioTipoCombustivel", b =>
                {
                    b.Property<Guid>("AnuncioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TiposCombustiveisId")
                        .HasColumnType("int");

                    b.HasKey("AnuncioId", "TiposCombustiveisId");

                    b.HasIndex("TiposCombustiveisId");

                    b.ToTable("AnuncioTipoCombustivel");
                });

            modelBuilder.Entity("CManager.Domain.Models.Anuncio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AnoFabricacao")
                        .HasColumnType("int");

                    b.Property<int>("AnoModelo")
                        .HasColumnType("int");

                    b.Property<int>("Cambio")
                        .HasColumnType("int");

                    b.Property<int>("Cor")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ExibirEmail")
                        .HasColumnType("bit");

                    b.Property<bool>("ExibirTelefone")
                        .HasColumnType("bit");

                    b.Property<string>("Km")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Marca")
                        .HasColumnType("int");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Portas")
                        .HasColumnType("int");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Versao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Anuncio");
                });

            modelBuilder.Entity("CManager.Domain.Models.Caracteristica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Caracteristica");
                });

            modelBuilder.Entity("CManager.Domain.Models.Opcional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Opcional");
                });

            modelBuilder.Entity("CManager.Domain.Models.TipoCombustivel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("TipoCombustivel");
                });

            modelBuilder.Entity("AnuncioCaracteristica", b =>
                {
                    b.HasOne("CManager.Domain.Models.Anuncio", null)
                        .WithMany()
                        .HasForeignKey("AnuncioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CManager.Domain.Models.Caracteristica", null)
                        .WithMany()
                        .HasForeignKey("CaracteristicasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnuncioOpcional", b =>
                {
                    b.HasOne("CManager.Domain.Models.Anuncio", null)
                        .WithMany()
                        .HasForeignKey("AnuncioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CManager.Domain.Models.Opcional", null)
                        .WithMany()
                        .HasForeignKey("OpcionaisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnuncioTipoCombustivel", b =>
                {
                    b.HasOne("CManager.Domain.Models.Anuncio", null)
                        .WithMany()
                        .HasForeignKey("AnuncioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CManager.Domain.Models.TipoCombustivel", null)
                        .WithMany()
                        .HasForeignKey("TiposCombustiveisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}