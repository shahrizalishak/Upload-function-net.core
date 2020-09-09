using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eForm.Migrations
{
    public partial class InitialEFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightInformations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(nullable: true),
                    DestinationDeparture = table.Column<string>(nullable: false),
                    DestinationArraival = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    TImeDeparture = table.Column<string>(nullable: true),
                    TimeArriaval = table.Column<string>(nullable: true),
                    FlightId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purposes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purposes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TravelAgents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelAgents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NRIC = table.Column<string>(nullable: true),
                    StaffID = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    MembershipNo = table.Column<string>(nullable: true),
                    ValidationName = table.Column<string>(nullable: true),
                    ValidationPhoneNo = table.Column<string>(nullable: true),
                    ValidationPosition = table.Column<string>(nullable: true),
                    ValidationDate = table.Column<DateTime>(nullable: false),
                    Validation = table.Column<bool>(nullable: false),
                    ApprovalName = table.Column<string>(nullable: true),
                    ApprovalPosition = table.Column<string>(nullable: true),
                    ApprovalDate = table.Column<DateTime>(nullable: false),
                    Approval = table.Column<bool>(nullable: false),
                    TravelAgentId = table.Column<int>(nullable: true),
                    PurposeId = table.Column<int>(nullable: true),
                    JobTitleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_JobTitles_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Purposes_PurposeId",
                        column: x => x.PurposeId,
                        principalTable: "Purposes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_TravelAgents_TravelAgentId",
                        column: x => x.TravelAgentId,
                        principalTable: "TravelAgents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightInformations_TenantId",
                table: "FlightInformations",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_JobTitleId",
                table: "Flights",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PurposeId",
                table: "Flights",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_TenantId",
                table: "Flights",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_TravelAgentId",
                table: "Flights",
                column: "TravelAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_TenantId",
                table: "JobTitles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Purposes_TenantId",
                table: "Purposes",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelAgents_TenantId",
                table: "TravelAgents",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightInformations");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "TestEntities");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropTable(
                name: "Purposes");

            migrationBuilder.DropTable(
                name: "TravelAgents");
        }
    }
}
