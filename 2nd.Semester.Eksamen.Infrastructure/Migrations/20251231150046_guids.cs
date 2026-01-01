using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2nd.Semester.Eksamen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class guids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "ProductSnapshots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "OrderSnapshots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "OrderLinesSnapshots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "CustomerSnapshots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "BookingsSnapshots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "AppliedDiscountSnapshots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "AddressSnapshots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "ProductSnapshots");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "OrderSnapshots");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "OrderLinesSnapshots");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "CustomerSnapshots");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "BookingsSnapshots");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "AppliedDiscountSnapshots");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "AddressSnapshots");
        }
    }
}
