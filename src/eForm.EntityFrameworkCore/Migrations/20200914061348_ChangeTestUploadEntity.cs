using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eForm.Migrations
{
    public partial class ChangeTestUploadEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "TestUpload");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "TestUpload");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "TestUpload");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "TestUpload");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TestUpload");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "TestUpload");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "TestUpload");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "TestUpload",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "TestUpload",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "TestUpload",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "TestUpload",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TestUpload",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "TestUpload",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "TestUpload",
                type: "bigint",
                nullable: true);
        }
    }
}
