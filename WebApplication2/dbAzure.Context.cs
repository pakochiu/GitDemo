﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbMSIT126TeamEntities : DbContext
    {
        public dbMSIT126TeamEntities()
            : base("name=dbMSIT126TeamEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tAluminumMaterialList> tAluminumMaterialLists { get; set; }
        public virtual DbSet<tCase> tCases { get; set; }
        public virtual DbSet<tCheck> tChecks { get; set; }
        public virtual DbSet<tCustomer> tCustomers { get; set; }
        public virtual DbSet<tDate> tDates { get; set; }
        public virtual DbSet<tDepartment> tDepartments { get; set; }
        public virtual DbSet<tEmployee> tEmployees { get; set; }
        public virtual DbSet<tFaceDetection> tFaceDetections { get; set; }
        public virtual DbSet<tGlass> tGlasses { get; set; }
        public virtual DbSet<tJobTitle> tJobTitles { get; set; }
        public virtual DbSet<tNew> tNews { get; set; }
        public virtual DbSet<tOrderComment> tOrderComments { get; set; }
        public virtual DbSet<tOrderDetail> tOrderDetails { get; set; }
        public virtual DbSet<tOrder> tOrders { get; set; }
        public virtual DbSet<tProduct> tProducts { get; set; }
        public virtual DbSet<tProductList> tProductLists { get; set; }
        public virtual DbSet<tWorkingTime> tWorkingTimes { get; set; }
    }
}
