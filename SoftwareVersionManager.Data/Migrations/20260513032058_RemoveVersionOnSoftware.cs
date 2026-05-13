using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareVersionManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveVersionOnSoftware : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Software");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Software",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
