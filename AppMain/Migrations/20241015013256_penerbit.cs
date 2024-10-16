using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AppMain.Migrations
{
    /// <inheritdoc />
    public partial class penerbit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Bukus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bukus");

            migrationBuilder.DropTable(
                name: "Penerbits");

            migrationBuilder.DropTable(
                name: "Pengarangs");

            migrationBuilder.DropTable(
                name: "Raks");
        }
    }
}
