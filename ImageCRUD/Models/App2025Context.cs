using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ImageCRUD.Models;

public partial class App2025Context : DbContext
{
    public App2025Context()
    {
    }

    public App2025Context(DbContextOptions<App2025Context> options)
        : base(options)
    {
    }

    //public virtual DbSet<AdminUser> AdminUsers { get; set; }
    public virtual DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-ALAB59H\\SQLEXPRESS;Initial Catalog=App2025;User ID=nayalish;Password=nayalish;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.Property(e => e.Password).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
