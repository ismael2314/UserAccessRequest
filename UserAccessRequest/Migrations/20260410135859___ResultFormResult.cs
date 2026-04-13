using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAccessRequest.Migrations
{
    /// <inheritdoc />
    public partial class __ResultFormResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestForms_Fields_FieldId",
                table: "RequestForms");

            migrationBuilder.DropIndex(
                name: "IX_RequestForms_FieldId",
                table: "RequestForms");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "RequestFormsResult");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "RequestFormsResult");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RequestFormsResult");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "RequestFormsResult");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "RequestFormsResult");

            migrationBuilder.DropColumn(
                name: "RejectedAt",
                table: "RequestFormsResult");

            migrationBuilder.DropColumn(
                name: "RejectedBy",
                table: "RequestFormsResult");

            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "RequestFormsResult");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "RequestFormsResult",
                newName: "FieldId");

            migrationBuilder.RenameColumn(
                name: "FieldId",
                table: "RequestForms",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "Iacrive",
                table: "formFields",
                newName: "Inactive");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "RequestForms",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "RequestForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RequestForms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FieldsId",
                table: "RequestForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "RequestForms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "RequestForms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedAt",
                table: "RequestForms",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectedBy",
                table: "RequestForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestStatus",
                table: "RequestForms",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestFormsResult_FieldId",
                table: "RequestFormsResult",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForms_FieldsId",
                table: "RequestForms",
                column: "FieldsId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForms_Fields_FieldsId",
                table: "RequestForms",
                column: "FieldsId",
                principalTable: "Fields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFormsResult_Fields_FieldId",
                table: "RequestFormsResult",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestForms_Fields_FieldsId",
                table: "RequestForms");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestFormsResult_Fields_FieldId",
                table: "RequestFormsResult");

            migrationBuilder.DropIndex(
                name: "IX_RequestFormsResult_FieldId",
                table: "RequestFormsResult");

            migrationBuilder.DropIndex(
                name: "IX_RequestForms_FieldsId",
                table: "RequestForms");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "RequestForms");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "RequestForms");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RequestForms");

            migrationBuilder.DropColumn(
                name: "FieldsId",
                table: "RequestForms");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "RequestForms");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "RequestForms");

            migrationBuilder.DropColumn(
                name: "RejectedAt",
                table: "RequestForms");

            migrationBuilder.DropColumn(
                name: "RejectedBy",
                table: "RequestForms");

            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "RequestForms");

            migrationBuilder.RenameColumn(
                name: "FieldId",
                table: "RequestFormsResult",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "RequestForms",
                newName: "FieldId");

            migrationBuilder.RenameColumn(
                name: "Inactive",
                table: "formFields",
                newName: "Iacrive");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "RequestFormsResult",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "RequestFormsResult",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RequestFormsResult",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "RequestFormsResult",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "RequestFormsResult",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedAt",
                table: "RequestFormsResult",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectedBy",
                table: "RequestFormsResult",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestStatus",
                table: "RequestFormsResult",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestForms_FieldId",
                table: "RequestForms",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForms_Fields_FieldId",
                table: "RequestForms",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
