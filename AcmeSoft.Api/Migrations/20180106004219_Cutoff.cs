using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AcmeSoft.Api.Migrations
{
    public partial class Cutoff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeNum",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Employee");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNum",
                table: "Employee",
                maxLength: 16,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeNum",
                table: "Employee");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNum",
                table: "Person",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Employee",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
