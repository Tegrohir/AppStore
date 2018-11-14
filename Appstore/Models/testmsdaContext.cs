using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppstoreAPI.Models
{
    public partial class testmsdaContext : DbContext
    {
        public testmsdaContext()
        {
        }

        public testmsdaContext(DbContextOptions<testmsdaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<App> App { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<Result> Result { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=localhost;Database=testmsda;user=root;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<App>(entity =>
            {
                entity.ToTable("app");

                entity.Property(e => e.AppId)
                    .HasColumnName("AppID")
                    .HasColumnType("int(10)");

                entity.Property(e => e.AppDescription).HasColumnType("varchar(255)");

                entity.Property(e => e.AppFile)
                    .IsRequired()
                    .HasColumnType("blob");

                entity.Property(e => e.AppName)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.AppVersion)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("device");

                entity.HasIndex(e => e.AppId)
                    .HasName("AppID");

                entity.HasIndex(e => e.UserId)
                    .HasName("UserID");

                entity.Property(e => e.DeviceId)
                    .HasColumnName("DeviceID")
                    .HasColumnType("int(6)");

                entity.Property(e => e.AppId)
                    .HasColumnName("AppID")
                    .HasColumnType("int(6)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(6)");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.Device)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Device_fk1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Device)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Device_fk0");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.ToTable("result");

                entity.HasIndex(e => e.DeviceId)
                    .HasName("Result_fk0");

                entity.Property(e => e.ResultId)
                    .HasColumnName("ResultID")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Date)
                    .HasColumnType("timestamp(6)")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP(6)'");

                entity.Property(e => e.DeviceId)
                    .HasColumnName("DeviceID")
                    .HasColumnType("int(10)");

                entity.Property(e => e.ResultJson)
                    .IsRequired()
                    .HasColumnName("ResultJSON")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.Result)
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Result_fk0");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(6)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("text");
            });
        }
    }
}
