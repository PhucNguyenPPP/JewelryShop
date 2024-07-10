﻿using System;
using System.Collections.Generic;
using BOL;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public partial class JewelryShopDbContext : DbContext
{
    public JewelryShopDbContext()
    {
    }

    public JewelryShopDbContext(DbContextOptions<JewelryShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BuyBackOrder> BuyBackOrders { get; set; }

    public virtual DbSet<BuyBackOrderDetail> BuyBackOrderDetails { get; set; }

    public virtual DbSet<BuyBackPolicy> BuyBackPolicies { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialProduct> MaterialProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PromotionProgram> PromotionPrograms { get; set; }

    public virtual DbSet<PromotionProgramCode> PromotionProgramCodes { get; set; }

    public virtual DbSet<ReturnOrder> ReturnOrders { get; set; }

    public virtual DbSet<ReturnOrderDetail> ReturnOrderDetails { get; set; }

    public virtual DbSet<ReturnPolicy> ReturnPolicies { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SaleOrder> SaleOrders { get; set; }

    public virtual DbSet<SaleOrderDetail> SaleOrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;uid=SA;pwd=12345;database=JewelryShopDB;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BuyBackOrder>(entity =>
        {
            entity.HasKey(e => e.Bboid).HasName("PK__BuyBackO__7EBCD83693666EF8");

            entity.ToTable("BuyBackOrder");

            entity.Property(e => e.Bboid)
                .ValueGeneratedNever()
                .HasColumnName("BBOId");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.BuyBackOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__BuyBackOr__Custo__48CFD27E");

            entity.HasOne(d => d.Employee).WithMany(p => p.BuyBackOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__BuyBackOr__Emplo__49C3F6B7");

            entity.HasOne(d => d.SaleOrder).WithMany(p => p.BuyBackOrders)
                .HasForeignKey(d => d.SaleOrderId)
                .HasConstraintName("FK__BuyBackOr__SaleO__4AB81AF0");
        });

        modelBuilder.Entity<BuyBackOrderDetail>(entity =>
        {
            entity.HasKey(e => e.BbodetailId).HasName("PK__BuyBackO__9EF99DA81054651E");

            entity.ToTable("BuyBackOrderDetail");

            entity.Property(e => e.BbodetailId)
                .ValueGeneratedNever()
                .HasColumnName("BBODetailId");
            entity.Property(e => e.Bboid).HasColumnName("BBOId");
            entity.Property(e => e.Bbprice)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("BBPrice");

            entity.HasOne(d => d.Bbo).WithMany(p => p.BuyBackOrderDetails)
                .HasForeignKey(d => d.Bboid)
                .HasConstraintName("FK__BuyBackOr__BBOId__4E88ABD4");

            entity.HasOne(d => d.Policy).WithMany(p => p.BuyBackOrderDetails)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__BuyBackOr__Polic__4D94879B");

            entity.HasOne(d => d.Product).WithMany(p => p.BuyBackOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__BuyBackOr__Produ__4F7CD00D");
        });

        modelBuilder.Entity<BuyBackPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__BuyBackP__2E1339A440DBF47C");

            entity.ToTable("BuyBackPolicy");

            entity.Property(e => e.PolicyId).ValueGeneratedNever();
            entity.Property(e => e.PolicyDescription).HasMaxLength(200);
            entity.Property(e => e.PolicyName).HasMaxLength(100);
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasKey(e => e.CounterId).HasName("PK__Counter__F12879C4DC13666C");

            entity.ToTable("Counter");

            entity.Property(e => e.CounterId).ValueGeneratedNever();
            entity.Property(e => e.CounterName).HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D87637DED9");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.AvatarImg).IsUnicode(false);
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.Customers)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Customer__Employ__2C3393D0");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F117A96C61C");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.AvatarImg).IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EmployeeName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Counter).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CounterId)
                .HasConstraintName("FK__Employee__Counte__286302EC");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Employee__RoleId__29572725");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__C50610F789B12DFB");

            entity.ToTable("Material");

            entity.Property(e => e.MaterialId).ValueGeneratedNever();
            entity.Property(e => e.MaterialName).HasMaxLength(100);
        });

        modelBuilder.Entity<MaterialProduct>(entity =>
        {
            entity.HasKey(e => e.MaterialProductId).HasName("PK__Material__219352D461D91DB9");

            entity.ToTable("MaterialProduct");

            entity.Property(e => e.MaterialProductId).ValueGeneratedNever();
            entity.Property(e => e.MaterialSize).HasColumnType("decimal(10, 3)");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialProducts)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK__MaterialP__Mater__33D4B598");

            entity.HasOne(d => d.Product).WithMany(p => p.MaterialProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__MaterialP__Produ__34C8D9D1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD6E0C7AE7");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.AvatarImg).IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName).HasMaxLength(100);

            entity.HasOne(d => d.Counter).WithMany(p => p.Products)
                .HasForeignKey(d => d.CounterId)
                .HasConstraintName("FK__Product__Counter__30F848ED");
        });

        modelBuilder.Entity<PromotionProgram>(entity =>
        {
            entity.HasKey(e => e.PromotionProgramId).HasName("PK__Promotio__7869220AFBCA2897");

            entity.ToTable("PromotionProgram");

            entity.Property(e => e.PromotionProgramId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.PromotionProgramName).HasMaxLength(100);
        });

        modelBuilder.Entity<PromotionProgramCode>(entity =>
        {
            entity.HasKey(e => e.PromotionCodeId).HasName("PK__Promotio__B537DD0548C9DE01");

            entity.ToTable("PromotionProgramCode");

            entity.Property(e => e.PromotionCodeId).ValueGeneratedNever();
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PromotionCodeName).HasMaxLength(100);

            entity.HasOne(d => d.PromotionProgram).WithMany(p => p.PromotionProgramCodes)
                .HasForeignKey(d => d.PromotionProgramId)
                .HasConstraintName("FK__Promotion__Promo__3B75D760");
        });

        modelBuilder.Entity<ReturnOrder>(entity =>
        {
            entity.HasKey(e => e.ReturnOrderId).HasName("PK__ReturnOr__4DBF55434515FC77");

            entity.ToTable("ReturnOrder");

            entity.Property(e => e.ReturnOrderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.ReturnOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__ReturnOrd__Custo__52593CB8");

            entity.HasOne(d => d.Employee).WithMany(p => p.ReturnOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ReturnOrd__Emplo__534D60F1");

            entity.HasOne(d => d.SaleOrder).WithMany(p => p.ReturnOrders)
                .HasForeignKey(d => d.SaleOrderId)
                .HasConstraintName("FK__ReturnOrd__SaleO__5441852A");
        });

        modelBuilder.Entity<ReturnOrderDetail>(entity =>
        {
            entity.HasKey(e => e.ReturnOrderDetailId).HasName("PK__ReturnOr__9927484B3A7F6AAE");

            entity.ToTable("ReturnOrderDetail");

            entity.Property(e => e.ReturnOrderDetailId).ValueGeneratedNever();
            entity.Property(e => e.ReturnPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Product).WithMany(p => p.ReturnOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ReturnOrd__Produ__5812160E");

            entity.HasOne(d => d.ReturnOrder).WithMany(p => p.ReturnOrderDetails)
                .HasForeignKey(d => d.ReturnOrderId)
                .HasConstraintName("FK__ReturnOrd__Retur__571DF1D5");
        });

        modelBuilder.Entity<ReturnPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__ReturnPo__2E1339A4903303A8");

            entity.ToTable("ReturnPolicy");

            entity.Property(e => e.PolicyId).ValueGeneratedNever();
            entity.Property(e => e.PolicyDescription).HasMaxLength(200);
            entity.Property(e => e.PolicyName).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A967C105B");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<SaleOrder>(entity =>
        {
            entity.HasKey(e => e.SaleOrderId).HasName("PK__SaleOrde__DB86E342041B5AC8");

            entity.ToTable("SaleOrder");

            entity.Property(e => e.SaleOrderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PaymentMethod).HasMaxLength(20);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TransactionCode).HasMaxLength(200);

            entity.HasOne(d => d.Customer).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__SaleOrder__Custo__412EB0B6");

            entity.HasOne(d => d.Employee).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__SaleOrder__Emplo__4222D4EF");

            entity.HasOne(d => d.PromotionCode).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.PromotionCodeId)
                .HasConstraintName("FK__SaleOrder__Promo__403A8C7D");
        });

        modelBuilder.Entity<SaleOrderDetail>(entity =>
        {
            entity.HasKey(e => e.SaleOrderDetailId).HasName("PK__SaleOrde__F6EA425AB80A1FC5");

            entity.ToTable("SaleOrderDetail");

            entity.Property(e => e.SaleOrderDetailId).ValueGeneratedNever();
            entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Product).WithMany(p => p.SaleOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__SaleOrder__Produ__45F365D3");

            entity.HasOne(d => d.SaleOrder).WithMany(p => p.SaleOrderDetails)
                .HasForeignKey(d => d.SaleOrderId)
                .HasConstraintName("FK__SaleOrder__SaleO__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
