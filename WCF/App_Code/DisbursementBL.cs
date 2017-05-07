using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DisbursementBL
/// </summary>
public class DisbursementBL
{
    DisbursementDA d = new DisbursementDA();
    DisbursementBO dbo;
    InventoryStockBO ibo;


    public DisbursementBO convertDisbursementBO(Disbursement e)
    {
        dbo = new DisbursementBO();
        dbo.DepartmentId = e.DepartmentID;
        dbo.DisbursementId = e.DisbursementID;
        dbo.ItemNumber = e.ItemNumber;
        dbo.OrderQuantity = e.OrderQuantity;
        dbo.DisbursementQuantity = e.DisbursementQuantity;
        dbo.Status = e.Status;
        dbo.RequisitionId = e.RequisitionID;
        return dbo;
    }

    public InventoryStockBO convertInventoryStockBO(InventoryStock i)
    {
        ibo = new InventoryStockBO();
        ibo.ItemNumber = i.ItemNumber;
        ibo.ItemName = i.ItemName;
        ibo.Quantity = i.Quantity;
        ibo.ItemCategory = i.ItemCategory;
        ibo.ReorderLevel = i.ReorderLevel;
        ibo.ReorderQuantity = i.ReorderQuantity;
        ibo.ItemUOM = i.ItemUOM;
        ibo.Bin = i.Bin;
        ibo.Supplier1 = i.Supplier1;
        ibo.Supplier2 = i.Supplier2;
        ibo.Supplier3 = i.Supplier3;
        ibo.Price1 = (double)i.Price1;
        ibo.Price2 = (double)i.Price2;
        ibo.Price3 = (double)i.Price3;

        return ibo;
    }

    public List<DisbursedInventoryBO> getDisbursementListByDepId(String depId)
    {
        List<DisbursedInventoryBO> dis = d.getDisListByDepId(depId);
        return dis;

    }
    //public void updateDisbursementStatus(string depId, ArrayList itemNumberList, ArrayList reqNumb, ArrayList disQty)
    //{
    //    d.updateDisList(depId, itemNumberList, reqNumb, disQty);

    //}

    public void updateDisbursementStatus(List<ReadyForCollectionBO> rcboLst)
    {
        d.updateDisList(rcboLst);

    }


}