using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

public partial class LabContext : DbContext
{
    public LabContext(DbContextOptions<LabContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<DeviceType> DeviceTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B87FB7A222");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Guid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GUID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.DeviceId).HasName("PK__Device__49E123318CDA388C");

            entity.ToTable("Device");

            entity.Property(e => e.DeviceId).HasColumnName("DeviceID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.DeviceTypeTblId).HasColumnName("DeviceType_Tbl_ID");
            entity.Property(e => e.Guid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GUID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Model)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.UnlockCode).HasMaxLength(100);

            entity.HasOne(d => d.DeviceTypeTbl).WithMany(p => p.Devices)
                .HasForeignKey(d => d.DeviceTypeTblId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Device_DeviceType_Tbl_ID");
        });

        modelBuilder.Entity<DeviceType>(entity =>
        {
            entity.HasKey(e => e.DeviceTypeId).HasName("PK__DeviceTy__07A6C716FE33E3FF");

            entity.ToTable("DeviceType");

            entity.Property(e => e.DeviceTypeId).HasColumnName("DeviceTypeID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.Guid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GUID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.TypeName)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BAF6CA03B00");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerTblId).HasColumnName("Customer_Tbl_ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.DeviceTblId).HasColumnName("Device_Tbl_ID");
            entity.Property(e => e.EstimatedPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Guid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GUID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.StatusTblId).HasColumnName("Status_Tbl_ID");

            entity.HasOne(d => d.CustomerTbl).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerTblId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Customer_Tbl_ID");

            entity.HasOne(d => d.DeviceTbl).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeviceTblId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Device_Tbl_ID");

            entity.HasOne(d => d.StatusTbl).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusTblId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Status_Tbl_ID");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE20437FCE480D");

            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.Guid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GUID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.StatusName)
                .IsRequired()
                .HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
