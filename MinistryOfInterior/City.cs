//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MinistryOfInterior
{
    using System;
    using System.Collections.Generic;
    
    public partial class City
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Nullable<long> District_ID { get; set; }
        public string Mayor { get; set; }
        public Nullable<int> Population { get; set; }
    
        public virtual District District { get; set; }
    }
}
