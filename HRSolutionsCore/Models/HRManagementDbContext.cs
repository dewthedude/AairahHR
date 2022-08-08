using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRSolutionsCore.Models
{
    public partial class HRManagementDbContext : DbContext
    {
        public HRManagementDbContext()
        {
        }

        public HRManagementDbContext(DbContextOptions<HRManagementDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MstCategory> MstCategories { get; set; } = null!;
        public virtual DbSet<MstSubCategory> MstSubCategories { get; set; } = null!;
        public virtual DbSet<MstSubSubCategory> MstSubSubCategories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SENSATIONSPC2;Database=HRManagementDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstCategory>(entity =>
            {
                entity.ToTable("mstCategory");

                entity.Property(e => e.AddBy).HasMaxLength(30);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MstSubCategory>(entity =>
            {
                entity.ToTable("mstSubCategory");

                entity.Property(e => e.AddBy).HasMaxLength(30);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.MstSubCategories)
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("FK__mstSubCat__IdCat__267ABA7A");
            });

            modelBuilder.Entity<MstSubSubCategory>(entity =>
            {
                entity.ToTable("mstSubSubCategory");

                entity.Property(e => e.AddBy).HasMaxLength(30);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdSubCategoryNavigation)
                    .WithMany(p => p.MstSubSubCategories)
                    .HasForeignKey(d => d.IdSubCategory)
                    .HasConstraintName("FK__mstSubSub__IdSub__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
