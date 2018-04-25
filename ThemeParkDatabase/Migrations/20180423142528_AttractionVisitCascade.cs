using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Migrations
{
    public partial class AttractionVisitCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttractionVisit_Attraction",
                table: "AttractionVisit");

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionVisit_Attraction",
                table: "AttractionVisit",
                column: "AttractionId",
                principalTable: "Attraction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttractionVisit_Attraction",
                table: "AttractionVisit");

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionVisit_Attraction",
                table: "AttractionVisit",
                column: "AttractionId",
                principalTable: "Attraction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
