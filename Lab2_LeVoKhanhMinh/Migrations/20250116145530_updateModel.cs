using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab2_LeVoKhanhMinh.Migrations
{
    /// <inheritdoc />
    public partial class updateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceTitle",
                table: "DeviceCategories",
                newName: "CategoryTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryTitle",
                table: "DeviceCategories",
                newName: "DeviceTitle");
        }
    }
}
