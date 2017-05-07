using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DisbursedInventoryBO
/// </summary>
public class DisbursedInventoryBO
{
    private string itemName;
    private string itemNumber;
    private string itemUOM;
    private int orderQuantity;
    private int disbursementQuantity;
    private int quantity;
    private string reqId;
    public string ItemName
    {
        get
        {
            return itemName;
        }
        set
        {
            itemName = value;
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
    public string ItemUOM
    {
        get
        {
            return itemUOM;
        }
        set
        {
            itemUOM = value;
        }
    }
    public int OrderQuantity
    {
        get
        {
            return orderQuantity;
        }
        set
        {
            orderQuantity = value;
        }
    }
    public int DisbursementQuantity
    {
        get
        {
            return disbursementQuantity;
        }
        set
        {
            disbursementQuantity = value;
        }
    }
    public int OutstandingQuantity
    {
        get
        {
            return quantity;
        }
        set
        {
            quantity = value;
        }
    }
    public string RequisitionId
    {
        get
        {
            return reqId;
        }
        set
        {
            reqId = value;
        }
    }
}