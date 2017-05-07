using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PurchaseOrderOp
/// </summary>
public class PurchaseOrderOp
{
    static SA43Team2StoreDBEntities m = new SA43Team2StoreDBEntities();
    public PurchaseOrderOp()
    {
       
    }

    public static List<WCFPOList> getPOList()
    {
        List<WCFPOList> POList = new List<WCFPOList>();
        POList = (from p in m.PurchaseOrders
                  join e in m.Employees on p.RequestorID equals e.EmpID
                  where p.Status.Equals("Pending")
                  select new WCFPOList
                  {
                      PONumber = p.POID,
                      GeneratedBy = e.EmpName,
                      CommentByClerk=p.CommentByClerk
                  }).ToList();
        return POList;
    }

    public static List<WCFPODetails> getPODetails(string POID)
    {
        List<WCFPODetails> PODetailsList = new List<WCFPODetails>();
        PODetailsList = (from pd in m.PurchaseOrderDetails
                         join i in m.InventoryStocks on pd.ItemNumber equals i.ItemNumber
                         join p in m.PurchaseOrders on pd.POID equals p.POID
                         where pd.POID == POID
                         select new WCFPODetails
                         {
                             ItemName = i.ItemName,
                             Quantity = pd.Quantity
                         }).ToList();
        return PODetailsList;
    }

    public static string updatePO(PurchaseOrder po)
    {
        var q = m.PurchaseOrders.Where(x => x.POID == po.POID).FirstOrDefault();
        PurchaseOrder p = (PurchaseOrder)q;
        q.ApprovalDate = DateTime.Today;
        q.Status = po.Status;
        q.CommentsBySupervisor = po.CommentsBySupervisor;
        int updateStatus = m.SaveChanges();
        if (updateStatus > 0)
            return "Success";
        return "Fail";
    }
}