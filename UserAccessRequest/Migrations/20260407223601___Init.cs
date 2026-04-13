using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAccessRequest.Migrations
{
    /// <inheritdoc />
    public partial class __Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FiledName = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    FieldType = table.Column<string>(type: "text", nullable: false),
                    FieldIsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    FieldPlaceholder = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FormName = table.Column<string>(type: "text", nullable: false),
                    FormDescription = table.Column<string>(type: "text", nullable: false),
                    FormDepartment = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    FormAction = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestForms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FormId = table.Column<string>(type: "text", nullable: false),
                    FieldId = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestForms_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestForms_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestFormsResult",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    RequestFormId = table.Column<string>(type: "text", nullable: false),
                    FieldValue = table.Column<string>(type: "text", nullable: true),
                    RequestStatus = table.Column<string>(type: "text", nullable: true),
                    IsRejected = table.Column<bool>(type: "boolean", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    RejectedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RejectedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    RequestId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestFormsResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestFormsResult_RequestForms_RequestFormId",
                        column: x => x.RequestFormId,
                        principalTable: "RequestForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestForms_FieldId",
                table: "RequestForms",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForms_FormId",
                table: "RequestForms",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestFormsResult_RequestFormId",
                table: "RequestFormsResult",
                column: "RequestFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestFormsResult");

            migrationBuilder.DropTable(
                name: "RequestForms");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Forms");
        }
    }
}
