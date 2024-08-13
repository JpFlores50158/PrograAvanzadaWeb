using Microsoft.EntityFrameworkCore;

namespace pro_GestorTareasProyectos.Models
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
                entity.HasKey(e => e.ID);
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

            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Mensaje).IsRequired().HasMaxLength(500);
                entity.HasOne(e => e.Tarea)
                      .WithMany(t => t.Comentarios)
                      .HasForeignKey(e => e.TareaID)
                      .OnDelete(DeleteBehavior.Restrict);  
                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Comentarios)
                      .HasForeignKey(e => e.UsuarioID)
                      .OnDelete(DeleteBehavior.Restrict);  
            });

            modelBuilder.Entity<UsuarioEnProyecto>(entity =>
            {
                entity.HasKey(e => e.ID);
            });

            modelBuilder.Entity<Comentario>().HasOne(x => x.Tarea).WithMany(m => m.Comentarios).HasForeignKey(f => f.TareaID);
            modelBuilder.Entity<Comentario>().HasOne(x => x.Usuario).WithMany(m => m.Comentarios).HasForeignKey(f => f.UsuarioID);

            base.OnModelCreating(modelBuilder);
        }

    }
}
