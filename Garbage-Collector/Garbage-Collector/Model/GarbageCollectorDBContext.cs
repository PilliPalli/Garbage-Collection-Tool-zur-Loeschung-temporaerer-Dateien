﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Garbage_Collector.Model;

public partial class GarbageCollectorDbContext : DbContext
{
    public GarbageCollectorDbContext()
    {
    }

    public GarbageCollectorDbContext(DbContextOptions<GarbageCollectorDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=192.168.178.118\\SQLEXPRESS;Initial Catalog=GarbageCollectorDB;User Id=dbuser;Password=abc123;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CE14C9D38");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4755F0D07").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}