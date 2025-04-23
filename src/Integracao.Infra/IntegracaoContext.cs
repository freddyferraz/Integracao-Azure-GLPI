using Integracao.Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Integracao.Infra;
public partial class IntegracaoContext : DbContext
{
    public IntegracaoContext(DbContextOptions<IntegracaoContext> options) : base(options)
    {

    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer().LogTo(Console.WriteLine, LogLevel.Information);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica o filtro global para todas as entidades que implementam Entity, e muda o tipo de string e datetime
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // Iterar sobre todas as propriedades
            foreach (var property in entityType.GetProperties())
            {
                // Configurar varchar para strings em vez de nvarchar
                if (property.ClrType == typeof(string))
                {
                    property.SetColumnType("varchar"); // Ou defina o tamanho que preferir
                }

                // Configurar datetime para DateTime em vez de datetime2
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetColumnType("datetime");
                }

                if (property.ClrType == typeof(DateTime?))
                {
                    property.SetColumnType("datetime");
                }
            }
        }

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.AcodUsuario);

            entity.ToTable("TUSUARIOS");

            entity.Property(e => e.AcodUsuario).HasColumnName("ACOD_USUARIO");
            entity.Property(e => e.AdesEmail).HasColumnName("ADES_EMAIL")
                  .HasMaxLength(50);
            entity.Property(e => e.AdesUsuario).HasColumnName("ADES_USUARIO")
                  .HasMaxLength(50);

        });

        modelBuilder.Entity<Tickets>(entity =>
        {
            entity.HasKey(e => e.AcodTickets);

            entity.ToTable("TTICKETS");

            entity.Property(e => e.AcodTickets).HasColumnName("ACOD_TICKETS");
            entity.Property(e => e.AcodTicketsglpi).HasColumnName("ACOD_TICKETS_GLPI");
            entity.Property(e => e.AcodTicketsDevops).HasColumnName("ACOD_TICKETS_DEVOPS");
            entity.Property(e => e.AcodStatus).HasColumnName("ACOD_STATUS");
            entity.Property(e => e.AdatAlteracao).HasColumnName("ADAT_ALTERACAO")
                  .HasMaxLength(50);
            entity.Property(e => e.AcodUsuario).HasColumnName("ACOD_USUARIO")
                  .HasMaxLength(50);


        });

        modelBuilder.Entity<TDEPARAStatus>(entity =>
        {
            entity.HasKey(e => e.AcodStatus);

            entity.ToTable("TDE_PARA_STATUS");

            entity.Property(e => e.AcodStatusGlpi).HasColumnName("ACOD_STATUS_Glpi");
            entity.Property(e => e.AdesStatusGlpi).HasColumnName("ADES_STATUS_GLPI")
                  .HasMaxLength(50);
            entity.Property(e => e.AcodStatusDevops).HasColumnName("ACOD_STATUS_GLPI");
            entity.Property(e => e.AdesStatusDevops).HasColumnName("ADES_STATUS_GLPI")
                  .HasMaxLength(50);
            entity.Property(e => e.AcodStatus).HasColumnName("ACOD_STATUS");

        });
        
        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
