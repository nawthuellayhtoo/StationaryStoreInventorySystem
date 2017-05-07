using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InventoryStockBO
/// </summary>
public class InventoryStockBO
{
    private string itemNumber;
    private string itemName;
    private int? quantity;
    private string itemCategory;
    private int? reorderLevel;
    private int? reorderQuantity;
    private string itemUOM;
    private string bin;
    private string supplier1;
    private string supplier2;
    private string supplier3;
    private double? price1;
    private double? price2;
    private double? price3;
    private SupplierBO supplierOne;
    private SupplierBO supplierTwo;
    private SupplierBO supplierThree;

    public SupplierBO SupplierThree
    {
        get { return supplierThree; }
        set { supplierThree = value; }
    }

    public SupplierBO SupplierTwo
    {
        get { return supplierTwo; }
        set { supplierTwo = value; }
    }

    public SupplierBO SupplierOne
    {
        get { return supplierOne; }
        set { supplierOne = value; }
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

    public int? Quantity
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

    public string ItemCategory
    {
        get
        {
            return itemCategory;
        }

        set
        {
            itemCategory = value;
        }
    }

    public int? ReorderLevel
    {
        get
        {
            return reorderLevel;
        }

        set
        {
            reorderLevel = value;
        }
    }

    public int? ReorderQuantity
    {
        get
        {
            return reorderQuantity;
        }

        set
        {
            reorderQuantity = value;
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

    public string Bin
    {
        get
        {
            return bin;
        }

        set
        {
            bin = value;
        }
    }

    public string Supplier1
    {
        get
        {
            return supplier1;
        }

        set
        {
            supplier1 = value;
        }
    }

    public string Supplier2
    {
        get
        {
            return supplier2;
        }

        set
        {
            supplier2 = value;
        }
    }

    public string Supplier3
    {
        get
        {
            return supplier3;
        }

        set
        {
            supplier3 = value;
        }
    }

    public double? Price1
    {
        get
        {
            return price1;
        }

        set
        {
            price1 = value;
        }
    }

    public double? Price2
    {
        get
        {
            return price2;
        }

        set
        {
            price2 = value;
        }
    }

    public double? Price3
    {
        get
        {
            return price3;
        }

        set
        {
            price3 = value;
        }
    }
}