﻿// <auto-generated />
using System;
using FaultReportingSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FaultReportingSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231028040612_five")]
    partial class five
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("FaultReportingSystem.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerContactNumber")
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerFirstName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerLastName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerPassword")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FaultReportingSystem.Models.Developer", b =>
                {
                    b.Property<int>("DeveloperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeveloperContactNumber")
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.Property<string>("DeveloperEmail")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("DeveloperFirstName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("DeveloperLastName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("DeveloperPassword")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DeveloperId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Developers");
                });

            modelBuilder.Entity("FaultReportingSystem.Models.Fault", b =>
                {
                    b.Property<int>("FaultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ClosedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateReported")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeveloperId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FaultType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int?>("HelpDeskId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Problem")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("ReportMethod")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.Property<int?>("SoftwareProductId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("FaultId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("HelpDeskId");

                    b.HasIndex("SoftwareProductId");

                    b.ToTable("Faults");
                });

            modelBuilder.Entity("FaultReportingSystem.Models.FaultLog", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeveloperId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FaultId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LogId");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("FaultId");

                    b.ToTable("FaultLogs");
                });

            modelBuilder.Entity("FaultReportingSystem.Models.HelpDesk", b =>
                {
                    b.Property<int>("HelpDeskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HelpDeskContactNumber")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("HelpDeskEmail")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("HelpDeskFirstName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("HelpDeskLastName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("HelpDeskPassword")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.HasKey("HelpDeskId");

                    b.ToTable("HelpDesks");
                });

            modelBuilder.Entity("FaultReportingSystem.Models.Manager", b =>
                {
                    b.Property<int>("ManagerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ManagerContactNumber")
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.Property<string>("ManagerEmail")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("ManagerFirstName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("ManagerLastName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("ManagerPassword")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.HasKey("ManagerId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("FaultReportingSystem.Models.SoftwareProduct", b =>
                {
                    b.Property<int>("SoftwareProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("SoftwareProductId");

                    b.ToTable("SoftwareProducts");
                });

            modelBuilder.Entity("FaultReportingSystem.Models.SystemAdministrator", b =>
                {
                    b.Property<int>("SystemAdministratorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContactNumber")
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.HasKey("SystemAdministratorId");

                    b.ToTable("SystemAdministrators");
                });

            modelBuilder.Entity("FaultReportingSystem.Models.Developer", b =>
                {
                    b.HasOne("FaultReportingSystem.Models.Manager", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("FaultReportingSystem.Models.Fault", b =>
                {
                    b.HasOne("FaultReportingSystem.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("FaultReportingSystem.Models.Developer", "Developer")
                        .WithMany()
                        .HasForeignKey("DeveloperId");

                    b.HasOne("FaultReportingSystem.Models.HelpDesk", "HelpDesk")
                        .WithMany()
                        .HasForeignKey("HelpDeskId");

                    b.HasOne("FaultReportingSystem.Models.SoftwareProduct", "SoftwareProduct")
                        .WithMany()
                        .HasForeignKey("SoftwareProductId");

                    b.Navigation("Customer");

                    b.Navigation("Developer");

                    b.Navigation("HelpDesk");

                    b.Navigation("SoftwareProduct");
                });

            modelBuilder.Entity("FaultReportingSystem.Models.FaultLog", b =>
                {
                    b.HasOne("FaultReportingSystem.Models.Developer", "Developer")
                        .WithMany()
                        .HasForeignKey("DeveloperId");

                    b.HasOne("FaultReportingSystem.Models.Fault", "Fault")
                        .WithMany("FaultLogs")
                        .HasForeignKey("FaultId");

                    b.Navigation("Developer");

                    b.Navigation("Fault");
                });

            modelBuilder.Entity("FaultReportingSystem.Models.Fault", b =>
                {
                    b.Navigation("FaultLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
