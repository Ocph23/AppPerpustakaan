﻿// <auto-generated />
using AppMain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AppMain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241001030729__users")]
    partial class _users
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AppMain.Anggota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Agama")
                        .HasColumnType("text");

                    b.Property<string>("Alamat")
                        .HasColumnType("text");

                    b.Property<int>("JenisKeanggotaan")
                        .HasColumnType("integer");

                    b.Property<string>("JenisKelamin")
                        .HasColumnType("text");

                    b.Property<string>("Kelas")
                        .HasColumnType("text");

                    b.Property<string>("NIK")
                        .HasColumnType("text");

                    b.Property<string>("NISN")
                        .HasColumnType("text");

                    b.Property<string>("Nama")
                        .HasColumnType("text");

                    b.Property<bool>("StatusAktif")
                        .HasColumnType("boolean");

                    b.Property<string>("TanggalLahir")
                        .HasColumnType("text");

                    b.Property<string>("TempatLahir")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Anggotas");
                });

            modelBuilder.Entity("AppMain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Aktif")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}