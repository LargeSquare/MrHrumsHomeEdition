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
    
    public partial class TypeOfEvent
    {
        public TypeOfEvent()
        {
            this.Event = new HashSet<Event>();
        }
    
        public int Id { get; set; }
        public string Type { get; set; }
        public bool IsSystem { get; set; }
    
        public virtual ICollection<Event> Event { get; set; }
    }
}
