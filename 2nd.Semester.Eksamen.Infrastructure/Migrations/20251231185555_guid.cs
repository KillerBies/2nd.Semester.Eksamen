using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2nd.Semester.Eksamen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class guid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeGuid",
                table: "TreatmentSnapshots",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "TreatmentSnapshots",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeGuid",
                table: "TreatmentSnapshots");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "TreatmentSnapshots");
        }
    }
}
