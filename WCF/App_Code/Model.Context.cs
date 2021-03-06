﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Linq;

public partial class SA43Team2StoreDBEntities : DbContext
{
    public SA43Team2StoreDBEntities()
        : base("name=SA43Team2StoreDBEntities")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }

    public virtual DbSet<Adjustment> Adjustments { get; set; }
    public virtual DbSet<AdjustmentDetail> AdjustmentDetails { get; set; }
    public virtual DbSet<CollectionPointDetail> CollectionPointDetails { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Disbursement> Disbursements { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<InventoryStock> InventoryStocks { get; set; }
    public virtual DbSet<OutstandingInfo> OutstandingInfoes { get; set; }
    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    public virtual DbSet<Requisition> Requisitions { get; set; }
    public virtual DbSet<RequisitionDetail> RequisitionDetails { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    public virtual DbSet<View_2> View_2 { get; set; }

    public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
    {
        var diagramnameParameter = diagramname != null ?
            new ObjectParameter("diagramname", diagramname) :
            new ObjectParameter("diagramname", typeof(string));

        var owner_idParameter = owner_id.HasValue ?
            new ObjectParameter("owner_id", owner_id) :
            new ObjectParameter("owner_id", typeof(int));

        var versionParameter = version.HasValue ?
            new ObjectParameter("version", version) :
            new ObjectParameter("version", typeof(int));

        var definitionParameter = definition != null ?
            new ObjectParameter("definition", definition) :
            new ObjectParameter("definition", typeof(byte[]));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
    }

    public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
    {
        var diagramnameParameter = diagramname != null ?
            new ObjectParameter("diagramname", diagramname) :
            new ObjectParameter("diagramname", typeof(string));

        var owner_idParameter = owner_id.HasValue ?
            new ObjectParameter("owner_id", owner_id) :
            new ObjectParameter("owner_id", typeof(int));

        var versionParameter = version.HasValue ?
            new ObjectParameter("version", version) :
            new ObjectParameter("version", typeof(int));

        var definitionParameter = definition != null ?
            new ObjectParameter("definition", definition) :
            new ObjectParameter("definition", typeof(byte[]));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
    }

    public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
    {
        var diagramnameParameter = diagramname != null ?
            new ObjectParameter("diagramname", diagramname) :
            new ObjectParameter("diagramname", typeof(string));

        var owner_idParameter = owner_id.HasValue ?
            new ObjectParameter("owner_id", owner_id) :
            new ObjectParameter("owner_id", typeof(int));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
    }

    public virtual int sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
    {
        var diagramnameParameter = diagramname != null ?
            new ObjectParameter("diagramname", diagramname) :
            new ObjectParameter("diagramname", typeof(string));

        var owner_idParameter = owner_id.HasValue ?
            new ObjectParameter("owner_id", owner_id) :
            new ObjectParameter("owner_id", typeof(int));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
    }

    public virtual int sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
    {
        var diagramnameParameter = diagramname != null ?
            new ObjectParameter("diagramname", diagramname) :
            new ObjectParameter("diagramname", typeof(string));

        var owner_idParameter = owner_id.HasValue ?
            new ObjectParameter("owner_id", owner_id) :
            new ObjectParameter("owner_id", typeof(int));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
    }

    public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
    {
        var diagramnameParameter = diagramname != null ?
            new ObjectParameter("diagramname", diagramname) :
            new ObjectParameter("diagramname", typeof(string));

        var owner_idParameter = owner_id.HasValue ?
            new ObjectParameter("owner_id", owner_id) :
            new ObjectParameter("owner_id", typeof(int));

        var new_diagramnameParameter = new_diagramname != null ?
            new ObjectParameter("new_diagramname", new_diagramname) :
            new ObjectParameter("new_diagramname", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
    }

    public virtual int sp_upgraddiagrams()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
    }

    public virtual ObjectResult<string> spCategoryList()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("spCategoryList");
    }

    public virtual ObjectResult<spGetItemsByCategory_Result> spGetItemsByCategory(string itemCategory)
    {
        var itemCategoryParameter = itemCategory != null ?
            new ObjectParameter("ItemCategory", itemCategory) :
            new ObjectParameter("ItemCategory", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetItemsByCategory_Result>("spGetItemsByCategory", itemCategoryParameter);
    }

    public virtual ObjectResult<spGetSupplierByItemName_Result> spGetSupplierByItemName(string itemName)
    {
        var itemNameParameter = itemName != null ?
            new ObjectParameter("ItemName", itemName) :
            new ObjectParameter("ItemName", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetSupplierByItemName_Result>("spGetSupplierByItemName", itemNameParameter);
    }

    public virtual ObjectResult<spGetUOMForItem_Result> spGetUOMForItem(string itemName)
    {
        var itemNameParameter = itemName != null ?
            new ObjectParameter("ItemName", itemName) :
            new ObjectParameter("ItemName", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetUOMForItem_Result>("spGetUOMForItem", itemNameParameter);
    }

    public virtual ObjectResult<spPurchaseOrderList_Result> spPurchaseOrderList(string requestorID)
    {
        var requestorIDParameter = requestorID != null ?
            new ObjectParameter("requestorID", requestorID) :
            new ObjectParameter("requestorID", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spPurchaseOrderList_Result>("spPurchaseOrderList", requestorIDParameter);
    }
}
