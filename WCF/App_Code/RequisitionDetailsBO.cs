using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RequisitionDetailsBO
/// </summary>
public class RequisitionDetailsBO
{
    private string requisitionDetailsId;
    private string requisitionId;
    private string itemNumber;
    private int? ItemQuantity;
    private InventoryStockBO item;

    public InventoryStockBO Item
    {
        get { return item; }
        set { item = value; }
    }


    public string RequisitionDetailsId
    {
        get
        {
            return requisitionDetailsId;
        }

        set
        {
            requisitionDetailsId = value;
        }
    }

    public string RequisitionId
    {
        get
        {
            return requisitionId;
        }

        set
        {
            requisitionId = value;
        }
    }

    public string ItemNumber
    {
        get
        {
            return itemNumber;
        }

        set
        {
            itemNumber = value;
        }
    }

    public int? ItemQuantity1
    {
        get
        {
            return ItemQuantity;
        }

        set
        {
            ItemQuantity = value;
        }
    }
}