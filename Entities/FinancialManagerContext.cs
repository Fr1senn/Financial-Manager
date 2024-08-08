using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Entities;

public partial class FinancialManagerContext : DbContext
{
    private readonly IConfiguration _configuration;

    public FinancialManagerContext(DbContextOptions<FinancialManagerContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<CategoryEntity> Categories { get; set; }

    public virtual DbSet<TransactionEntity> Transactions { get; set; }

    public virtual DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("transaction_type", new[] { "income", "expense" });

        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.HasIndex(e => new { e.Id, e.UserId }, "categories_id_user_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Categories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("categories_user_id_fkey");
        });

        modelBuilder.Entity<TransactionEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transactions_pkey");

            entity.ToTable("transactions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpenseDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expense_date");
            entity.Property(e => e.Significance)
                .HasPrecision(10, 2)
                .HasColumnName("significance");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(15)
                .HasColumnName("transaction_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transactions_user_id_fkey");

            entity.HasOne(d => d.Category).WithMany(p => p.Transactions)
                .HasPrincipalKey(p => new { p.Id, p.UserId })
                .HasForeignKey(d => new { d.CategoryId, d.UserId })
                .HasConstraintName("transactions_category_id_user_id_fkey");
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BudgetUpdateDay)
                .HasDefaultValue((short)1)
                .HasColumnName("budget_update_day");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.HashedPassword)
                .HasMaxLength(64)
                .HasColumnName("hashed_password");
            entity.Property(e => e.MonthlyBudget)
                .HasDefaultValue(0)
                .HasColumnName("monthly_budget");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registration_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
