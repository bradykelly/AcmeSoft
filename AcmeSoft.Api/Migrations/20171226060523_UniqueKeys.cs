using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AcmeSoft.Api.Migrations
{
    public partial class UniqueKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [UX_Person_LastName_Birthdate] UNIQUE NONCLUSTERED (
                [LastName] ASC,
                [FirstName] ASC,
                [BirthDate] ASC
                )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]");

            migrationBuilder.Sql(@"ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [UX_Employee_EmployeeNum_EmployedDate] UNIQUE NONCLUSTERED (
                [EmployeeNum] ASC,
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE [dbo].[Person] DROP CONSTRAINT [UX_Person_LastName_Birthdate]");

            migrationBuilder.Sql("ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [UX_Employee_EmployeeNum_EmployedDate]");
        }
    }
}
