using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OutstandingInfoBO
/// </summary>
public class OutstandingInfoBO
{
    private string outstandingId;
    private string itemNumber;
    private string departmentId;
    private int? quantity;
    private InventoryStockBO item;
    private DepartmentBO department;
    private string status;

    public DepartmentBO Department
    {
        get { return department; }
        set { department = value; }
    }

    public InventoryStockBO Item
    {
        get { return item; }
        set { item = value; }
    }

    public string OutstandingId
    {
        get
        {
            return outstandingId;
        }

        set
        {
            outstandingId = value;
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

    public string DepartmentId
    {
        get
        {
            return departmentId;
        }

        set
        {
            departmentId = value;
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

    public string Status
    {
        get
        {
            return status;
        }

        set
        {
            status = value;
        }
    }
}