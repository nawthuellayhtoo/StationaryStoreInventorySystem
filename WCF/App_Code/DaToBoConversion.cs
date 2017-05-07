using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DaToBoConversion
/// </summary>
public class DaToBoConversion
{
  
    DisbursementBO dbbo;
    DepartmentBO dbo;

   

    public Disbursement convertDBoToD(DisbursementBO dbo)
    {
        Disbursement d = new Disbursement();
        d.DepartmentID = dbo.DepartmentId;
        d.DisbursementID = dbo.DisbursementId;
        d.DisbursementQuantity = dbo.DisbursementQuantity;
        d.ItemNumber = dbo.ItemNumber;
        d.OrderQuantity = dbo.OrderQuantity;
        d.RequisitionID = dbo.RequisitionId;
        d.Status = dbo.Status;

        return d;
    }

    public DepartmentBO convertDeptBO(Department d)
    {
        dbo = new DepartmentBO();
        dbo.DeptId = d.DepartmentID;
        dbo.DeptName = d.DepartmentName;
        dbo.DeptRep = d.RepID;
        dbo.DeptCollectionPoint = d.CollectionPointID;
        return dbo;
    }

}
