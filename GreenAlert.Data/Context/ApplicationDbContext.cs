using Microsoft.EntityFrameworkCore;
using GreenAlert.Core.Models;

namespace GreenAlert.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Estacao> Estacoes { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<Alerta> Alertas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nomes de tabelas
            modelBuilder.Entity<Estacao>().ToTable("ESTACAO");
            modelBuilder.Entity<Sensor>().ToTable("SENSOR");
            modelBuilder.Entity<Alerta>().ToTable("ALERTA");

            // Relacionamentos
            modelBuilder.Entity<Sensor>()
                .HasOne(s => s.Estacao)
                .WithMany(e => e.Sensores)
                .HasForeignKey(s => s.EstacaoId);

            modelBuilder.Entity<Alerta>()
                .HasOne(a => a.Sensor)
                .WithMany(s => s.Alertas)
                .HasForeignKey(a => a.SensorId);
        }
    }
}
