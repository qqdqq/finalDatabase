using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Migrations
{
    public partial class TicketCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Visitor",
                table: "Ticket");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Visitor",
                table: "Ticket",
                column: "VisitorId",
                principalTable: "Visitor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Visitor",
                table: "Ticket");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Visitor",
                table: "Ticket",
                column: "VisitorId",
                principalTable: "Visitor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
