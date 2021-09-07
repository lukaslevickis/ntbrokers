﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NTBrokers.DAL;

namespace NTBrokers.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210903134603_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NTBrokers.DAL.Entities.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Area")
                        .HasColumnType("int");

                    b.Property<int?>("BrokerId")
                        .HasColumnType("int");

                    b.Property<int>("BuildingFloors")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("FlatFloor")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("NTBrokers.DAL.Entities.Broker", b =>
                {
                    b.Property<int>("BrokerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrokerId");

                    b.ToTable("Brokers");
                });

            modelBuilder.Entity("NTBrokers.DAL.Entities.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Address")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("NTBrokers.DAL.Entities.CompanyBroker", b =>
                {
                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("BrokerId")
                        .HasColumnType("int");

                    b.HasKey("CompanyId", "BrokerId");

                    b.HasIndex("BrokerId");

                    b.ToTable("CompanyBrokers");
                });

            modelBuilder.Entity("NTBrokers.DAL.Entities.Apartment", b =>
                {
                    b.HasOne("NTBrokers.DAL.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("NTBrokers.DAL.Entities.CompanyBroker", b =>
                {
                    b.HasOne("NTBrokers.DAL.Entities.Broker", "Broker")
                        .WithMany("Companies")
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NTBrokers.DAL.Entities.Company", "Company")
                        .WithMany("Brokers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Broker");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("NTBrokers.DAL.Entities.Broker", b =>
                {
                    b.Navigation("Companies");
                });

            modelBuilder.Entity("NTBrokers.DAL.Entities.Company", b =>
                {
                    b.Navigation("Brokers");
                });
#pragma warning restore 612, 618
        }
    }
}
