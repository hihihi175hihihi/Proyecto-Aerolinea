using API.Models.ViewModelSP;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class Aerolinea_DesarrolloContext : DbContext
{
    public Aerolinea_DesarrolloContext()
    {
    }

    public Aerolinea_DesarrolloContext(DbContextOptions<Aerolinea_DesarrolloContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Access> Access { get; set; }

    public virtual DbSet<AccessRoles> AccessRoles { get; set; }

    public virtual DbSet<Bitacoras> Bitacoras { get; set; }

    public virtual DbSet<Cargos> Cargos { get; set; }

    public virtual DbSet<Ciudades> Ciudades { get; set; }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<Compras> Compras { get; set; }

    public virtual DbSet<ComprasDetalle> ComprasDetalles { get; set; }

    public virtual DbSet<Empleados> Empleados { get; set; }

    public virtual DbSet<Escalas> Escalas { get; set; }

    public virtual DbSet<FrecuenciaVuelo> FrecuenciaVuelos { get; set; }

    public virtual DbSet<Pagos> Pagos { get; set; }

    public virtual DbSet<Paises> Paises { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Tarjetas> Tarjetas { get; set; }

    public virtual DbSet<Tokens> Tokens { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    public virtual DbSet<Vuelos> Vuelos { get; set; }

    public virtual DbSet<WishList> WishLists { get; set; }
    public virtual DbSet<DetalleCompra> DetalleCompra { get; set; }
    public virtual DbSet<FiltrosVuelos> FiltrosVuelos { get; set; }
    public virtual DbSet<GenerateItinerary> GenerateItinerary { get; set; }
    public virtual DbSet<ReporteCompras> ReporteCompras { get; set; }
    public virtual DbSet<TotalOcupacionxDia> TotalOcupacionxDia { get; set; }
    public virtual DbSet<TotalVentasxDia> TotalVentasxDia { get; set; }
    public virtual DbSet<TotalVentasAnual> TotalVentasAnual { get; set; }
    public virtual DbSet<WishListxUsuario> WishListxUsuario { get; set; }

    public async Task<List<T>> RunSpAsync<T>(string storedProcedureName, params SqlParameter[] parameters) where T : class
    {
        return await Set<T>().FromSqlRaw($"EXEC {storedProcedureName} {string.Join(",", parameters.Select(p => p.ParameterName))}", parameters).ToListAsync();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FiltrosVuelos>(entity => {entity.HasNoKey();});
        modelBuilder.Entity<WishListxUsuario>(entity => {entity.HasNoKey();});
        modelBuilder.Entity<DetalleCompra>(entity => {entity.HasNoKey();});
        modelBuilder.Entity<GenerateItinerary>(entity => {entity.HasNoKey();});
        modelBuilder.Entity<ReporteCompras>(entity => {entity.HasNoKey();});
        modelBuilder.Entity<TotalVentasxDia>(entity => {entity.HasNoKey();});
        modelBuilder.Entity<TotalVentasAnual>(entity => {entity.HasNoKey();});
        modelBuilder.Entity<TotalOcupacionxDia>(entity => {entity.HasNoKey();});
        
    }
}
