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
    
    public partial class INSUMOS
    {
        public INSUMOS()
        {
            this.STOCK1 = new HashSet<STOCK>();
        }
    
        public decimal ID_INSUMO { get; set; }
        public string NOM_INSUMO { get; set; }
        public decimal PRECIO_UNITARIO { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal STOCK { get; set; }
        public decimal ID_TIPOINSUMO { get; set; }
    
        public virtual ICollection<STOCK> STOCK1 { get; set; }
        public virtual TIPO_INSUMO TIPO_INSUMO { get; set; }
    }
}
