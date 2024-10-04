﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Entities.Models;

public partial class FinancialManagerContext : DbContext
{
    private readonly IConfiguration _configuration;

    public FinancialManagerContext(
        DbContextOptions<FinancialManagerContext> options,
        IConfiguration configuration
    )
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

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

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.HasIndex(e => new { e.Id, e.UserId }, "categories_id_user_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Title).HasMaxLength(100).HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.Categories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("categories_user_id_fkey");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("refresh_tokens_pkey");

            entity.ToTable("tokens");

            entity.HasIndex(e => e.RefreshToken, "refresh_tokens_token_key").IsUnique();

            entity
                .Property(e => e.Id)
                .HasDefaultValueSql("nextval('refresh_tokens_id_seq'::regclass)")
                .HasColumnName("id");
            entity
                .Property(e => e.ExpirationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiration_date");
            entity.Property(e => e.IsRevoked).HasDefaultValue(false).HasColumnName("is_revoked");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.Tokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("refresh_tokens_user_id_fkey");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transactions_pkey");

            entity.ToTable("transactions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity
                .Property(e => e.ExpenseDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expense_date");
            entity.Property(e => e.Significance).HasPrecision(10, 2).HasColumnName("significance");
            entity.Property(e => e.Title).HasColumnType("character varying").HasColumnName("title");
            entity
                .Property(e => e.TransactionType)
                .HasColumnType("character varying")
                .HasColumnName("transaction_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity
                .HasOne(d => d.User)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transactions_user_id_fkey");

            entity
                .HasOne(d => d.Category)
                .WithMany(p => p.Transactions)
                .HasPrincipalKey(p => new { p.Id, p.UserId })
                .HasForeignKey(d => new { d.CategoryId, d.UserId })
                .HasConstraintName("transactions_category_id_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity
                .Property(e => e.BudgetUpdateDay)
                .HasDefaultValue((short)1)
                .HasColumnName("budget_update_day");
            entity.Property(e => e.Email).HasColumnType("character varying").HasColumnName("email");
            entity
                .Property(e => e.FullName)
                .HasColumnType("character varying")
                .HasColumnName("full_name");
            entity
                .Property(e => e.HashedPassword)
                .HasColumnType("character varying")
                .HasColumnName("hashed_password");
            entity
                .Property(e => e.MonthlyBudget)
                .HasDefaultValue(0)
                .HasColumnName("monthly_budget");
            entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");
            entity
                .Property(e => e.RegistrationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registration_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}