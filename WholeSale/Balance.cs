//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WholeSale
{
    using System;
    using System.Collections.Generic;
    
    public partial class Balance
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public decimal qty { get; set; }
        public decimal avgPrice { get; set; }
        public decimal maxPrice { get; set; }
        public decimal maxPriceQty { get; set; }
        public decimal minPrice { get; set; }
        public decimal minPriceQty { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string createBy { get; set; }
        public System.DateTime createTime { get; set; }
        public string editBy { get; set; }
        public System.DateTime editTime { get; set; }
        public string compCode { get; set; }
        public string branchCode { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
