using Microsoft.EntityFrameworkCore;
using RentCars.Api.Models;

namespace RentCars.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Alquiler> Alquileres { get; set; }
        public DbSet<Multa> Multas { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        [Obsolete]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Forzar nombres de tabla en singular para coincidir con la base
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Vehiculo>().ToTable("Vehiculo");
            modelBuilder.Entity<Empleado>().ToTable("Empleado");
            modelBuilder.Entity<Alquiler>().ToTable("Alquiler");
            modelBuilder.Entity<Pago>().ToTable("Pago");
            modelBuilder.Entity<Multa>().ToTable("Multa");
            modelBuilder.Entity<Contacto>().ToTable("Contacto");
             
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioId);

                entity.Property(e => e.Nombre_Completo).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Email).IsRequired();

                entity.HasMany(e => e.Alquileres)
                      .WithOne(a => a.Usuario)
                      .HasForeignKey(a => a.UsuarioId);

                entity.HasMany(e => e.Contactos)
                      .WithOne(c => c.Usuario)
                      .HasForeignKey(c => c.UsuarioId);
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.EmpleadoId);

                entity.Property(e => e.Nombre_Completo).IsRequired();
                entity.Property(e => e.Cargo).IsRequired();
                entity.Property(e => e.DNI).IsRequired();
                entity.Property(e => e.Telefono).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Sucursal).IsRequired();

                entity.HasMany(e => e.Alquileres)
                      .WithOne(a => a.Empleado)
                      .HasForeignKey(a => a.EmpleadoId);
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.VehiculoId);

                entity.Property(e => e.Marca).IsRequired();
                entity.Property(e => e.Modelo).IsRequired();
                entity.Property(e => e.Placa).IsRequired();
                entity.Property(e => e.Color).IsRequired();
                entity.Property(e => e.Tipo).IsRequired();
                entity.Property(e => e.Estado).IsRequired();

                entity.HasMany(e => e.Alquileres)
                      .WithOne(a => a.Vehiculo)
                      .HasForeignKey(a => a.VehiculoId);
            });

            modelBuilder.Entity<Alquiler>(entity =>
            {
                entity.HasKey(e => e.AlquilerId);

                entity.Property(e => e.Estado).IsRequired();

                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Alquileres)
                      .HasForeignKey(e => e.UsuarioId);

                entity.HasOne(e => e.Vehiculo)
                      .WithMany(v => v.Alquileres)
                      .HasForeignKey(e => e.VehiculoId);

                entity.HasOne(e => e.Empleado)
                      .WithMany(emp => emp.Alquileres)
                      .HasForeignKey(e => e.EmpleadoId);

                entity.HasMany(e => e.Pagos)
                      .WithOne(p => p.Alquiler)
                      .HasForeignKey(p => p.AlquilerId);

                entity.HasMany(e => e.Multas)
                      .WithOne(m => m.Alquiler)
                      .HasForeignKey(m => m.AlquilerId);
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.PagoId);

                entity.Property(e => e.MetodoPago).IsRequired();

                entity.HasOne(e => e.Alquiler)
                      .WithMany(a => a.Pagos)
                      .HasForeignKey(e => e.AlquilerId);
            });

            modelBuilder.Entity<Multa>(entity =>
            {
                entity.HasKey(e => e.MultaId);

                entity.Property(e => e.Descripcion).IsRequired();
                entity.Property(e => e.Estado).IsRequired();
                entity.Property(e => e.Tipo).IsRequired();

                entity.HasOne(e => e.Alquiler)
                      .WithMany(a => a.Multas)
                      .HasForeignKey(e => e.AlquilerId);
            });

            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.ContactoId);

                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Motivo).IsRequired();
                entity.Property(e => e.Mensaje).IsRequired();

                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Contactos)
                      .HasForeignKey(e => e.UsuarioId);

                entity.HasCheckConstraint("CK_Contacto_Motivo",
                    "[Motivo] IN ('Consulta', 'Queja', 'Sugerencia', 'Problema tecnico', 'Otro')");
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}