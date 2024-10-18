﻿// <auto-generated />
using System;
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
    [Migration("20241015013256_penerbit")]
    partial class penerbit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AppMain.Models.Anggota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Agama")
                        .HasColumnType("integer");

                    b.Property<string>("Alamat")
                        .HasColumnType("text");

                    b.Property<int>("JenisKeanggotaan")
                        .HasColumnType("integer");

                    b.Property<int>("JenisKelamin")
                        .HasColumnType("integer");

                    b.Property<string>("Kelas")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NIK")
                        .HasColumnType("text");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NomorKartu")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .HasColumnType("text");

                    b.Property<bool>("StatusAktif")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("TanggalLahir")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TempatLahir")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Anggotas");
                });

            modelBuilder.Entity("AppMain.Models.Buku", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ISBN")
                        .HasColumnType("text");

                    b.Property<string>("Judul")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Jumlah")
                        .HasColumnType("integer");

                    b.Property<int>("PenerbitId")
                        .HasColumnType("integer");

                    b.Property<int>("PengarangId")
                        .HasColumnType("integer");

                    b.Property<int>("RakId")
                        .HasColumnType("integer");

                    b.Property<int>("Tahun")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PenerbitId");

                    b.HasIndex("PengarangId");

                    b.HasIndex("RakId");

                    b.ToTable("Bukus");
                });

            modelBuilder.Entity("AppMain.Models.Kunjungan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AnggotaId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Keluar")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Masuk")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AnggotaId");

                    b.ToTable("Kunjungans");
                });

            modelBuilder.Entity("AppMain.Models.Penerbit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Alamat")
                        .HasColumnType("text");

                    b.Property<string>("Nama")
                        .HasColumnType("text");

                    b.Property<string>("Telp")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Penerbits");
                });

            modelBuilder.Entity("AppMain.Models.Pengarang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Alamat")
                        .HasColumnType("text");

                    b.Property<string>("Nama")
                        .HasColumnType("text");

                    b.Property<string>("Telp")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Pengarangs");
                });

            modelBuilder.Entity("AppMain.Models.Rak", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Kode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Lokasi")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Raks");
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

            modelBuilder.Entity("AppMain.Models.Buku", b =>
                {
                    b.HasOne("AppMain.Models.Penerbit", "Penerbit")
                        .WithMany()
                        .HasForeignKey("PenerbitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppMain.Models.Pengarang", "Pengarang")
                        .WithMany()
                        .HasForeignKey("PengarangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppMain.Models.Rak", "Rak")
                        .WithMany()
                        .HasForeignKey("RakId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Penerbit");

                    b.Navigation("Pengarang");

                    b.Navigation("Rak");
                });

            modelBuilder.Entity("AppMain.Models.Kunjungan", b =>
                {
                    b.HasOne("AppMain.Models.Anggota", "Anggota")
                        .WithMany()
                        .HasForeignKey("AnggotaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anggota");
                });
#pragma warning restore 612, 618
        }
    }
}