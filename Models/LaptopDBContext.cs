using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using IdentitySecureApp.Models;
using System.Linq;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IdentitySecureApp.Models
{
    public partial class LaptopDBContext : DbContext
    {
        public LaptopDBContext()
        {
        }

        public LaptopDBContext(DbContextOptions<LaptopDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tbllaptops> Tbllaptops { get; set; }
        public virtual DbSet<Tblorders> Tblorders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=H5CG1220K2N;database=LaptopDB;Persist Security Info=True;  integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tbllaptops>(entity =>
            {
                entity.ToTable("tbllaptops");

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.SellerEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tblorders>(entity =>
            {
                entity.ToTable("tblorders");

                entity.Property(e => e.CustomerMail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.LaptopId).HasColumnName("Laptop_Id");

                entity.Property(e => e.Paid)
                    .HasColumnName("paid")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Laptop)
                    .WithMany(p => p.Tblorders)
                    .HasForeignKey(d => d.LaptopId)
                    .HasConstraintName("FK_tblorders_tbllaptops");
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);

            foreach(var foreignkey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignkey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<IdentitySecureApp.Models.ordersViewModel> ordersViewModel { get; set; }

        public DbSet<IdentitySecureApp.Models.EditUserViewModel> EditUserViewModel { get; set; }
    }
}
