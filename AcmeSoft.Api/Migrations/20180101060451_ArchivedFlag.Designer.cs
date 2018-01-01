﻿// <auto-generated />
using AcmeSoft.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AcmeSoft.Api.Migrations
{
    [DbContext(typeof(CompanyContext))]
    [Migration("20180101060451_ArchivedFlag")]
    partial class ArchivedFlag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AcmeSoft.Shared.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Archived");

                    b.Property<DateTime>("EmployedDate")
                        .HasColumnType("date");

                    b.Property<string>("EmployeeNum")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<int>("PersonId");

                    b.Property<DateTime?>("TerminatedDate")
                        .HasColumnType("date");

                    b.HasKey("EmployeeId");

                    b.HasIndex("PersonId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("AcmeSoft.Shared.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Archived");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasMaxLength(13);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("PersonId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("AcmeSoft.Shared.Models.Employee", b =>
                {
                    b.HasOne("AcmeSoft.Shared.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
