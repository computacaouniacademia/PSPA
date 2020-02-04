﻿// <auto-generated />
using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WebApi.Migrations
{
    [DbContext(typeof(IdentityContext))]
    [Migration("20190622183849_v5")]
    partial class v5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Infra.Entidades.ArquivoBase", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("IndexID");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<long?>("PedidoImportacaoID");

                    b.HasKey("ID");

                    b.HasIndex("IndexID");

                    b.HasIndex("PedidoImportacaoID");

                    b.ToTable("ArquivoBase");
                });

            modelBuilder.Entity("Infra.Entidades.Cabecalho", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("ArquivoBaseID");

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("ArquivoBaseID");

                    b.ToTable("Cabecalho");
                });

            modelBuilder.Entity("Infra.Entidades.Index", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Indice");
                });

            modelBuilder.Entity("Infra.Entidades.LinhaPedidoImportacao", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("ArquivoBaseID");

                    b.Property<string>("EstaFeito");

                    b.Property<long?>("PedidoImportacaoID");

                    b.HasKey("ID");

                    b.HasIndex("ArquivoBaseID");

                    b.HasIndex("PedidoImportacaoID");

                    b.ToTable("LinhaPedidoImportacao");
                });

            modelBuilder.Entity("Infra.Entidades.LogPedidoImportacao", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<string>("IndicadorStatus")
                        .IsRequired()
                        .HasMaxLength(1);

                    b.Property<long>("PedidoImportacaoID");

                    b.HasKey("ID");

                    b.HasIndex("PedidoImportacaoID");

                    b.ToTable("LogPedidoImportacao");
                });

            modelBuilder.Entity("Infra.Entidades.PedidoImportacao", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DataTermino");

                    b.Property<string>("Estado")
                        .HasMaxLength(1);

                    b.Property<string>("UrlOrigem");

                    b.Property<string>("UsuarioId");

                    b.HasKey("ID");

                    b.HasIndex("UsuarioId");

                    b.ToTable("PedidoImportacao");
                });

            modelBuilder.Entity("Infra.Entidades.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecondName");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("usuario","PSPABase");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Infra.Entidades.ArquivoBase", b =>
                {
                    b.HasOne("Infra.Entidades.Index", "Index")
                        .WithMany("ArquivoBases")
                        .HasForeignKey("IndexID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infra.Entidades.PedidoImportacao", "PedidoImportacao")
                        .WithMany("Arquivos")
                        .HasForeignKey("PedidoImportacaoID");
                });

            modelBuilder.Entity("Infra.Entidades.Cabecalho", b =>
                {
                    b.HasOne("Infra.Entidades.ArquivoBase", "ArquivoBase")
                        .WithMany("Cabecalhos")
                        .HasForeignKey("ArquivoBaseID");
                });

            modelBuilder.Entity("Infra.Entidades.LinhaPedidoImportacao", b =>
                {
                    b.HasOne("Infra.Entidades.ArquivoBase", "ArquivoBase")
                        .WithMany()
                        .HasForeignKey("ArquivoBaseID");

                    b.HasOne("Infra.Entidades.PedidoImportacao", "PedidoImportacao")
                        .WithMany()
                        .HasForeignKey("PedidoImportacaoID");
                });

            modelBuilder.Entity("Infra.Entidades.LogPedidoImportacao", b =>
                {
                    b.HasOne("Infra.Entidades.PedidoImportacao", "PedidoImportacao")
                        .WithMany("LogPedidoImportacao")
                        .HasForeignKey("PedidoImportacaoID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Infra.Entidades.PedidoImportacao", b =>
                {
                    b.HasOne("Infra.Entidades.Usuario", "Usuario")
                        .WithMany("PedidosImportacao")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Infra.Entidades.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Infra.Entidades.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Infra.Entidades.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Infra.Entidades.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
