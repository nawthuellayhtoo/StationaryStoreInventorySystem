using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Model;

namespace DataAccess.DepartmentDA
{
    public class CreateRequisitionDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        public string getItemNumberByItemName(string itemName)
        {
            string itemNumber = "";
            var q = (from i in context.InventoryStocks
                     where i.ItemName == itemName
                     select i.ItemNumber).FirstOrDefault();
            itemNumber = Convert.ToString(q);
            return itemNumber;
        }

        public string getLastRequisitionId()
        {
            string requisitionId = "";
            var q = (from r in context.Requisitions
                     select r.RequisitionID).ToList();

            requisitionId = Convert.ToString(q.LastOrDefault());
            return requisitionId;
        }

        public string getLastRequisitionDetailsId()
        {
            string requisitionDetailsId = "";
            var q = (from r in context.RequisitionDetails
                     select r.RequisitionDetailsID).ToList();
            requisitionDetailsId = Convert.ToString(q.LastOrDefault());
            return requisitionDetailsId;
        }

        public void submitRequisition(Requisition requisition)
        {
            context.Requisitions.Add(requisition);
            context.SaveChanges();
        }

        public void submitRequisitionDetails(RequisitionDetail requisitionDetail)
        {
            context.RequisitionDetails.Add(requisitionDetail);
            context.SaveChanges();
        }

        public string getHeadNameByDepId(string depId)
        {
            string headName = "";
            var q = (from e in context.Employees
                     where e.DepartmentID == depId && e.EmpTitle == "head"
                     select e.EmpName).FirstOrDefault();
            headName = q.ToString();
            return headName;
        }

        public string getHeadEmailByDepId(string depId)
        {
            string headEmail = "";
            var q = (from e in context.Employees
                     where e.DepartmentID == depId && e.EmpTitle == "head"
                     select e.Email).FirstOrDefault();
            headEmail = q.ToString();
            return headEmail;
        }

        public int getReorderLevelByItemNumber(string itemNumber)
        {
            var q = (from i in context.InventoryStocks
                     where i.ItemNumber == itemNumber
                     select i.ReorderLevel).FirstOrDefault();
            int reorderLevel = Convert.ToInt32(q);
            return reorderLevel;
        }

        public int getInventoryQtyByItemNumber(string itemNumber)
        {
            var q = (from i in context.InventoryStocks
                     where i.ItemNumber == itemNumber
                     select i.Quantity).FirstOrDefault();
            int inventoryQty = Convert.ToInt32(q);
            return inventoryQty;
        }

        public List<string> getAllClerksEmail()
        {
            List<string> emailList = new List<string>();
            var q = (from e in context.Employees
                     where e.EmpTitle == "Clerk"
                     select e.Email).ToList();
            emailList = q;
            return emailList;
        }
    }
}
