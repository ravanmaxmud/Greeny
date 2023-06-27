using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrennyWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Contacts",
                newName: "Subject");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Contacts",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
