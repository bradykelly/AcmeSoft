using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AcmeSoft.Api.Migrations
{
    public partial class NoArchive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Employee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Archived",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Archived",
                table: "Employee",
                nullable: true);
        }
    }
}
