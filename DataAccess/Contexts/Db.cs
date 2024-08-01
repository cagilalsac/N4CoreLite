using System;
using System.Collections.Generic;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using N4CoreLite.Contexts.Bases;

namespace DataAccess.Contexts;

public partial class Db : DbContext, IDb
{
    public Db(DbContextOptions<Db> options)
        : base(options)
    {
    }

    public virtual DbSet<Ders> Ders { get; set; }

    public virtual DbSet<Ogrenci> Ogrenci { get; set; }

    public virtual DbSet<OgrenciDers> OgrenciDers { get; set; }

    public virtual DbSet<OgrenciRaporuView> OgrenciRaporuView { get; set; }

    public virtual DbSet<Sinif> Sinif { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ders>(entity =>
        {
            entity.Property(e => e.Adi)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Guid).HasMaxLength(36);
        });

        modelBuilder.Entity<Ogrenci>(entity =>
        {
            entity.HasIndex(e => e.KullaniciAdi, "UC_Ogrenci").IsUnique();

            entity.Property(e => e.Adi)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.DogumTarihi).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasMaxLength(36);
            entity.Property(e => e.KullaniciAdi)
                .IsRequired()
                .HasMaxLength(256);
            entity.Property(e => e.Soyadi)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.TcKimlikNo).HasMaxLength(11);

            entity.HasOne(d => d.Sinif).WithMany(p => p.Ogrenci)
                .HasForeignKey(d => d.SinifId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ogrenci_Sinif");
        });

        modelBuilder.Entity<OgrenciDers>(entity =>
        {
            entity.HasIndex(e => new { e.OgrenciId, e.DersId }, "UC_OgrenciDers").IsUnique();

            entity.Property(e => e.Guid).HasMaxLength(36);
            entity.Property(e => e.Not1).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.Not2).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.Not3).HasColumnType("decimal(5, 1)");

            entity.HasOne(d => d.Ders).WithMany(p => p.OgrenciDers)
                .HasForeignKey(d => d.DersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OgrenciDers_Ders");

            entity.HasOne(d => d.Ogrenci).WithMany(p => p.OgrenciDers)
                .HasForeignKey(d => d.OgrenciId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OgrenciDers_Ogrenci");
        });

        modelBuilder.Entity<OgrenciRaporuView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OgrenciRaporuView");

            entity.Property(e => e.Ders).HasMaxLength(111);
            entity.Property(e => e.DersNotu1).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.DersNotu2).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.DersNotu3).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.DersOrtalamasi).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.Guid).HasMaxLength(36);
            entity.Property(e => e.OgrenciAdiSoyadi).HasMaxLength(101);
            entity.Property(e => e.OgrenciDogumTarihi).HasColumnType("datetime");
            entity.Property(e => e.OgrenciGuid).HasMaxLength(36);
            entity.Property(e => e.OgrenciKullaniciAdi).HasMaxLength(256);
            entity.Property(e => e.OgrenciTcKimlikNo).HasMaxLength(11);
            entity.Property(e => e.Sinif)
                .IsRequired()
                .HasMaxLength(36);
        });

        modelBuilder.Entity<Sinif>(entity =>
        {
            entity.Property(e => e.Adi)
                .IsRequired()
                .HasMaxLength(25);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasMaxLength(36);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        OnModelCreatingGeneratedFunctions(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
