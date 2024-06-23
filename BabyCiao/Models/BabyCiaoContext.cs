using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BabyCiao.Models;

public partial class BabyciaoContext : DbContext
{

   
    public BabyciaoContext(DbContextOptions<BabyciaoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FunctionSetting> FunctionSettings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=babyciao;Integrated Security=true;TrustServerCertificate=true;Encrypt=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FunctionSetting>(entity =>
        {
            entity.HasKey(e => new { e.GroupCodePermissionGroup, e.FunctionCodeSystemFunction }).HasName("PK__Function__1A698783897C5C0E");

            entity.ToTable("FunctionSetting");

            entity.Property(e => e.GroupCodePermissionGroup).HasColumnName("GroupCode_PermissionGroup");
            entity.Property(e => e.FunctionCodeSystemFunction)
                .HasMaxLength(50)
                .HasColumnName("FunctionCode_SystemFunction");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
