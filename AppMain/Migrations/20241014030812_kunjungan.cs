using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AppMain.Migrations
{
    /// <inheritdoc />
    public partial class kunjungan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "Anggotas",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Kunjungans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnggotaId = table.Column<int>(type: "integer", nullable: false),
                    Masuk = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Keluar = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Kunjungans_AnggotaId",
                table: "Kunjungans",
                column: "AnggotaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kunjungans");

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "Anggotas",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
