using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AppMain.Migrations
{
    /// <inheritdoc />
    public partial class _initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anggotas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nama = table.Column<string>(type: "text", nullable: false),
                    NomorKartu = table.Column<string>(type: "text", nullable: false),
                    TempatLahir = table.Column<string>(type: "text", nullable: false),
                    TanggalLahir = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NIK = table.Column<string>(type: "text", nullable: true),
                    Agama = table.Column<int>(type: "integer", nullable: false),
                    JenisKelamin = table.Column<int>(type: "integer", nullable: false),
                    Alamat = table.Column<string>(type: "text", nullable: true),
                    Kelas = table.Column<string>(type: "text", nullable: false),
                    StatusAktif = table.Column<bool>(type: "boolean", nullable: false),
                    JenisKeanggotaan = table.Column<int>(type: "integer", nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anggotas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Penerbits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nama = table.Column<string>(type: "text", nullable: true),
                    Alamat = table.Column<string>(type: "text", nullable: true),
                    Telp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penerbits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pengarangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nama = table.Column<string>(type: "text", nullable: true),
                    Alamat = table.Column<string>(type: "text", nullable: true),
                    Telp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pengarangs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Raks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Kode = table.Column<string>(type: "text", nullable: false),
                    Lokasi = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Aktif = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kunjungans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnggotaId = table.Column<int>(type: "integer", nullable: false),
                    Masuk = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Keluar = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunjungans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kunjungans_Anggotas_AnggotaId",
                        column: x => x.AnggotaId,
                        principalTable: "Anggotas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bukus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Kode = table.Column<string>(type: "text", nullable: false),
                    Judul = table.Column<string>(type: "text", nullable: false),
                    Tahun = table.Column<int>(type: "integer", nullable: false),
                    Jumlah = table.Column<int>(type: "integer", nullable: false),
                    ISBN = table.Column<string>(type: "text", nullable: true),
                    PenerbitId = table.Column<int>(type: "integer", nullable: false),
                    PengarangId = table.Column<int>(type: "integer", nullable: false),
                    RakId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bukus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bukus_Penerbits_PenerbitId",
                        column: x => x.PenerbitId,
                        principalTable: "Penerbits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bukus_Pengarangs_PengarangId",
                        column: x => x.PengarangId,
                        principalTable: "Pengarangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bukus_Raks_RakId",
                        column: x => x.RakId,
                        principalTable: "Raks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bukus_PenerbitId",
                table: "Bukus",
                column: "PenerbitId");

            migrationBuilder.CreateIndex(
                name: "IX_Bukus_PengarangId",
                table: "Bukus",
                column: "PengarangId");

            migrationBuilder.CreateIndex(
                name: "IX_Bukus_RakId",
                table: "Bukus",
                column: "RakId");

            migrationBuilder.CreateIndex(
                name: "IX_Kunjungans_AnggotaId",
                table: "Kunjungans",
                column: "AnggotaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bukus");

            migrationBuilder.DropTable(
                name: "Kunjungans");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Penerbits");

            migrationBuilder.DropTable(
                name: "Pengarangs");

            migrationBuilder.DropTable(
                name: "Raks");

            migrationBuilder.DropTable(
                name: "Anggotas");
        }
    }
}
