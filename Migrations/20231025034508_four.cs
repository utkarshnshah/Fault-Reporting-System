using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaultReportingSystem.Migrations
{
    /// <inheritdoc />
    public partial class four : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Developers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Developers_ManagerId",
                table: "Developers",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_Managers_ManagerId",
                table: "Developers",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "ManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developers_Managers_ManagerId",
                table: "Developers");

            migrationBuilder.DropIndex(
                name: "IX_Developers_ManagerId",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Developers");
        }
    }
}
