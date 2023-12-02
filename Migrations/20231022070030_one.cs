using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaultReportingSystem.Migrations
{
    /// <inheritdoc />
    public partial class one : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerFirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerLastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerEmail = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    CustomerPassword = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CustomerContactNumber = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    DeveloperId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeveloperFirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DeveloperLastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DeveloperEmail = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    DeveloperPassword = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    DeveloperContactNumber = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.DeveloperId);
                });

            migrationBuilder.CreateTable(
                name: "HelpDesks",
                columns: table => new
                {
                    HelpDeskId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HelpDeskFirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HelpDeskLastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HelpDeskEmail = table.Column<string>(type: "TEXT", nullable: false),
                    HelpDeskPassword = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    HelpDeskContactNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpDesks", x => x.HelpDeskId);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    ManagerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ManagerFirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ManagerLastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ManagerEmail = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    ManagerPassword = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    ManagerContactNumber = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ManagerId);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareProducts",
                columns: table => new
                {
                    SoftwareProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ProductDescription = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareProducts", x => x.SoftwareProductId);
                });

            migrationBuilder.CreateTable(
                name: "SystemAdministrators",
                columns: table => new
                {
                    SystemAdministratorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemAdministrators", x => x.SystemAdministratorId);
                });

            migrationBuilder.CreateTable(
                name: "Faults",
                columns: table => new
                {
                    FaultId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateReported = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Priority = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Problem = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ClosedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FaultType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ReportMethod = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: true),
                    DeveloperId = table.Column<int>(type: "INTEGER", nullable: true),
                    HelpDeskId = table.Column<int>(type: "INTEGER", nullable: true),
                    SoftwareProductId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faults", x => x.FaultId);
                    table.ForeignKey(
                        name: "FK_Faults_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Faults_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "DeveloperId");
                    table.ForeignKey(
                        name: "FK_Faults_HelpDesks_HelpDeskId",
                        column: x => x.HelpDeskId,
                        principalTable: "HelpDesks",
                        principalColumn: "HelpDeskId");
                    table.ForeignKey(
                        name: "FK_Faults_SoftwareProducts_SoftwareProductId",
                        column: x => x.SoftwareProductId,
                        principalTable: "SoftwareProducts",
                        principalColumn: "SoftwareProductId");
                });

            migrationBuilder.CreateTable(
                name: "FaultLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Action = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    DeveloperId = table.Column<int>(type: "INTEGER", nullable: true),
                    FaultId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaultLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_FaultLogs_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "DeveloperId");
                    table.ForeignKey(
                        name: "FK_FaultLogs_Faults_FaultId",
                        column: x => x.FaultId,
                        principalTable: "Faults",
                        principalColumn: "FaultId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaultLogs_DeveloperId",
                table: "FaultLogs",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_FaultLogs_FaultId",
                table: "FaultLogs",
                column: "FaultId");

            migrationBuilder.CreateIndex(
                name: "IX_Faults_CustomerId",
                table: "Faults",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Faults_DeveloperId",
                table: "Faults",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Faults_HelpDeskId",
                table: "Faults",
                column: "HelpDeskId");

            migrationBuilder.CreateIndex(
                name: "IX_Faults_SoftwareProductId",
                table: "Faults",
                column: "SoftwareProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaultLogs");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "SystemAdministrators");

            migrationBuilder.DropTable(
                name: "Faults");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Developers");

            migrationBuilder.DropTable(
                name: "HelpDesks");

            migrationBuilder.DropTable(
                name: "SoftwareProducts");
        }
    }
}
