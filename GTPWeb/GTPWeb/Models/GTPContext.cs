using Microsoft.EntityFrameworkCore;

namespace GTPWeb.Models
{
    public class GTPContext : DbContext
    {
        public GTPContext(DbContextOptions<GTPContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<UsuarioEnProyecto> UsuariosEnProyectos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CorreoElectronico).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Contrasena).IsRequired().HasMaxLength(256);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(255);
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(255);
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(255);
                entity.Property(e => e.Prioridad).HasMaxLength(50);
                entity.Property(e => e.Categoria).HasMaxLength(50);
                entity.Property(e => e.Estado).IsRequired();

                // Configurar relación con UsuarioEnProyecto
                entity.HasOne(e => e.UsuarioEnProyecto)
                      .WithMany(up => up.Tareas)
                      .HasForeignKey(e => e.UsuarioEnProyectoID)
                      .OnDelete(DeleteBehavior.Restrict); // Cambiado a Restrict o NoAction si es necesario
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Mensaje).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.FechaCreacion).IsRequired();

                entity.HasOne(e => e.Tarea)
                      .WithMany(t => t.Comentarios)
                      .HasForeignKey(e => e.TareaID)
                      .OnDelete(DeleteBehavior.Restrict); // Cambiado a Restrict o NoAction si es necesario

                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Comentarios)
                      .HasForeignKey(e => e.UsuarioID)
                      .OnDelete(DeleteBehavior.Restrict); // Cambiado a Restrict o NoAction si es necesario
            });

            modelBuilder.Entity<UsuarioEnProyecto>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.HasOne(e => e.Proyecto)
                      .WithMany(p => p.UsuariosEnProyectos)
                      .HasForeignKey(e => e.ProyectoID)
                      .OnDelete(DeleteBehavior.Restrict); // Cambiado a Restrict o NoAction si es necesario

                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.UsuariosEnProyectos)
                      .HasForeignKey(e => e.UsuarioID)
                      .OnDelete(DeleteBehavior.Restrict); // Cambiado a Restrict o NoAction si es necesario

                entity.HasOne(e => e.Rol)
                      .WithMany(r => r.UsuariosEnProyectos)
                      .HasForeignKey(e => e.RolID)
                      .OnDelete(DeleteBehavior.Restrict); // Cambiado a Restrict o NoAction si es necesario
            });
           
        }


    }
}
