using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eForm.Migrations
{
    public partial class ChangeTempFileToFullAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "TempUpload",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "TempUpload",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "TempUpload",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "TempUpload",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TempUpload",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "TempUpload",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "TempUpload",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "TempUpload");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "TempUpload");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "TempUpload");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "TempUpload");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TempUpload");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "TempUpload");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "TempUpload");
        }
    }
}
