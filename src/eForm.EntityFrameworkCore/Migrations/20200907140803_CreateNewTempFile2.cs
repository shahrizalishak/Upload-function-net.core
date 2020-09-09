using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eForm.Migrations
{
    public partial class CreateNewTempFile2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempUpload",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    Bytes = table.Column<byte[]>(nullable: false),
                    TestId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempUpload", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TempUpload_TenantId",
                table: "TempUpload",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempUpload");
        }
    }
}
