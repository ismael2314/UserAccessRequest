using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAccessRequest.Migrations
{
    /// <inheritdoc />
    public partial class __FieldAddSection_deleteBehviors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_formFields_Fields_FieldId",
                table: "formFields");

            migrationBuilder.DropForeignKey(
                name: "FK_formFields_Forms_FormId",
                table: "formFields");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestForms_Forms_FormId",
                table: "RequestForms");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestFormsResult_Fields_FieldId",
                table: "RequestFormsResult");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestFormsResult_RequestForms_RequestFormId",
                table: "RequestFormsResult");

            migrationBuilder.AddForeignKey(
                name: "FK_formFields_Fields_FieldId",
                table: "formFields",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_formFields_Forms_FormId",
                table: "formFields",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForms_Forms_FormId",
                table: "RequestForms",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFormsResult_Fields_FieldId",
                table: "RequestFormsResult",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFormsResult_RequestForms_RequestFormId",
                table: "RequestFormsResult",
                column: "RequestFormId",
                principalTable: "RequestForms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_formFields_Fields_FieldId",
                table: "formFields");

            migrationBuilder.DropForeignKey(
                name: "FK_formFields_Forms_FormId",
                table: "formFields");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestForms_Forms_FormId",
                table: "RequestForms");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestFormsResult_Fields_FieldId",
                table: "RequestFormsResult");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestFormsResult_RequestForms_RequestFormId",
                table: "RequestFormsResult");

            migrationBuilder.AddForeignKey(
                name: "FK_formFields_Fields_FieldId",
                table: "formFields",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_formFields_Forms_FormId",
                table: "formFields",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForms_Forms_FormId",
                table: "RequestForms",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFormsResult_Fields_FieldId",
                table: "RequestFormsResult",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFormsResult_RequestForms_RequestFormId",
                table: "RequestFormsResult",
                column: "RequestFormId",
                principalTable: "RequestForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
