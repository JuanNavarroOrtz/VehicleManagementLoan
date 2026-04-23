using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManagementLoan.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class crearCamposParaIdentificarLoanMaintenance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Consecutive",
                table: "MaintenanceRecords",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Consecutive",
                table: "Loans",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consecutive",
                table: "MaintenanceRecords");

            migrationBuilder.DropColumn(
                name: "Consecutive",
                table: "Loans");
        }
    }
}
