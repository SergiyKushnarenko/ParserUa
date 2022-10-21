﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Parser1.EF;

#nullable disable

namespace Parser1.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20221019094126_addSubdirectionOfWork")]
    partial class addSubdirectionOfWork
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Parser1.Models.Direction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Directions");
                });

            modelBuilder.Entity("Parser1.Models.Scientist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Degree")
                        .HasColumnType("text");

                    b.Property<int>("DirectionId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Organization")
                        .HasColumnType("text");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DirectionId");

                    b.ToTable("Scientists");
                });

            modelBuilder.Entity("Parser1.Models.ScientistWork", b =>
                {
                    b.Property<int>("ScientistId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkOfScientistId")
                        .HasColumnType("integer");

                    b.HasKey("ScientistId", "WorkOfScientistId");

                    b.HasIndex("WorkOfScientistId");

                    b.ToTable("ScientistsWork");
                });

            modelBuilder.Entity("Parser1.Models.SubdirectionOfWork", b =>
                {
                    b.Property<int>("SubdirectionOfWorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SubdirectionOfWorkId"));

                    b.Property<int>("DirectionId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SubdirectionOfWorkId");

                    b.HasIndex("DirectionId");

                    b.ToTable("SubdirectionOfWork");
                });

            modelBuilder.Entity("Parser1.Models.WorkOfScientist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("WorkOfScientists");
                });

            modelBuilder.Entity("Parser1.Models.Scientist", b =>
                {
                    b.HasOne("Parser1.Models.Direction", "Direction")
                        .WithMany()
                        .HasForeignKey("DirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Direction");
                });

            modelBuilder.Entity("Parser1.Models.ScientistWork", b =>
                {
                    b.HasOne("Parser1.Models.Scientist", "Scientist")
                        .WithMany("ScientistsWorks")
                        .HasForeignKey("ScientistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Parser1.Models.WorkOfScientist", "WorkOfScientist")
                        .WithMany("ScientistsWorks")
                        .HasForeignKey("WorkOfScientistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scientist");

                    b.Navigation("WorkOfScientist");
                });

            modelBuilder.Entity("Parser1.Models.SubdirectionOfWork", b =>
                {
                    b.HasOne("Parser1.Models.Direction", "Direction")
                        .WithMany("Subdirections")
                        .HasForeignKey("DirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Direction");
                });

            modelBuilder.Entity("Parser1.Models.Direction", b =>
                {
                    b.Navigation("Subdirections");
                });

            modelBuilder.Entity("Parser1.Models.Scientist", b =>
                {
                    b.Navigation("ScientistsWorks");
                });

            modelBuilder.Entity("Parser1.Models.WorkOfScientist", b =>
                {
                    b.Navigation("ScientistsWorks");
                });
#pragma warning restore 612, 618
        }
    }
}
