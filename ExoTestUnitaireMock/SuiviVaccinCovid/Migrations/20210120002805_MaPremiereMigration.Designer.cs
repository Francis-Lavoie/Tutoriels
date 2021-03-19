﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuiviVaccinCovid;
using SuiviVaccinCovid.Modele;

namespace SuiviVaccinCovid.Migrations
{
    [DbContext(typeof(VaccinContext))]
    [Migration("20210120002805_MaPremiereMigration")]
    partial class MaPremiereMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("SuiviVaccinCovid.Vaccin", b =>
                {
                    b.Property<int>("VaccinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("NAMPatient")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VaccinId");

                    b.ToTable("Vaccins");
                });
#pragma warning restore 612, 618
        }
    }
}