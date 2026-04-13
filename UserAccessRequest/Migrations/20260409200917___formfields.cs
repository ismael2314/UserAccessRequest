using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAccessRequest.Migrations
{
    /// <inheritdoc />
    public partial class __formfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FieldValue",
                table: "Fields",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FieldValueType",
                table: "Fields",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "formFields",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FormId = table.Column<string>(type: "text", nullable: false),
                    FieldId = table.Column<string>(type: "text", nullable: false),
                    Iacrive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_formFields_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_formFields_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_formFields_FieldId",
                table: "formFields",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_formFields_FormId",
                table: "formFields",
                column: "FormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "formFields");

            migrationBuilder.DropColumn(
                name: "FieldValue",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "FieldValueType",
                table: "Fields");
        }
    }
}
