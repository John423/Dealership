//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dealership2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public int TransactionID { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public System.DateTime DateOfTransaction { get; set; }
        public int CustomerID { get; set; }
        public int VehicleID { get; set; }
        public int ServiceID { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}