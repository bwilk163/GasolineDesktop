//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GasolineDesktop
{
    using System;
    using System.Collections.Generic;
    
    public partial class GasStationFuel
    {
        public System.Guid FuelTypeId { get; set; }
        public System.Guid GasStationId { get; set; }
        public decimal Price { get; set; }
        public System.DateTime LastUpdateUtc { get; set; }
    
        public virtual FuelType FuelType { get; set; }
        public virtual GasStation GasStation { get; set; }
    }
}
