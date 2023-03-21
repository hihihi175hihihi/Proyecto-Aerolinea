using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acceso> Accesos { get; set; }

    public virtual DbSet<Accessrole> Accessroles { get; set; }

    public virtual DbSet<Bitacora> Bitacoras { get; set; }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Ciudade> Ciudades { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<ComprasDetalle> ComprasDetalles { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Escala> Escalas { get; set; }

    public virtual DbSet<FrecuenciaVuelo> FrecuenciaVuelos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Paise> Paises { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tarjeta> Tarjetas { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vuelo> Vuelos { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("USR_AEROLINEA_DESA");

        modelBuilder.Entity<Acceso>(entity =>
        {
            entity.HasKey(e => e.IdAccess).HasName("ACESO_PK");

            entity.ToTable("ACCESOS");

            entity.Property(e => e.IdAccess)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ACCESS");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Url)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Accessrole>(entity =>
        {
            entity.HasKey(e => e.IdAccessRoles).HasName("ACCESSROLES_PK");

            entity.ToTable("ACCESSROLES");

            entity.Property(e => e.IdAccessRoles)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ACCESS_ROLES");
            entity.Property(e => e.IdAccess)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ACCESS");
            entity.Property(e => e.IdRol)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ROL");

            entity.HasOne(d => d.IdAccessNavigation).WithMany(p => p.Accessroles)
                .HasForeignKey(d => d.IdAccess)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ACCESSROLES_FK1");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Accessroles)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ACCESSROLES_FK2");
        });

        modelBuilder.Entity<Bitacora>(entity =>
        {
            entity.HasKey(e => e.IdBitacora).HasName("BITACORA_PK");

            entity.ToTable("BITACORAS");

            entity.Property(e => e.IdBitacora)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_BITACORA");
            entity.Property(e => e.Accion)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("ACCION");
            entity.Property(e => e.Error)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("ERROR");
            entity.Property(e => e.Fecha)
                .HasColumnType("DATE")
                .HasColumnName("FECHA");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_USUARIO");
            entity.Property(e => e.Request)
                .IsUnicode(false)
                .HasColumnName("REQUEST");
            entity.Property(e => e.Response)
                .IsUnicode(false)
                .HasColumnName("RESPONSE");
            entity.Property(e => e.Tabla)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TABLA");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Bitacoras)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BITACORAS_FK1");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo).HasName("CARGO_PK");

            entity.ToTable("CARGOS");

            entity.Property(e => e.IdCargo)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_CARGO");
            entity.Property(e => e.Cargo1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CARGO");
        });

        modelBuilder.Entity<Ciudade>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("CIUDADES_PK");

            entity.ToTable("CIUDADES");

            entity.Property(e => e.IdCiudad)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_CIUDAD");
            entity.Property(e => e.IdPais)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_PAIS");
            entity.Property(e => e.NombreCiudad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_CIUDAD");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Ciudades)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CIUDADES_FK1");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("CLIENTES_PK");

            entity.ToTable("CLIENTES");

            entity.HasIndex(e => e.IdUsuario, "CLIENTES_INDEX1").IsUnique();

            entity.Property(e => e.IdCliente)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_CLIENTE");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("APELLIDOS");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("CORREO_ELECTRONICO");
            entity.Property(e => e.Dpi)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DPI");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_INGRESO");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_USUARIO");
            entity.Property(e => e.Nombres)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NOMBRES");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CLIENTES_FK1");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("COMPRAS_PK");

            entity.ToTable("COMPRAS");

            entity.Property(e => e.IdCompra)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_COMPRA");
            entity.Property(e => e.FechaCompra)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_COMPRA");
            entity.Property(e => e.IdCliente)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_CLIENTE");
            entity.Property(e => e.Total)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTAL");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("COMPRAS_FK1");
        });

        modelBuilder.Entity<ComprasDetalle>(entity =>
        {
            entity.HasKey(e => e.IdComprasDetalle).HasName("COMPRASDETALLE_PK");

            entity.ToTable("COMPRAS_DETALLE");

            entity.Property(e => e.IdComprasDetalle)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_COMPRAS_DETALLE");
            entity.Property(e => e.Cantidad)
                .HasColumnType("NUMBER")
                .HasColumnName("CANTIDAD");
            entity.Property(e => e.IdCompra)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_COMPRA");
            entity.Property(e => e.IdVuelo)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_VUELO");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.ComprasDetalles)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("COMPRAS_DETALLE_FK1");

            entity.HasOne(d => d.IdVueloNavigation).WithMany(p => p.ComprasDetalles)
                .HasForeignKey(d => d.IdVuelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("COMPRAS_DETALLE_FK2");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("EMPLEADO_PK");

            entity.ToTable("EMPLEADOS");

            entity.HasIndex(e => e.IdUsuario, "EMPLEADOS_INDEX1").IsUnique();

            entity.Property(e => e.IdEmpleado)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_EMPLEADO");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.CodigoEmpleado)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CODIGO_EMPLEADO");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DIRECCION");
            entity.Property(e => e.IdCargo)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_CARGO");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_USUARIO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCargo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EMPLEADOS_FK1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Empleado)
                .HasForeignKey<Empleado>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EMPLEADOS_FK2");
        });

        modelBuilder.Entity<Escala>(entity =>
        {
            entity.HasKey(e => e.IdEscala).HasName("ESCALA_PK");

            entity.ToTable("ESCALAS");

            entity.Property(e => e.IdEscala)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ESCALA");
            entity.Property(e => e.DuracionEscala)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("DURACION_ESCALA");
            entity.Property(e => e.DuracionLlegada)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("DURACION_LLEGADA");
            entity.Property(e => e.IdCiudadEscala)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_CIUDAD_ESCALA");
            entity.Property(e => e.IdVuelo)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_VUELO");

            entity.HasOne(d => d.IdCiudadEscalaNavigation).WithMany(p => p.Escalas)
                .HasForeignKey(d => d.IdCiudadEscala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ESCALAS_FK1");

            entity.HasOne(d => d.IdVueloNavigation).WithMany(p => p.Escalas)
                .HasForeignKey(d => d.IdVuelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ESCALAS_FK2");
        });

        modelBuilder.Entity<FrecuenciaVuelo>(entity =>
        {
            entity.HasKey(e => e.IdFrecuenciaVuelo).HasName("FRECUENCIA_VUELOS_PK");

            entity.ToTable("FRECUENCIA_VUELOS");

            entity.Property(e => e.IdFrecuenciaVuelo)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_FRECUENCIA_VUELO");
            entity.Property(e => e.DiaSemana)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DIA_SEMANA");
            entity.Property(e => e.HoraLlegada)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("HORA_LLEGADA");
            entity.Property(e => e.HoraSalida)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("HORA_SALIDA");
            entity.Property(e => e.IdVuelo)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_VUELO");

            entity.HasOne(d => d.IdVueloNavigation).WithMany(p => p.FrecuenciaVuelos)
                .HasForeignKey(d => d.IdVuelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FRECUENCIA_VUELOS_FK1");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PAGOS_PK");

            entity.ToTable("PAGOS");

            entity.Property(e => e.IdPago)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_PAGO");
            entity.Property(e => e.FechaPago)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_PAGO");
            entity.Property(e => e.IdCompra)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_COMPRA");
            entity.Property(e => e.MontoPago)
                .HasColumnType("NUMBER(18,2)")
                .HasColumnName("MONTO_PAGO");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PAGOS_FK1");
        });

        modelBuilder.Entity<Paise>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PAISES_PK");

            entity.ToTable("PAISES");

            entity.Property(e => e.IdPais)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_PAIS");
            entity.Property(e => e.NombrePais)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_PAIS");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("ROLES_PK");

            entity.ToTable("ROLES");

            entity.Property(e => e.IdRol)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ROL");
            entity.Property(e => e.Rol)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ROL");
        });

        modelBuilder.Entity<Tarjeta>(entity =>
        {
            entity.HasKey(e => e.IdTarjeta).HasName("TARJETAS_PK");

            entity.ToTable("TARJETAS");

            entity.Property(e => e.IdTarjeta)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_TARJETA");
            entity.Property(e => e.Brand)
                .HasColumnType("NUMBER")
                .HasColumnName("BRAND");
            entity.Property(e => e.ExpMonth)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("EXP_MONTH");
            entity.Property(e => e.ExpYear)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("EXP_YEAR");
            entity.Property(e => e.IdCliente)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_CLIENTE");
            entity.Property(e => e.Last4)
                .HasColumnType("NUMBER")
                .HasColumnName("LAST4");
            entity.Property(e => e.TokenCard)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("TOKEN_CARD");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Tarjeta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TARJETAS_FK1");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.IdToken).HasName("TOKENS_PK");

            entity.ToTable("TOKENS");

            entity.Property(e => e.IdToken)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_TOKEN");
            entity.Property(e => e.Active)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("ACTIVE");
            entity.Property(e => e.CreateAt)
                .HasColumnType("DATE")
                .HasColumnName("CREATE_AT");
            entity.Property(e => e.Expiration)
                .HasColumnType("DATE")
                .HasColumnName("EXPIRATION");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_USUARIO");
            entity.Property(e => e.Token1)
                .IsUnicode(false)
                .HasColumnName("TOKEN");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TOKENS_FK1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("USUARIO_PK");

            entity.ToTable("USUARIOS");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_USUARIO");
            entity.Property(e => e.Active)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("ACTIVE");
            entity.Property(e => e.IdRol)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_ROL");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("USUARIOS_FK1");
        });

        modelBuilder.Entity<Vuelo>(entity =>
        {
            entity.HasKey(e => e.IdVuelo).HasName("VUELO_PK");

            entity.ToTable("VUELOS");

            entity.Property(e => e.IdVuelo)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID_VUELO");
            entity.Property(e => e.IdCiudadDestino)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_CIUDAD_DESTINO");
            entity.Property(e => e.IdCiudadOrigen)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_CIUDAD_ORIGEN");
            entity.Property(e => e.MaxPasajeros)
                .HasColumnType("NUMBER")
                .HasColumnName("MAX_PASAJEROS");
            entity.Property(e => e.Precio)
                .HasColumnType("NUMBER(18,3)")
                .HasColumnName("PRECIO");

            entity.HasOne(d => d.IdCiudadDestinoNavigation).WithMany(p => p.VueloIdCiudadDestinoNavigations)
                .HasForeignKey(d => d.IdCiudadDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("VUELOS_FK2");

            entity.HasOne(d => d.IdCiudadOrigenNavigation).WithMany(p => p.VueloIdCiudadOrigenNavigations)
                .HasForeignKey(d => d.IdCiudadOrigen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("VUELOS_FK1");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.IdWishlist).HasName("WISHLIST_PK");

            entity.ToTable("WISHLIST");

            entity.Property(e => e.IdWishlist)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_WISHLIST");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_CREACION");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_USUARIO");
            entity.Property(e => e.IdVuelo)
                .HasColumnType("NUMBER")
                .HasColumnName("ID_VUELO");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("WISHLIST_FK2");

            entity.HasOne(d => d.IdVueloNavigation).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.IdVuelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("WISHLIST_FK1");
        });
        modelBuilder.HasSequence("ACCESSROLES_SEQ");
        modelBuilder.HasSequence("ACESO_SEQ");
        modelBuilder.HasSequence("BITACORA_SEQ");
        modelBuilder.HasSequence("CARGOS_SEQ");
        modelBuilder.HasSequence("CIUDADES_SEQ");
        modelBuilder.HasSequence("CLIENTES_SEQ");
        modelBuilder.HasSequence("COMPRAS_SEQ");
        modelBuilder.HasSequence("COMPRASDETALLE_SEQ");
        modelBuilder.HasSequence("EMPLEADO_SEQ");
        modelBuilder.HasSequence("EMPLEADOS_SEQ");
        modelBuilder.HasSequence("ESCALA_SEQ");
        modelBuilder.HasSequence("ESCALAS_SEQ");
        modelBuilder.HasSequence("FRECUENCIA_VUELOS_SEQ");
        modelBuilder.HasSequence("PAGOS_SEQ");
        modelBuilder.HasSequence("PAISES_SEQ");
        modelBuilder.HasSequence("ROLES_SEQ");
        modelBuilder.HasSequence("TARJETAS_SEQ");
        modelBuilder.HasSequence("TOKENS_SEQ");
        modelBuilder.HasSequence("USUARIO_SEQ");
        modelBuilder.HasSequence("VUELO_SEQ");
        modelBuilder.HasSequence("WISHLIST_SEQ");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
