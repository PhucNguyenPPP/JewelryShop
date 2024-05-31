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

    public virtual DbSet<ProductCounter> ProductCounters { get; set; }

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
            entity.HasKey(e => e.Bboid).HasName("PK__BuyBackO__7EBCD8367B94B056");

            entity.ToTable("BuyBackOrder");

            entity.Property(e => e.Bboid)
                .ValueGeneratedNever()
                .HasColumnName("BBOId");
            entity.Property(e => e.BbpolicyId).HasColumnName("BBPolicyId");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Bbpolicy).WithMany(p => p.BuyBackOrders)
                .HasForeignKey(d => d.BbpolicyId)
                .HasConstraintName("FK__BuyBackOr__BBPol__36B12243");

            entity.HasOne(d => d.Customer).WithMany(p => p.BuyBackOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__BuyBackOr__Custo__35BCFE0A");
        });

        modelBuilder.Entity<BuyBackOrderDetail>(entity =>
        {
            entity.HasKey(e => e.BbodetailId).HasName("PK__BuyBackO__9EF99DA8E50C0246");

            entity.ToTable("BuyBackOrderDetail");

            entity.Property(e => e.BbodetailId)
                .ValueGeneratedNever()
                .HasColumnName("BBODetailId");
            entity.Property(e => e.Bboid).HasColumnName("BBOId");
            entity.Property(e => e.Bbprice).HasColumnName("BBPrice");

            entity.HasOne(d => d.Bbo).WithMany(p => p.BuyBackOrderDetails)
                .HasForeignKey(d => d.Bboid)
                .HasConstraintName("FK__BuyBackOr__BBOId__3A81B327");

            entity.HasOne(d => d.Policy).WithMany(p => p.BuyBackOrderDetails)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__BuyBackOr__Polic__398D8EEE");

            entity.HasOne(d => d.Product).WithMany(p => p.BuyBackOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__BuyBackOr__Produ__3B75D760");
        });

        modelBuilder.Entity<BuyBackPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__BuyBackP__2E1339A4B6FD2419");

            entity.ToTable("BuyBackPolicy");

            entity.Property(e => e.PolicyId).ValueGeneratedNever();
            entity.Property(e => e.PolicyDescription).HasMaxLength(200);
            entity.Property(e => e.PolicyName).HasMaxLength(100);
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasKey(e => e.CounterId).HasName("PK__Counter__F12879C484BE1CBE");

            entity.ToTable("Counter");

            entity.Property(e => e.CounterId).ValueGeneratedNever();
            entity.Property(e => e.CounterName).HasMaxLength(100);
        });

        modelBuilder.Entity<CounterEmployee>(entity =>
        {
            entity.HasKey(e => e.CounterEmployeeId).HasName("PK__CounterE__5320D1F3E80144B7");

            entity.ToTable("CounterEmployee");

            entity.Property(e => e.CounterEmployeeId).ValueGeneratedNever();
            entity.Property(e => e.WorkingDate).HasColumnType("datetime");

            entity.HasOne(d => d.Counter).WithMany(p => p.CounterEmployees)
                .HasForeignKey(d => d.CounterId)
                .HasConstraintName("FK__CounterEm__Count__5441852A");

            entity.HasOne(d => d.Employee).WithMany(p => p.CounterEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__CounterEm__Emplo__5535A963");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8936A53DD");

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
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1104BC391C");

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
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__C50610F79BC977A4");

            entity.ToTable("Material");

            entity.Property(e => e.MaterialId).ValueGeneratedNever();
            entity.Property(e => e.MaterialName).HasMaxLength(100);
        });

        modelBuilder.Entity<MaterialProduct>(entity =>
        {
            entity.HasKey(e => e.MaterialProductId).HasName("PK__Material__219352D457D93336");

            entity.ToTable("MaterialProduct");

            entity.Property(e => e.MaterialProductId).ValueGeneratedNever();

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialProducts)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK__MaterialP__Mater__300424B4");

            entity.HasOne(d => d.Product).WithMany(p => p.MaterialProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__MaterialP__Produ__30F848ED");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CDE5984757");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.AvatarImg).IsUnicode(false);
            entity.Property(e => e.ProductName).HasMaxLength(100);
        });

        modelBuilder.Entity<ProductCounter>(entity =>
        {
            entity.HasKey(e => e.ProductCounterId).HasName("PK__ProductC__AD9704B2BB6D9252");

            entity.ToTable("ProductCounter");

            entity.Property(e => e.ProductCounterId).ValueGeneratedNever();

            entity.HasOne(d => d.Counter).WithMany(p => p.ProductCounters)
                .HasForeignKey(d => d.CounterId)
                .HasConstraintName("FK__ProductCo__Count__5165187F");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCounters)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductCo__Produ__5070F446");
        });

        modelBuilder.Entity<PromotionProgram>(entity =>
        {
            entity.HasKey(e => e.PromotionProgramId).HasName("PK__Promotio__7869220AC68CFC18");

            entity.ToTable("PromotionProgram");

            entity.Property(e => e.PromotionProgramId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.PromotionProgramName).HasMaxLength(100);
        });

        modelBuilder.Entity<PromotionProgramCode>(entity =>
        {
            entity.HasKey(e => e.PromotionCodeId).HasName("PK__Promotio__B537DD05CAC6A0BB");

            entity.ToTable("PromotionProgramCode");

            entity.Property(e => e.PromotionCodeId).ValueGeneratedNever();
            entity.Property(e => e.PromotionCodeName).HasMaxLength(100);

            entity.HasOne(d => d.PromotionProgram).WithMany(p => p.PromotionProgramCodes)
                .HasForeignKey(d => d.PromotionProgramId)
                .HasConstraintName("FK__Promotion__Promo__403A8C7D");
        });

        modelBuilder.Entity<ReturnPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__ReturnPo__2E1339A4264D997F");

            entity.ToTable("ReturnPolicy");

            entity.Property(e => e.PolicyId).ValueGeneratedNever();
            entity.Property(e => e.PolicyDescription).HasMaxLength(200);
            entity.Property(e => e.PolicyName).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A629FC063");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<SaleOrder>(entity =>
        {
            entity.HasKey(e => e.SaleOrderId).HasName("PK__SaleOrde__DB86E3428E4F9735");

            entity.ToTable("SaleOrder");

            entity.Property(e => e.SaleOrderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__SaleOrder__Custo__46E78A0C");

            entity.HasOne(d => d.Employee).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__SaleOrder__Emplo__47DBAE45");

            entity.HasOne(d => d.Policy).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__SaleOrder__Polic__44FF419A");

            entity.HasOne(d => d.PromotionCode).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.PromotionCodeId)
                .HasConstraintName("FK__SaleOrder__Promo__45F365D3");
        });

        modelBuilder.Entity<SaleOrderDetail>(entity =>
        {
            entity.HasKey(e => e.SaleOrderDetailId).HasName("PK__SaleOrde__F6EA425A59B300B3");

            entity.ToTable("SaleOrderDetail");

            entity.Property(e => e.SaleOrderDetailId).ValueGeneratedNever();
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.SaleOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__SaleOrder__Produ__4BAC3F29");

            entity.HasOne(d => d.SaleOrder).WithMany(p => p.SaleOrderDetails)
                .HasForeignKey(d => d.SaleOrderId)
                .HasConstraintName("FK__SaleOrder__SaleO__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
