using System;
using System.Collections.Generic;
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

    public virtual DbSet<Access> Accesses { get; set; }

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

}
