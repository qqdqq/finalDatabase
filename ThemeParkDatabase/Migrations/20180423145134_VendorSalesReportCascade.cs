using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Migrations
{
    public partial class VendorSalesReportCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorSalesReport_Vendor",
                table: "VendorSalesReport");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorSalesReport_Vendor",
                table: "VendorSalesReport",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorSalesReport_Vendor",
                table: "VendorSalesReport");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorSalesReport_Vendor",
                table: "VendorSalesReport",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
