using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SupplierBO
/// </summary>
public class SupplierBO
{
    private string supplierId;
    private string supplierName;
    private string contactName;
    private int? phoneNumber;
    private string address;
    private int? fax;
    private string email;
    private string gstRegistrationNumber;

    public string SupplierId
    {
        get
        {
            return supplierId;
        }

        set
        {
            supplierId = value;
        }
    }

    public string SupplierName
    {
        get
        {
            return supplierName;
        }

        set
        {
            supplierName = value;
        }
    }

    public string ContactName
    {
        get
        {
            return contactName;
        }

        set
        {
            contactName = value;
        }
    }

    public int? PhoneNumber
    {
        get
        {
            return phoneNumber;
        }

        set
        {
            phoneNumber = value;
        }
    }

    public string Address
    {
        get
        {
            return address;
        }

        set
        {
            address = value;
        }
    }

    public int? Fax
    {
        get
        {
            return fax;
        }

        set
        {
            fax = value;
        }
    }

    public string Email
    {
        get
        {
            return email;
        }

        set
        {
            email = value;
        }
    }

    public string GstRegistrationNumber
    {
        get
        {
            return gstRegistrationNumber;
        }

        set
        {
            gstRegistrationNumber = value;
        }
    }
}