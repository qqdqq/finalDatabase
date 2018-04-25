using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Migrations
{
    public partial class FixTypesAndTypos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherAudit");

            migrationBuilder.RenameColumn(
                name: "UpdatedOne",
                table: "MaintenanceAudit",
                newName: "UpdatedOn");

            migrationBuilder.AlterColumn<int>(
                name: "EstimatedCost",
                table: "MaintenanceAudit",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceAuditId",
                table: "MaintenanceAudit",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaintenanceAuditId",
                table: "MaintenanceAudit");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "MaintenanceAudit",
                newName: "UpdatedOne");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedCost",
                table: "MaintenanceAudit",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "WeatherAudit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    InchesPrecipitation = table.Column<double>(nullable: false),
                    Rainout = table.Column<bool>(nullable: false),
                    Temperature = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherAudit", x => x.Id);
                });
        }
    }
}
