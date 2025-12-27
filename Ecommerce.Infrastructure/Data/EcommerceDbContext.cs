using Azure;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Data
{
    public class EcommerceDbContext : IdentityDbContext<Usuario>
    {
        public EcommerceDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<Pago> Pagos { get; set; }

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

            modelBuilder.Entity<Producto>(static entity =>
            {
                entity.HasKey(x => x.idProducto);
                entity.Property(x => x.idProducto)
                .ValueGeneratedOnAdd();

                entity.Property(x => x.nombreProducto)
                .IsRequired()
                .HasMaxLength(40);

                entity.Property(x => x.descripcionProducto)
                .IsRequired()
                .HasMaxLength(500);

                entity.Property(x => x.precio)
                .IsRequired()
                .HasColumnType("decimal(11,2)");

                entity.Property(x => x.stock)
                .IsRequired();

                entity.Property(x => x.seccion)
                .IsRequired()
                .HasMaxLength(6);

                entity.Property(x => x.imagen1)
                .IsRequired()
                .HasMaxLength(500);

                entity.Property(x => x.imagen2)
                .HasMaxLength(500);

                entity.Property(x => x.imagen3)
                .HasMaxLength(500);

                entity.Property(x => x.estado)
                .IsRequired()
                .HasMaxLength(8);

                entity.Property(x => x.fechaRegistro)
                .IsRequired()
                .HasColumnType("datetimeoffset");   // Almacena fecha y zona horaria

                entity.HasIndex(x => x.nombreProducto).IsUnique();

                entity.ToTable(table =>
                {
                    table.HasCheckConstraint("CK_Producto_Seccion", "\"seccion\" IN('Hombre', 'Mujer', 'Unisex')");
                    table.HasCheckConstraint("CK_Producto_Estado", "\"estado\" IN ('Activo', 'Inactivo')");
                });

                // Relación con el modelo de categorías
                entity.HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.idCategoria)
                .IsRequired();
            });

            modelBuilder.Entity<Usuario>(static entity =>
            {
                entity.Property(x => x.nombreCompeto)
                .IsRequired()
                .HasMaxLength(75);
            });

            modelBuilder.Entity<Pedido>(static entity =>
            {
                entity.HasKey(x => x.idPedido);
                entity.Property(x => x.idPedido)
                .ValueGeneratedOnAdd();

                entity.Property(x => x.nombreCliente)
                .IsRequired()
                .HasMaxLength(75);

                entity.Property(x => x.direccionEnvio)
                .IsRequired()
                .HasMaxLength(250);

                entity.Property(x => x.telefono)
                .IsRequired()
                .HasMaxLength(15);

                entity.Property(x => x.total)
                .IsRequired()
                .HasColumnType("decimal(11,2)");

                entity.Property(x => x.estado)
                .IsRequired()
                .HasMaxLength(9);

                entity.Property(x => x.fechaRegistro)
                .IsRequired()
                .HasColumnType("datetimeoffset");   // Almacena fecha y zona horaria

                entity.ToTable(table =>
                {
                    table.HasCheckConstraint("CK_Pedido_Estado", "\"estado\" IN ('Pendiente', 'Entregado', 'Cancelado')");
                });

                // Relación con el modelo usuario
                entity.HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.idUsuario)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            });

            modelBuilder.Entity<DetallePedido>(static entity =>
            {
                entity.HasKey(x => x.idDetallePedido);
                entity.Property(x => x.idDetallePedido)
                .ValueGeneratedOnAdd();

                // Relación con el modelo Pedido
                entity.HasOne(dp => dp.Pedido)
                .WithMany(p => p.Detalles)
                .HasForeignKey(dp => dp.idPedido)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

                // Relación con el modelo Producto
                entity.HasOne(dp => dp.Producto)
                .WithMany(p => p.Detalles)
                .HasForeignKey(dp => dp.idProducto)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

                entity.Property(x => x.nombreProducto)
                .IsRequired()
                .HasMaxLength(40);

                entity.Property(x => x.cantidad)
                .IsRequired();

                entity.Property(x => x.precioUnitario)
                .IsRequired()
                .HasColumnType("decimal(11,2)");

                entity.Property(x => x.subtotal)
                .HasColumnType("decimal(11,2)")
                .HasComputedColumnSql("\"cantidad\" * \"precioUnitario\"", stored: true);
            });

            modelBuilder.Entity<Pago>(static entity =>
            {
                entity.HasKey(x => x.idPago);
                entity.Property(x => x.idPago)
                .ValueGeneratedOnAdd();

                entity.Property(x => x.monto)
                .IsRequired()
                .HasColumnType("decimal(11,2)");

                entity.Property(x => x.moneda)
                .IsRequired()
                .HasMaxLength(3);

                entity.Property(x => x.estado)
                .IsRequired()
                .HasMaxLength(20);

                entity.Property(x => x.fechaPago)
                .IsRequired()
                .HasColumnType("datetimeoffset");   // Almacena fecha y zona horaria

                entity.ToTable(table =>
                {
                    table.HasCheckConstraint("CK_Pago_Estado", "\"estado\" IN ('Aprobado', 'Rechazado', 'Cancelado', 'Pendiente')");
                });

                // Relación con el modelo Pago
                entity.HasOne(p => p.Pedido)
                .WithMany(pe => pe.Pagos)
                .HasForeignKey(p => p.idPedido)
                .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
