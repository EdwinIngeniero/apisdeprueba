using Microsoft.EntityFrameworkCore;

namespace Apis_de_prueba.Models;

public partial class CrudPruebaContext : DbContext
{
    public CrudPruebaContext()
    {
    }

    public CrudPruebaContext(DbContextOptions<CrudPruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tarea> Tareas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.CodigoTarea).HasName("PK__Tareas__FEEA7E1AFFBC6F7F");

            entity.Property(e => e.CodigoTarea)
                .ValueGeneratedNever()
                .HasColumnName("Codigo_tarea");
            entity.Property(e => e.DiasActivos)
                .HasComputedColumnSql("(datediff(day,[Fecha_inicio],getdate()))", false)
                .HasColumnName("Dias_activos");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaInicio)
                .HasColumnType("date")
                .HasColumnName("Fecha_inicio");
            entity.Property(e => e.MiTarea)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Mi_tarea");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
