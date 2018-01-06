using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AcmeSoft.Api.Migrations
{
    public partial class Employment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Person_PersonId",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employment");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_PersonId",
                table: "Employment",
                newName: "IX_Employment_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employment",
                table: "Employment",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employment_Person_PersonId",
                table: "Employment",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employment_Person_PersonId",
                table: "Employment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employment",
                table: "Employment");

            migrationBuilder.RenameTable(
                name: "Employment",
                newName: "Employee");

            migrationBuilder.RenameIndex(
                name: "IX_Employment_PersonId",
                table: "Employee",
                newName: "IX_Employee_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Person_PersonId",
                table: "Employee",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
