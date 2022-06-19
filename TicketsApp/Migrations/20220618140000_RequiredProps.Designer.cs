﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TicketsApp.Data;

namespace TicketsApp.Migrations
{
    [DbContext(typeof(TicketsDbContext))]
    [Migration("20220618140000_RequiredProps")]
    partial class RequiredProps
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("TicketsApp.Data.Models.Segment", b =>
                {
                    b.Property<int>("SegmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("AirlineCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("ArriveDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ArrivePlace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTimeOffset>("DepartDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DepartPlace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("DocNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("DocType")
                        .HasColumnType("integer");

                    b.Property<int>("FlightNum")
                        .HasColumnType("integer");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OperationPlace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("OperationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OperationType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PassengerType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PnrId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TicketNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("TicketType")
                        .HasColumnType("integer");

                    b.HasKey("SegmentId");

                    b.HasIndex("TicketNumber", "SerialNumber")
                        .IsUnique();

                    b.ToTable("Segments");
                });
#pragma warning restore 612, 618
        }
    }
}