using System;
using System.Collections.Generic;
using BOL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<PriceRate> PriceRates { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PromotionProgram> PromotionPrograms { get; set; }

    public virtual DbSet<PromotionProgramCode> PromotionProgramCodes { get; set; }

    public virtual DbSet<ReturnOrder> ReturnOrders { get; set; }

    public virtual DbSet<ReturnOrderDetail> ReturnOrderDetails { get; set; }

    public virtual DbSet<ReturnPolicy> ReturnPolicies { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SaleOrder> SaleOrders { get; set; }

    public virtual DbSet<SaleOrderDetail> SaleOrderDetails { get; set; }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnectionString"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BuyBackOrder>(entity =>
        {
            entity.HasKey(e => e.Bboid).HasName("PK__BuyBackO__7EBCD836402FD3EA");

            entity.ToTable("BuyBackOrder");

            entity.Property(e => e.Bboid)
                .ValueGeneratedNever()
                .HasColumnName("BBOId");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.BuyBackOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__BuyBackOr__Custo__4D94879B");

            entity.HasOne(d => d.Employee).WithMany(p => p.BuyBackOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__BuyBackOr__Emplo__4E88ABD4");

            entity.HasOne(d => d.SaleOrder).WithMany(p => p.BuyBackOrders)
                .HasForeignKey(d => d.SaleOrderId)
                .HasConstraintName("FK__BuyBackOr__SaleO__4F7CD00D");
        });

        modelBuilder.Entity<BuyBackOrderDetail>(entity =>
        {
            entity.HasKey(e => e.BbodetailId).HasName("PK__BuyBackO__9EF99DA8576FB814");

            entity.ToTable("BuyBackOrderDetail");

            entity.Property(e => e.BbodetailId)
                .ValueGeneratedNever()
                .HasColumnName("BBODetailId");
            entity.Property(e => e.Bboid).HasColumnName("BBOId");
            entity.Property(e => e.Bbprice)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("BBPrice");
            entity.Property(e => e.Reason).HasMaxLength(200);

            entity.HasOne(d => d.Bbo).WithMany(p => p.BuyBackOrderDetails)
                .HasForeignKey(d => d.Bboid)
                .HasConstraintName("FK__BuyBackOr__BBOId__534D60F1");

            entity.HasOne(d => d.Policy).WithMany(p => p.BuyBackOrderDetails)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__BuyBackOr__Polic__52593CB8");

            entity.HasOne(d => d.Product).WithMany(p => p.BuyBackOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__BuyBackOr__Produ__5441852A");
        });

        modelBuilder.Entity<BuyBackPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__BuyBackP__2E1339A437E2E4A9");

            entity.ToTable("BuyBackPolicy");

            entity.Property(e => e.PolicyId).ValueGeneratedNever();
            entity.Property(e => e.PolicyDescription).HasMaxLength(200);
            entity.Property(e => e.PolicyName).HasMaxLength(100);
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasKey(e => e.CounterId).HasName("PK__Counter__F12879C4F4291EF9");

            entity.ToTable("Counter");

            entity.Property(e => e.CounterId).ValueGeneratedNever();
            entity.Property(e => e.CounterName).HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D88FCB0B0D");

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
                .HasConstraintName("FK__Customer__Employ__2E1BDC42");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F11BD5FCCD2");

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
                .HasConstraintName("FK__Employee__Counte__2A4B4B5E");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Employee__RoleId__2B3F6F97");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__C50610F77C533DFE");

            entity.ToTable("Material");

            entity.Property(e => e.MaterialId).ValueGeneratedNever();
            entity.Property(e => e.AmountInStock).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MaterialName).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.MaterialType).WithMany(p => p.Materials)
                .HasForeignKey(d => d.MaterialTypeId)
                .HasConstraintName("FK__Material__Materi__32E0915F");
        });

        modelBuilder.Entity<MaterialProduct>(entity =>
        {
            entity.HasKey(e => e.MaterialProductId).HasName("PK__Material__219352D4BE078A73");

            entity.ToTable("MaterialProduct");

            entity.Property(e => e.MaterialProductId).ValueGeneratedNever();
            entity.Property(e => e.MaterialSize).HasColumnType("decimal(10, 3)");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialProducts)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK__MaterialP__Mater__38996AB5");

            entity.HasOne(d => d.Product).WithMany(p => p.MaterialProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__MaterialP__Produ__398D8EEE");
        });

        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Material__516F03B54E307164");

            entity.ToTable("MaterialType");

            entity.Property(e => e.TypeId).ValueGeneratedNever();
            entity.Property(e => e.TypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<PriceRate>(entity =>
        {
            entity.HasKey(e => e.PriceRateId).HasName("PK__PriceRat__B6A2BDE4A88717A5");

            entity.ToTable("PriceRate");

            entity.Property(e => e.PriceRateId).ValueGeneratedNever();
            entity.Property(e => e.PriceRate1).HasColumnName("PriceRate");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CDBA9B2822");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.AvatarImg).IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName).HasMaxLength(100);

            entity.HasOne(d => d.Counter).WithMany(p => p.Products)
                .HasForeignKey(d => d.CounterId)
                .HasConstraintName("FK__Product__Counter__35BCFE0A");
        });

        modelBuilder.Entity<PromotionProgram>(entity =>
        {
            entity.HasKey(e => e.PromotionProgramId).HasName("PK__Promotio__7869220A7C3F12E5");

            entity.ToTable("PromotionProgram");

            entity.Property(e => e.PromotionProgramId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.PromotionProgramName).HasMaxLength(100);
        });

        modelBuilder.Entity<PromotionProgramCode>(entity =>
        {
            entity.HasKey(e => e.PromotionCodeId).HasName("PK__Promotio__B537DD0508AFBE11");

            entity.ToTable("PromotionProgramCode");

            entity.Property(e => e.PromotionCodeId).ValueGeneratedNever();
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PromotionCodeName).HasMaxLength(100);

            entity.HasOne(d => d.PromotionProgram).WithMany(p => p.PromotionProgramCodes)
                .HasForeignKey(d => d.PromotionProgramId)
                .HasConstraintName("FK__Promotion__Promo__403A8C7D");
        });

        modelBuilder.Entity<ReturnOrder>(entity =>
        {
            entity.HasKey(e => e.ReturnOrderId).HasName("PK__ReturnOr__4DBF5543526DEBE4");

            entity.ToTable("ReturnOrder");

            entity.Property(e => e.ReturnOrderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.ReturnOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__ReturnOrd__Custo__571DF1D5");

            entity.HasOne(d => d.Employee).WithMany(p => p.ReturnOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ReturnOrd__Emplo__5812160E");

            entity.HasOne(d => d.SaleOrder).WithMany(p => p.ReturnOrders)
                .HasForeignKey(d => d.SaleOrderId)
                .HasConstraintName("FK__ReturnOrd__SaleO__59063A47");
        });

        modelBuilder.Entity<ReturnOrderDetail>(entity =>
        {
            entity.HasKey(e => e.ReturnOrderDetailId).HasName("PK__ReturnOr__9927484B379441BF");

            entity.ToTable("ReturnOrderDetail");

            entity.Property(e => e.ReturnOrderDetailId).ValueGeneratedNever();
            entity.Property(e => e.Reason).HasMaxLength(200);
            entity.Property(e => e.ReturnPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Product).WithMany(p => p.ReturnOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ReturnOrd__Produ__5CD6CB2B");

            entity.HasOne(d => d.ReturnOrder).WithMany(p => p.ReturnOrderDetails)
                .HasForeignKey(d => d.ReturnOrderId)
                .HasConstraintName("FK__ReturnOrd__Retur__5BE2A6F2");
        });

        modelBuilder.Entity<ReturnPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__ReturnPo__2E1339A43FEECE57");

            entity.ToTable("ReturnPolicy");

            entity.Property(e => e.PolicyId).ValueGeneratedNever();
            entity.Property(e => e.PolicyDescription).HasMaxLength(200);
            entity.Property(e => e.PolicyName).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A6501B305");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<SaleOrder>(entity =>
        {
            entity.HasKey(e => e.SaleOrderId).HasName("PK__SaleOrde__DB86E342CF753D46");

            entity.ToTable("SaleOrder");

            entity.Property(e => e.SaleOrderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PaymentMethod).HasMaxLength(20);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TransactionCode).HasMaxLength(200);

            entity.HasOne(d => d.Customer).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__SaleOrder__Custo__45F365D3");

            entity.HasOne(d => d.Employee).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__SaleOrder__Emplo__46E78A0C");

            entity.HasOne(d => d.PromotionCode).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.PromotionCodeId)
                .HasConstraintName("FK__SaleOrder__Promo__44FF419A");
        });

        modelBuilder.Entity<SaleOrderDetail>(entity =>
        {
            entity.HasKey(e => e.SaleOrderDetailId).HasName("PK__SaleOrde__F6EA425A0B325E3A");

            entity.ToTable("SaleOrderDetail");

            entity.Property(e => e.SaleOrderDetailId).ValueGeneratedNever();
            entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Product).WithMany(p => p.SaleOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__SaleOrder__Produ__4AB81AF0");

            entity.HasOne(d => d.SaleOrder).WithMany(p => p.SaleOrderDetails)
                .HasForeignKey(d => d.SaleOrderId)
                .HasConstraintName("FK__SaleOrder__SaleO__49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
