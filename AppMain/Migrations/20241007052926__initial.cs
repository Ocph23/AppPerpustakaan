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
                    TanggalLahir = table.Column<DateOnly>(type: "date", nullable: true),
                    NIK = table.Column<string>(type: "text", nullable: false),
                    Agama = table.Column<string>(type: "text", nullable: false),
                    JenisKelamin = table.Column<string>(type: "text", nullable: false),
                    Alamat = table.Column<string>(type: "text", nullable: true),
                    Kelas = table.Column<string>(type: "text", nullable: false),
                    StatusAktif = table.Column<bool>(type: "boolean", nullable: false),
                    JenisKeanggotaan = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anggotas", x => x.Id);
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anggotas");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
