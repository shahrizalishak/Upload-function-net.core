using Microsoft.EntityFrameworkCore.Migrations;

namespace eForm.Migrations
{
    public partial class CreateFKTempUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestId",
                table: "TempUpload");

            migrationBuilder.AddColumn<int>(
                name: "TestEntityId",
                table: "TempUpload",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TempUpload_TestEntityId",
                table: "TempUpload",
                column: "TestEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TempUpload_TestEntities_TestEntityId",
                table: "TempUpload",
                column: "TestEntityId",
                principalTable: "TestEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TempUpload_TestEntities_TestEntityId",
                table: "TempUpload");

            migrationBuilder.DropIndex(
                name: "IX_TempUpload_TestEntityId",
                table: "TempUpload");

            migrationBuilder.DropColumn(
                name: "TestEntityId",
                table: "TempUpload");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "TempUpload",
                type: "int",
                nullable: true);
        }
    }
}
