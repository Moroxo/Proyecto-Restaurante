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
    using System.Collections.Generic;
    
    public partial class MESA
    {
        public MESA()
        {
            this.PEDIDOS = new HashSet<PEDIDOS>();
            this.RESERVA = new HashSet<RESERVA>();
        }
    
        public decimal NRO_MESA { get; set; }
        public string TIPO_MESA { get; set; }
        public decimal NRO_SILLAS { get; set; }
        public string ESTADO { get; set; }
    
        public virtual ICollection<PEDIDOS> PEDIDOS { get; set; }
        public virtual ICollection<RESERVA> RESERVA { get; set; }
    }
}
