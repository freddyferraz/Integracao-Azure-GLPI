using Microsoft.EntityFrameworkCore;
using Integracao.Domain.Entities;

namespace Integracao.Infra.Data;

public sealed class SqlServerDbContext: DbContext
{
    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TDeParaStatus> DeParaStatus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer()
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
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

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.AcodTickets);

            entity.ToTable("TTICKETS");

            entity.Property(e => e.AcodTickets).HasColumnName("ACOD_TICKET");
            entity.Property(e => e.AcodTicketsglpi).HasColumnName("ACOD_TICKET_GLPI");
            entity.Property(e => e.AcodTicketsDevops).HasColumnName("ACOD_TICKET_DEVOPS");
            entity.Property(e => e.AcodStatus).HasColumnName("ACOD_STATUS");
            entity.Property(e => e.AdatAlteracao).HasColumnName("ADAT_ALTERACAO");
            entity.Property(e => e.AcodUsuario).HasColumnName("ACOD_USUARIO")
                  .HasMaxLength(50);


        });

        modelBuilder.Entity<TDeParaStatus>(entity =>
        {
            entity.HasKey(e => e.AcodStatus);

            entity.ToTable("TDE_PARA_STATUS");

            entity.Property(e => e.AcodStatusGlpi).HasColumnName("ACOD_STATUS_Glpi");
            entity.Property(e => e.AdesStatusGlpi).HasColumnName("ADES_STATUS_GLPI")
                  .HasMaxLength(50);
            entity.Property(e => e.AcodStatusDevops).HasColumnName("ACOD_STATUS_DEVOPS");
            entity.Property(e => e.AdesStatusDevops).HasColumnName("ADES_STATUS_DEVOPS")
                  .HasMaxLength(50);
            entity.Property(e => e.AcodStatus).HasColumnName("ACOD_STATUS");

        });
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlServerDbContext).Assembly);
    }
}
