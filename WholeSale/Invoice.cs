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
    
    public partial class Invoice
    {
        public int invoiceId { get; set; }
        public string invoiceNo { get; set; }
        public int orderId { get; set; }
        public int customerId { get; set; }
        public int vendorId { get; set; }
        public decimal totalVat { get; set; }
        public decimal totalDc { get; set; }
        public decimal totalQty { get; set; }
        public decimal totalPrice { get; set; }
        public int status { get; set; }
        public bool isActive { get; set; }
        public string remark { get; set; }
        public string createBy { get; set; }
        public System.DateTime createTime { get; set; }
        public string editBy { get; set; }
        public System.DateTime editTime { get; set; }
        public string compCode { get; set; }
        public string branchCode { get; set; }
    }
}