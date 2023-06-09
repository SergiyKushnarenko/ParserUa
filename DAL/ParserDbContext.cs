﻿using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ParserDbContext : DbContext
    {
        public DbSet<Scientist> Scientists { get; set; } = null!;
        public DbSet<Organization> Organizations { get; set; } = null!;
        public DbSet<Concept> Concepts { get; set; } = null!;
        public DbSet<FieldOfResearch> FieldsOfResearch { get; set; } = null!;
        public DbSet<ScientistFieldOfResearch> ScientistsFieldsOfResearch { get; set; } = null!;
        public DbSet<Work> Works { get; set; } = null!;
        public DbSet<ScientistWork> ScientistWorks { get; set; } = null!;
        public DbSet<ScientistSocialNetwork> ScientistsSocialNetworks { get; set; } = null!;

        public ParserDbContext()
        {
        }

        public ParserDbContext(DbContextOptions<ParserDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ScientistWork>()
                .HasOne(bc => bc.Scientist)
                .WithMany(b => b.ScientistsWorks)
                .HasForeignKey(bc => bc.ScientistId);

            modelBuilder.Entity<ScientistWork>()
                .HasOne(bc => bc.Work)
                .WithMany(c => c.ScientistsWorks)
                .HasForeignKey(bc => bc.WorkId);

            modelBuilder.Entity<ScientistSocialNetwork>()
                .HasOne(s => s.Scientist)
                .WithMany(g => g.ScientistSocialNetworks)
                .HasForeignKey(s => s.ScientistId);

            modelBuilder.Entity<Scientist>()
                .HasOne(s => s.Organization)
                .WithMany(g => g.Scientists)
                .HasForeignKey(s => s.OrganizationId);

            modelBuilder.Entity<Concept>()
                .HasOne(s => s.Scientist)
                .WithMany(g => g.Concepts)
                .HasForeignKey(s => s.ScientistId);

            modelBuilder.Entity<ScientistFieldOfResearch>()
                .HasOne(s => s.Scientist)
                .WithMany(g => g.ScientistFieldsOfResearch)
                .HasForeignKey(s => s.ScientistId);

            modelBuilder.Entity<ScientistFieldOfResearch>()
                .HasOne(s => s.FieldOfResearch)
                .WithMany(g => g.ScientistsFieldsOfResearch)
                .HasForeignKey(s => s.FieldOfResearchId);
        }
    }
}


