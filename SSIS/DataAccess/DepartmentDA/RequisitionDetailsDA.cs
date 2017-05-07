
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess.DepartmentDA
{
    public class RequisitionDetailsDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();
        public List<RetrievalRequisitionDetailsBO> getRequisitionDetailsByReqId(string reqId)
        {
            List<RetrievalRequisitionDetailsBO> reqdetailslst = new List<RetrievalRequisitionDetailsBO>();
            reqdetailslst = (from r in context.RequisitionDetails
                             join i in context.InventoryStocks on r.ItemNumber equals i.ItemNumber
                             where r.RequisitionID == reqId
                             select new RetrievalRequisitionDetailsBO
                             {
                                 RetrievalItemName = i.ItemName,
                                 RetrievalQuantity = r.ItemQuantity,
                                 RetrievalUOM = i.ItemUOM
                             }).ToList();
            return reqdetailslst;
        }

        public string getEmpComments(string reqId)
        {
            string empComments = "";
            var q = (from r in context.Requisitions
                     where r.RequisitionID == reqId
                     select r.CommentsByEmp).FirstOrDefault();
            empComments = Convert.ToString(q);
            return empComments;
        }

        public void updateRequisitionStatus(string requisitionId, string status)
        {
            Requisition r = context.Requisitions.Where(x => x.RequisitionID == requisitionId).FirstOrDefault();
            r.Status = status;
            if (status == "Approved")
            {
                r.ApprovalDate = DateTime.Today;
            }
            context.SaveChanges();
        }
        public RetrievalRequisitionListBO getReqInfo(string reqId)
        {
            var q = (from r in context.Requisitions
                     where r.RequisitionID == reqId
                     select new RetrievalRequisitionListBO
                     {
                         RetrievalempName = r.Employee.EmpName,
                         RetrievalempRequisitionDate = r.EmpRequisitionDate,
                         Retrievalstatus = r.Status
                     }).FirstOrDefault();
            return q;
        }

        public void resubmitRequisition(string requisitionId, string comment)
        {
            Requisition r = context.Requisitions.Where(x => x.RequisitionID == requisitionId).FirstOrDefault();
            r.CommentsByEmp = comment;
            r.Status = "Pending";
            r.EmpRequisitionDate = DateTime.Today;
            context.SaveChanges();
        }

        public void resubmitRequisitionDetails(string requisitionDetailsId, int quantity)
        {
            RequisitionDetail rd = context.RequisitionDetails.Where(x => x.RequisitionDetailsID == requisitionDetailsId).FirstOrDefault();
            rd.ItemQuantity = quantity;
            context.SaveChanges();
        }

        public string getRequisitionDetailsIdByItemNumber(string requisitionId, string itemNumber)
        {
            string requisitionDetailsId = "";
            var q = (from rd in context.RequisitionDetails
                     where rd.ItemNumber == itemNumber && rd.RequisitionID == requisitionId
                     select rd.RequisitionDetailsID).FirstOrDefault();
            requisitionDetailsId = Convert.ToString(q);
            return requisitionDetailsId;
        }



        public void deleteRequisition(string requisitionId)
        {
            RequisitionDetail rd = context.RequisitionDetails.Where(x => x.RequisitionID == requisitionId).FirstOrDefault();
            context.RequisitionDetails.Remove(rd);
            context.SaveChanges();

            Requisition r = context.Requisitions.Where(x => x.RequisitionID == requisitionId).FirstOrDefault();
            context.Requisitions.Remove(r);
            context.SaveChanges();
        }
    }
}
