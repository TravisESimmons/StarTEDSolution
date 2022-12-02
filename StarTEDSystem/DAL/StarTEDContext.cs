
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StarTEDSystem.Entities;

namespace StarTEDSystem.DAL
{
    public partial class StarTEDContext : DbContext
    {
        public StarTEDContext()
        {
        }

        public StarTEDContext(DbContextOptions<StarTEDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Programs> Programs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_CI_AS");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.PositionID, "IX_PositionID");

                entity.HasIndex(e => e.ProgramID, "IX_ProgramID");

                entity.Property(e => e.DateHired).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LoginID)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Employees_dbo.Positions_PositionID");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ProgramID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Employees_dbo.Programs_ProgramID");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ReportsToNavigation)
                    .WithMany(p => p.InverseReportsToNavigation)
                    .HasForeignKey(d => d.ReportsTo)
                    .HasConstraintName("FK_dbp.Positions_ReportsTo");
            });

            modelBuilder.Entity<Programs>(entity =>
            {
                entity.HasIndex(e => e.SchoolCode, "IX_SchoolCode");

                entity.Property(e => e.DiplomaName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InternationalTuition).HasColumnType("money");

                entity.Property(e => e.ProgramName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SchoolCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tuition).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}