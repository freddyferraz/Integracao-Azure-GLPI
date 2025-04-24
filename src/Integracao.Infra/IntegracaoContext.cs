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
       
        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
