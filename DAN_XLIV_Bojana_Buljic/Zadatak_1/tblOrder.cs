//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zadatak_1
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOrder
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public Nullable<int> ItemId { get; set; }
        public int Amount { get; set; }
        public System.DateTime OrderDateTime { get; set; }
        public string OrderStatus { get; set; }
    
        public virtual tblMenu tblMenu { get; set; }
    }
}
