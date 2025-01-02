using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppApi.Migrations
{
    /// <inheritdoc />
    public partial class unique_stuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_Name_Email",
                table: "Users",
                columns: new[] { "Id", "Name", "Email" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Id_Name_Email",
                table: "Users");
        }
    }
}
