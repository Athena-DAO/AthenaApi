﻿// <auto-generated />
using CommandAndControlWebApi.DAL;
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
    [Migration("20180419065001_AddedCompleteDataSet")]
    partial class AddedCompleteDataSet
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CommandAndControlWebApi.Models.CompleteDataSet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid?>("XComponentDataSetId");

                    b.Property<Guid?>("YComponentDataSetId");

                    b.HasKey("Id");

                    b.HasIndex("XComponentDataSetId");

                    b.HasIndex("YComponentDataSetId");

                    b.ToTable("CompleteDataSets");
                });

            modelBuilder.Entity("CommandAndControlWebApi.Models.DataSet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("URL")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("DataSets");
                });

            modelBuilder.Entity("CommandAndControlWebApi.Models.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CoverPicture")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("ProfilePicture")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("CommandAndControlWebApi.Models.ProfileCompleteDataSet", b =>
                {
                    b.Property<Guid>("CompleteDataSetId");

                    b.Property<Guid>("ProfileId");

                    b.HasKey("CompleteDataSetId", "ProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("ProfilesCompleteDataSets");
                });

            modelBuilder.Entity("CommandAndControlWebApi.Models.ProfileDataSet", b =>
                {
                    b.Property<Guid>("DataSetId");

                    b.Property<Guid>("ProfileId");

                    b.HasKey("DataSetId", "ProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("ProfilesDataSets");
                });

            modelBuilder.Entity("CommandAndControlWebApi.Models.CompleteDataSet", b =>
                {
                    b.HasOne("CommandAndControlWebApi.Models.DataSet", "XComponentDataSet")
                        .WithMany("XCompleteDataSets")
                        .HasForeignKey("XComponentDataSetId");

                    b.HasOne("CommandAndControlWebApi.Models.DataSet", "YComponentDataSet")
                        .WithMany("YCompleteDataSets")
                        .HasForeignKey("YComponentDataSetId");
                });

            modelBuilder.Entity("CommandAndControlWebApi.Models.ProfileCompleteDataSet", b =>
                {
                    b.HasOne("CommandAndControlWebApi.Models.CompleteDataSet", "CompleteDataSet")
                        .WithMany("ProfileCompleteDataSet")
                        .HasForeignKey("CompleteDataSetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CommandAndControlWebApi.Models.Profile", "Profile")
                        .WithMany("ProfileCompleteDataSet")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CommandAndControlWebApi.Models.ProfileDataSet", b =>
                {
                    b.HasOne("CommandAndControlWebApi.Models.DataSet", "DataSet")
                        .WithMany("DataSetProfiles")
                        .HasForeignKey("DataSetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CommandAndControlWebApi.Models.Profile", "Profile")
                        .WithMany("ProfileDataSets")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}