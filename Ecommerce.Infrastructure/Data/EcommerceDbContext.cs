using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Data
{
    //public class EcommerceDbContext : IdentityDbContext<Usuario>
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categoria>(static entity =>
            {
                entity.HasKey(x => x.idCategoria);
                entity.Property(x => x.idCategoria)
                .ValueGeneratedOnAdd();

                entity.Property(x => x.nombreCategoria)
                .IsRequired()
                .HasMaxLength(40);

                entity.Property(x => x.descripcionCategoria)
                .IsRequired()
                .HasMaxLength(500);

                entity.Property(x => x.estado)
                .IsRequired()
                .HasMaxLength(8);

                entity.Property(x => x.fechaRegistro)
                .IsRequired()
                .HasColumnType("datetimeoffset");   // Almacena fecha y zona horaria

                entity.HasIndex(x => x.nombreCategoria).IsUnique();

                entity.ToTable(table =>
                {
                    table.HasCheckConstraint("CK_Categoria_Estado", "\"estado\" IN ('Activo', 'Inactivo')");
                });
            });                      
        }
    }
}
