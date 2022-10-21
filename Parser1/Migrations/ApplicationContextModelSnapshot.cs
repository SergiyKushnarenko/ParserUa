﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Parser1.EF;

#nullable disable

namespace Parser1.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.ToTable("Directions", (string)null);
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

                    b.ToTable("Scientists", (string)null);
                });

            modelBuilder.Entity("Parser1.Models.ScientistSubdirection", b =>
                {
                    b.Property<int>("ScientistId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubdirectionId")
                        .HasColumnType("integer");

                    b.HasKey("ScientistId", "SubdirectionId");

                    b.HasIndex("SubdirectionId");

                    b.ToTable("ScientistSubdirections", (string)null);
                });

            modelBuilder.Entity("Parser1.Models.ScientistWork", b =>
                {
                    b.Property<int>("ScientistId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkOfScientistId")
                        .HasColumnType("integer");

                    b.HasKey("ScientistId", "WorkOfScientistId");

                    b.HasIndex("WorkOfScientistId");

                    b.ToTable("ScientistsWork", (string)null);
                });

            modelBuilder.Entity("Parser1.Models.SocialNetworkOfScientist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ScientistId")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ScientistId");

                    b.ToTable("NetworkOfScientists", (string)null);
                });

            modelBuilder.Entity("Parser1.Models.Subdirection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DirectionId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DirectionId");

                    b.ToTable("Subdirection", (string)null);
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

                    b.ToTable("WorkOfScientists", (string)null);
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

            modelBuilder.Entity("Parser1.Models.ScientistSubdirection", b =>
                {
                    b.HasOne("Parser1.Models.Scientist", "Scientist")
                        .WithMany("ScientistSubdirections")
                        .HasForeignKey("ScientistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Parser1.Models.Subdirection", "Subdirection")
                        .WithMany("ScientistSubdirections")
                        .HasForeignKey("SubdirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scientist");

                    b.Navigation("Subdirection");
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

            modelBuilder.Entity("Parser1.Models.SocialNetworkOfScientist", b =>
                {
                    b.HasOne("Parser1.Models.Scientist", "Scientist")
                        .WithMany("NetworkOfScientists")
                        .HasForeignKey("ScientistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scientist");
                });

            modelBuilder.Entity("Parser1.Models.Subdirection", b =>
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
                    b.Navigation("NetworkOfScientists");

                    b.Navigation("ScientistSubdirections");

                    b.Navigation("ScientistsWorks");
                });

            modelBuilder.Entity("Parser1.Models.Subdirection", b =>
                {
                    b.Navigation("ScientistSubdirections");
                });

            modelBuilder.Entity("Parser1.Models.WorkOfScientist", b =>
                {
                    b.Navigation("ScientistsWorks");
                });
#pragma warning restore 612, 618
        }
    }
}
