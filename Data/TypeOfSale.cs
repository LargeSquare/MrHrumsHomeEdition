//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MrHrumsHomeEdition.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TypeOfSale
    {
        public TypeOfSale()
        {
            this.PositionInOrder = new HashSet<PositionInOrder>();
        }
    
        public int Id { get; set; }
        public string Type { get; set; }
        public bool Visible { get; set; }
        public bool IsSystem { get; set; }
    
        public virtual ICollection<PositionInOrder> PositionInOrder { get; set; }
    }
}
