using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RequisitionOp
/// </summary>
public class RequisitionOp
{
    static SA43Team2StoreDBEntities m = new SA43Team2StoreDBEntities();
    public RequisitionOp()
    {
        
    }
    public static List<WCFRequisition> getRequisition(string deptId)
    {
        List<WCFRequisition> l = new List<WCFRequisition>();
        WCFRequisition e;


        List<Employee> e1 = (from x in m.Employees
                             where x.DepartmentID.Equals(deptId)
                             select x).ToList();

        List<Requisition> req = (from x in m.Requisitions
                                 where x.Employee.DepartmentID.Equals(deptId)
                                && x.Status.Equals("Pending")
                                 select x).ToList();

        foreach (Requisition t in req)
        {
            e = new WCFRequisition();
            e.RequisitionId = t.RequisitionID;
            e.EmpName = t.Employee.EmpName;
            e.CommentsEmp = t.CommentsByEmp;


            l.Add(e);


        }
        return l;

    }


    public static List<WCFRequisitionDetail> getRequisitionDetail(string reqId)
    {
     
        List<WCFRequisitionDetail> l = new List<WCFRequisitionDetail>();
        WCFRequisitionDetail e;


        List<RequisitionDetail> r1 = (from x in m.RequisitionDetails
                                      where x.RequisitionID.Equals(reqId)
                                      select x).ToList();

        foreach(RequisitionDetail r in r1)
        {
            try
            {
                e = new WCFRequisitionDetail();

                InventoryStock i = (from x in m.InventoryStocks
                                    where x.ItemNumber.Equals(r.ItemNumber)
                                    select x).First();
                e.ItemName = i.ItemName;
                e.ItemUOM = i.ItemUOM;
                e.Quantity =Convert.ToInt32( r.ItemQuantity);

                l.Add(e);
            }
            catch
            {
                continue;
            }
        }
        return l;

    }

    public static string ApproveRequisition(string comment,string reqId)
    {
        string update = "false";

        Requisition r = (from x in m.Requisitions
                         where x.RequisitionID.Equals(reqId)
                         select x).First();

        r.Status = "Approved";
        r.CommentsByHead = comment;
        r.ApprovalDate = DateTime.Now;

        if (m.SaveChanges() > 0)
        {
            update = "true";
        }
        else
        {
            update = "false";
        }

        return update;
    }


    public static string RejectRequisition(string comment, string reqId)
    {
        string update = "false";

        Requisition r = (from x in m.Requisitions
                         where x.RequisitionID.Equals(reqId)
                         select x).First();

        r.Status = "Rejected";
        r.CommentsByHead = comment;
        r.ApprovalDate = DateTime.Now;

        if (m.SaveChanges() > 0)
        {
            update = "true";
        }
        else
        {
            update = "false";
        }

        return update;
    }
}