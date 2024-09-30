using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AppMain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    Nama = table.Column<string>(type: "text", nullable: true),
                    NISN = table.Column<string>(type: "text", nullable: true),
                    TempatLahir = table.Column<string>(type: "text", nullable: true),
                    TanggalLahir = table.Column<string>(type: "text", nullable: true),
                    NIK = table.Column<string>(type: "text", nullable: true),
                    Agama = table.Column<string>(type: "text", nullable: true),
                    JenisKelamin = table.Column<string>(type: "text", nullable: true),
                    Alamat = table.Column<string>(type: "text", nullable: true),
                    Kelas = table.Column<string>(type: "text", nullable: true),
                    StatusAktif = table.Column<bool>(type: "boolean", nullable: false),
                    JenisKeanggotaan = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anggotas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anggotas");
        }
    }
}
