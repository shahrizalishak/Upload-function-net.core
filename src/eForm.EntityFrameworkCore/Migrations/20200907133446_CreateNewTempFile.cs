using Microsoft.EntityFrameworkCore.Migrations;

namespace eForm.Migrations
{
    public partial class CreateNewTempFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TestUpload_TenantId",
                table: "TestUpload",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TestUpload_TenantId",
                table: "TestUpload");
        }
    }
}
