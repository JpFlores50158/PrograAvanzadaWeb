using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace GPTApi.Models
{
    public class GTPContext : DbContext
    {
        public GTPContext(DbContextOptions<GTPContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CorreoElectronico).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Contrasena).IsRequired().HasMaxLength(256);


            });
            base.OnModelCreating(modelBuilder);

        }
    }
}
