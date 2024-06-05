using System;
using System.Collections.Generic;
using BOL.Entities;
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

    public virtual DbSet<CounterEmployee> CounterEmployees { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialProduct> MaterialProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PromotionProgram> PromotionPrograms { get; set; }

    public virtual DbSet<PromotionProgramCode> PromotionProgramCodes { get; set; }

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
            entity.HasKey(e => e.Bboid).HasName("PK__BuyBackO__7EBCD8367754453C");

            entity.ToTable("BuyBackOrder");

            entity.Property(e => e.Bboid)
                .ValueGeneratedNever()
                .HasColumnName("BBOId");
            entity.Property(e => e.BbpolicyId).HasColumnName("BBPolicyId");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Bbpolicy).WithMany(p => p.BuyBackOrders)
                .HasForeignKey(d => d.BbpolicyId)
                .HasConstraintName("FK__BuyBackOr__BBPol__398D8EEE");

            entity.HasOne(d => d.Customer).WithMany(p => p.BuyBackOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__BuyBackOr__Custo__38996AB5");
        });

        modelBuilder.Entity<BuyBackOrderDetail>(entity =>
        {
            entity.HasKey(e => e.BbodetailId).HasName("PK__BuyBackO__9EF99DA8C59B0080");

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
                .HasConstraintName("FK__BuyBackOr__BBOId__3D5E1FD2");

            entity.HasOne(d => d.Policy).WithMany(p => p.BuyBackOrderDetails)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__BuyBackOr__Polic__3C69FB99");

            entity.HasOne(d => d.Product).WithMany(p => p.BuyBackOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__BuyBackOr__Produ__3E52440B");
        });

        modelBuilder.Entity<BuyBackPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__BuyBackP__2E1339A442914DF9");

            entity.ToTable("BuyBackPolicy");

            entity.Property(e => e.PolicyId).ValueGeneratedNever();
            entity.Property(e => e.PolicyDescription).HasMaxLength(200);
            entity.Property(e => e.PolicyName).HasMaxLength(100);
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasKey(e => e.CounterId).HasName("PK__Counter__F12879C4B1052B0C");

            entity.ToTable("Counter");

            entity.Property(e => e.CounterId).ValueGeneratedNever();
            entity.Property(e => e.CounterName).HasMaxLength(100);
        });

        modelBuilder.Entity<CounterEmployee>(entity =>
        {
            entity.HasKey(e => e.CounterEmployeeId).HasName("PK__CounterE__5320D1F305E1FE8D");

            entity.ToTable("CounterEmployee");

            entity.Property(e => e.CounterEmployeeId).ValueGeneratedNever();
            entity.Property(e => e.WorkingDate).HasColumnType("datetime");

            entity.HasOne(d => d.Counter).WithMany(p => p.CounterEmployees)
                .HasForeignKey(d => d.CounterId)
                .HasConstraintName("FK__CounterEm__Count__5165187F");

            entity.HasOne(d => d.Employee).WithMany(p => p.CounterEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__CounterEm__Emplo__52593CB8");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D823F1CE55");

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
                .HasConstraintName("FK__Customer__Employ__29572725");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1184A1626A");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.AvatarImg).IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EmployeeName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Employee__RoleId__267ABA7A");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__C50610F7F533852E");

            entity.ToTable("Material");

            entity.Property(e => e.MaterialId).ValueGeneratedNever();
            entity.Property(e => e.MaterialName).HasMaxLength(100);
        });

        modelBuilder.Entity<MaterialProduct>(entity =>
        {
            entity.HasKey(e => e.MaterialProductId).HasName("PK__Material__219352D4BA5A954F");

            entity.ToTable("MaterialProduct");

            entity.Property(e => e.MaterialProductId).ValueGeneratedNever();
            entity.Property(e => e.MaterialSize).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialProducts)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK__MaterialP__Mater__32E0915F");

            entity.HasOne(d => d.Product).WithMany(p => p.MaterialProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__MaterialP__Produ__33D4B598");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD3A524916");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.AvatarImg).IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName).HasMaxLength(100);

            entity.HasOne(d => d.Counter).WithMany(p => p.Products)
                .HasForeignKey(d => d.CounterId)
                .HasConstraintName("FK__Product__Counter__300424B4");
        });

        modelBuilder.Entity<PromotionProgram>(entity =>
        {
            entity.HasKey(e => e.PromotionProgramId).HasName("PK__Promotio__7869220A323C7924");

            entity.ToTable("PromotionProgram");

            entity.Property(e => e.PromotionProgramId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.PromotionProgramName).HasMaxLength(100);
        });

        modelBuilder.Entity<PromotionProgramCode>(entity =>
        {
            entity.HasKey(e => e.PromotionCodeId).HasName("PK__Promotio__B537DD05D2478EFD");

            entity.ToTable("PromotionProgramCode");

            entity.Property(e => e.PromotionCodeId).ValueGeneratedNever();
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PromotionCodeName).HasMaxLength(100);

            entity.HasOne(d => d.PromotionProgram).WithMany(p => p.PromotionProgramCodes)
                .HasForeignKey(d => d.PromotionProgramId)
                .HasConstraintName("FK__Promotion__Promo__4316F928");
        });

        modelBuilder.Entity<ReturnPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__ReturnPo__2E1339A4946F2928");

            entity.ToTable("ReturnPolicy");

            entity.Property(e => e.PolicyId).ValueGeneratedNever();
            entity.Property(e => e.PolicyDescription).HasMaxLength(200);
            entity.Property(e => e.PolicyName).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A8B5294A3");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<SaleOrder>(entity =>
        {
            entity.HasKey(e => e.SaleOrderId).HasName("PK__SaleOrde__DB86E3420D26FCC3");

            entity.ToTable("SaleOrder");

            entity.Property(e => e.SaleOrderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__SaleOrder__Custo__49C3F6B7");

            entity.HasOne(d => d.Employee).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__SaleOrder__Emplo__4AB81AF0");

            entity.HasOne(d => d.Policy).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__SaleOrder__Polic__47DBAE45");

            entity.HasOne(d => d.PromotionCode).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.PromotionCodeId)
                .HasConstraintName("FK__SaleOrder__Promo__48CFD27E");
        });

        modelBuilder.Entity<SaleOrderDetail>(entity =>
        {
            entity.HasKey(e => e.SaleOrderDetailId).HasName("PK__SaleOrde__F6EA425A9B1ED182");

            entity.ToTable("SaleOrderDetail");

            entity.Property(e => e.SaleOrderDetailId).ValueGeneratedNever();
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");
            entity.Property(e => e.ReturnPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Product).WithMany(p => p.SaleOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__SaleOrder__Produ__4E88ABD4");

            entity.HasOne(d => d.SaleOrder).WithMany(p => p.SaleOrderDetails)
                .HasForeignKey(d => d.SaleOrderId)
                .HasConstraintName("FK__SaleOrder__SaleO__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
