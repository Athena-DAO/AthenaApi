﻿// <auto-generated />
using CommandAndControlWebApi.DAL;
using CommandAndControlWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CommandAndControlWebApi.Migrations
{
    [DbContext(typeof(DataCenterContext))]
    [Migration("20180119152207_AddedInputs")]
    partial class AddedInputs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CommandAndControlWebApi.Models.Algorithm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MasterExecutableFileLocation");

                    b.Property<string>("Name");

                    b.Property<string>("SlaveExecutableFileLocation");

                    b.HasKey("Id");

                    b.ToTable("Algorithms");
                });

            modelBuilder.Entity("CommandAndControlWebApi.Models.Conatiner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IpAddress");

                    b.Property<int>("PortNumber");

                    b.HasKey("Id");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("CommandAndControlWebApi.Models.ContainerExecutesAlgorithm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AlgorithmId");

                    b.Property<Guid?>("ConatinerId");

                    b.Property<Guid>("ContainerId");

                    b.Property<DateTime>("EndDateTime");

                    b.Property<string>("Input");

                    b.Property<string>("Log");

                    b.Property<string>("Output");

                    b.Property<DateTime>("StartDateTime");

                    b.Property<bool?>("SuccessfullyComplted");

                    b.HasKey("Id");

                    b.HasIndex("AlgorithmId");

                    b.HasIndex("ConatinerId");

                    b.ToTable("ContainerExecutesAlgorithms");
                });

            modelBuilder.Entity("CommandAndControlWebApi.Models.Input", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AlgorithmId");

                    b.Property<int>("DataType");

                    b.Property<string>("Name");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AlgorithmId");

                    b.ToTable("Inputs");
                });

            modelBuilder.Entity("CommandAndControlWebApi.Models.ContainerExecutesAlgorithm", b =>
                {
                    b.HasOne("CommandAndControlWebApi.Models.Algorithm", "Algorithm")
                        .WithMany("Executions")
                        .HasForeignKey("AlgorithmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CommandAndControlWebApi.Models.Conatiner", "Conatiner")
                        .WithMany("Executions")
                        .HasForeignKey("ConatinerId");
                });

            modelBuilder.Entity("CommandAndControlWebApi.Models.Input", b =>
                {
                    b.HasOne("CommandAndControlWebApi.Models.Algorithm", "Algorithm")
                        .WithMany("Inputs")
                        .HasForeignKey("AlgorithmId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}