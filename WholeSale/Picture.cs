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
    
    public partial class Picture
    {
        public int pictureId { get; set; }
        public int productId { get; set; }
        public byte[] image { get; set; }
        public bool isActive { get; set; }
        public string createBy { get; set; }
        public System.DateTime createTime { get; set; }
        public string editBy { get; set; }
        public System.DateTime editTime { get; set; }
        public string compCode { get; set; }
        public string branchCode { get; set; }
    }
}