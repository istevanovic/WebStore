//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Prodavnica.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class slike
    {
        public int idSlika { get; set; }
        public int idProizvod { get; set; }
        public string putanjaSlika { get; set; }
    
        public virtual proizvod proizvod { get; set; }
    }
}
