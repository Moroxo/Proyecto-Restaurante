//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Restaurante
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntitiesRestaurant : DbContext
    {
        public EntitiesRestaurant()
            : base("name=EntitiesRestaurant")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<BOLETA_DE_PAGO> BOLETA_DE_PAGO { get; set; }
        public DbSet<CLIENTE> CLIENTE { get; set; }
        public DbSet<DETALLE_PEDIDO> DETALLE_PEDIDO { get; set; }
        public DbSet<EMPLEADO> EMPLEADO { get; set; }
        public DbSet<INFORMES> INFORMES { get; set; }
        public DbSet<INSUMOS> INSUMOS { get; set; }
        public DbSet<MESA> MESA { get; set; }
        public DbSet<METODO_PAGO> METODO_PAGO { get; set; }
        public DbSet<PEDIDOS> PEDIDOS { get; set; }
        public DbSet<PLATO> PLATO { get; set; }
        public DbSet<RESERVA> RESERVA { get; set; }
        public DbSet<STOCK> STOCK { get; set; }
        public DbSet<TIPO_INSUMO> TIPO_INSUMO { get; set; }
        public DbSet<TIPO_USUARIO> TIPO_USUARIO { get; set; }
        public DbSet<USUARIO> USUARIO { get; set; }
    }
}
