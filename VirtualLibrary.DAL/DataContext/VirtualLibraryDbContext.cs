using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VirtualLibrary.Models;

namespace VirtualLibrary.DAL.DataContext;

public partial class VirtualLibraryDbContext : DbContext
{
    public VirtualLibraryDbContext()
    {
    }

    public VirtualLibraryDbContext(DbContextOptions<VirtualLibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Editorial> Editorials { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolUsuario> RolUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.AutorId).HasName("PK__Autor__F58AE9292A324130");

            entity.ToTable("Autor");

            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Editorial>(entity =>
        {
            entity.HasKey(e => e.EditorialId).HasName("PK__Editoria__D54C82EE02A3D261");

            entity.ToTable("Editorial");

            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroId).HasName("PK__Genero__A99D024810174590");

            entity.ToTable("Genero");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.LibroId).HasName("PK__Libro__35A1ECEDD9F7C315");

            entity.ToTable("Libro");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.AutorIdAutorNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.AutorIdAutor)
                .HasConstraintName("FK__Libro__AutorIdAu__3D5E1FD2");

            entity.HasOne(d => d.EditorialIdEditorialNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.EditorialIdEditorial)
                .HasConstraintName("FK__Libro__Editorial__3F466844");

            entity.HasOne(d => d.GeneroIdGeneroNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.GeneroIdGenero)
                .HasConstraintName("FK__Libro__GeneroIdG__3E52440B");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584CC4727866");

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolUsuario>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.RolIdRolNavigation).WithMany()
                .HasForeignKey(d => d.RolIdRol)
                .HasConstraintName("FK__RolUsuari__RolId__44FF419A");

            entity.HasOne(d => d.UsuarioIdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.UsuarioIdUsuario)
                .HasConstraintName("FK__RolUsuari__Usuar__45F365D3");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF971A5D6463");

            entity.ToTable("Usuario");

            entity.Property(e => e.Contraseña)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
