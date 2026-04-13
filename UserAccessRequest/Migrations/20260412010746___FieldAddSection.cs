using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAccessRequest.Migrations
{
    /// <inheritdoc />
    public partial class __FieldAddSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FieldSection",
                table: "Fields",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldSection",
                table: "Fields");
        }
    }
}
