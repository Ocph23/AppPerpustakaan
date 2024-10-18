using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppMain.Migrations
{
    /// <inheritdoc />
    public partial class kodebuku : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kode",
                table: "Bukus",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kode",
                table: "Bukus");
        }
    }
}
