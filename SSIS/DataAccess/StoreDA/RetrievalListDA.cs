using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess.StoreDA
{
    public class RetrievalListDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();
        public List<string> getRequitionIdList() //To gt requisition order lst with need to be retrieved
        {

            var qry = from r in context.Requisitions
                      where (r.Status == "Approved")
                      select r.RequisitionID;

            List<string> sLst = qry.ToList();
            return sLst;

        }
        public List<string> getItemIdListByReqId(string reqId) //To get the item list with orderes to be retrived
        {
            var qry = from i in context.InventoryStocks
                      join d in context.RequisitionDetails on i.ItemNumber equals d.ItemNumber
                      join e in context.Employees on d.RequisitionID equals reqId
                      join r in context.Requisitions on e.EmpID equals r.EmpID
                      join dep in context.Departments on e.DepartmentID equals dep.DepartmentID
                      select i.ItemNumber;

            List<string> sLst = qry.ToList();

            return sLst;
        }
        public string getItemNameByItemNumber(string itemNumber) //to get item Name
        {
            InventoryStock i = context.InventoryStocks.Where(x => x.ItemNumber == itemNumber).FirstOrDefault();
            return i.ItemName;
        }

        public int getStockQtyByItemName(string itemName) //To get the qty in the stock
        {
            InventoryStock i = context.InventoryStocks.Where(x => x.ItemName == itemName).FirstOrDefault();
            return (int)i.Quantity;
        }

        public int getReorderLevelByItemNumber(string itemName) //Not used
        {
            InventoryStock i = context.InventoryStocks.Where(x => x.ItemName == itemName).FirstOrDefault();
            return (int)i.ReorderLevel;
        }
        public List<string> getItemNameByReqId(List<string> reqIdList) //To get itemNameList with requisitions
        {
            List<string> slLst = new List<string>();
            foreach (string reqId in reqIdList)
            {
                var qry = from i in context.InventoryStocks
                          join d in context.RequisitionDetails on i.ItemNumber equals d.ItemNumber
                          join e in context.Employees on d.RequisitionID equals reqId
                          join r in context.Requisitions on e.EmpID equals r.EmpID
                          join dep in context.Departments on e.DepartmentID equals dep.DepartmentID
                          select i.ItemName;

                List<string> sLst = (List<string>)qry.ToList();
                foreach (String s in sLst)
                {
                    slLst.Add(s);
                }
            }
            return slLst.Distinct().ToList();
        }

        public string getItemNumberByItemName(string itemName)    //To get item number
        {
            InventoryStock i = context.InventoryStocks.Where(x => x.ItemName.Equals(itemName)).FirstOrDefault();
            return i.ItemNumber;
        }

        public string getBinNumberByItemNumber(string itemNumber) //To get bin number
        {
            InventoryStock i = context.InventoryStocks.Where(x => x.ItemNumber == itemNumber).FirstOrDefault();
            return i.Bin;
        }
        public List<string> getDepartmentListByItemNumber(string itemNumber) //To get Dept list
        {
            var qry = (from rd in context.RequisitionDetails
                       join r in context.Requisitions on rd.RequisitionID equals r.RequisitionID
                       join e in context.Employees on r.EmpID equals e.EmpID
                       join d in context.Departments on e.DepartmentID equals d.DepartmentID
                       where (rd.ItemNumber == itemNumber && r.Status == "Approved")
                       select d.DepartmentID).Distinct();

            List<string> dLst = (List<string>)qry.ToList();
            return dLst;
        }
        public List<string> getDepartmentListWithOutstanding(string itemNumber) //To get department with oustanding to be addressed
        {
            var qry = (from o in context.OutstandingInfoes
                       where (o.Quantity != 0 && o.ItemNumber == itemNumber && o.Status != "Received")
                       select o.DepartmentID).Distinct();

            List<string> dLst = qry.ToList();
            return dLst;
        }

        public List<string> getItemNumberListWithOutstanding() //To get item numbers with outstanding to be addressed
        {
            var qry = from o in context.OutstandingInfoes
                      where (o.Quantity != 0 && o.Status != "Received")
                      select o.ItemNumber;

            List<string> dLst = qry.ToList();
            return dLst;
        }
        public int getOutstandingByDepAndItem(string depId, string itemNumber) //get outstanding qty 
        {
            OutstandingInfo o = context.OutstandingInfoes.Where(x => x.DepartmentID.Equals(depId) && x.ItemNumber.Equals(itemNumber) && x.Status != "Received").FirstOrDefault();
            if (o != null)
            {
                if (o.Status == "Pending")
                {
                    return (int)o.Quantity;
                }
                else
                {
                    return (int)o.PartialPendingQty;
                }
            }
            else
            {
                return 0;
            }

        }

        public int getOrderedQtyByDepAndItem(string depId, string itemNumber) //To get ordered qty
        {
            var qry = from rd in context.RequisitionDetails
                      join r in context.Requisitions on rd.RequisitionID equals r.RequisitionID
                      join e in context.Employees on r.EmpID equals e.EmpID
                      join dep in context.Departments on e.DepartmentID equals dep.DepartmentID
                      where (rd.ItemNumber == itemNumber && r.Status == "approved" && dep.DepartmentID == depId)
                      select rd;

            List<RequisitionDetail> dLst = qry.ToList();
            int qty = 0;
            foreach (RequisitionDetail d in dLst)
            {
                if (d != null)
                {
                    qty = qty + (int)d.ItemQuantity;

                }
                else
                {
                    qty = qty + 0;
                }
            }
            return qty;
        }

        public string getLastDisbursementId() //To generate new disbursement Id
        {
            var qry = from d in context.Disbursements
                      select d.DisbursementID;

            List<string> finalLst = qry.ToList();
            List<int> numberLst = new List<int>();
            string b = "";
            foreach (string s in finalLst)
            {
                string a = s.Substring(2);
                b = s.Substring(0, 2);
                int n = Convert.ToInt32(a);
                numberLst.Add(n);
            }
            List<int> ascendingLst = numberLst.OrderBy(i => i).ToList();


            int lastNumber = ascendingLst.Last();
            string lastId = b + lastNumber.ToString();
            return lastId;
        }


        public string getEmailIdByDepId(string depId)   //To send email notification
        {
            string qry = (from d in context.Departments
                          join e in context.Employees on d.RepID equals e.EmpID
                          where (d.DepartmentID == depId)
                          select e.Email).FirstOrDefault();

            return qry;

        }
        public string getRepNameByDepId(string depId) //To get the Rep Id to send email
        {
            string qry = (from d in context.Departments
                          join e in context.Employees on d.RepID equals e.EmpID
                          where (d.DepartmentID == depId)
                          select e.EmpName).FirstOrDefault();

            return qry;
        }
        public void updateReqStatus(string reqId) //Update req status
        {
            Requisition r = context.Requisitions.Where(x => x.RequisitionID == reqId).First();
            r.Status = "Received";
            context.SaveChanges();
        }

        public void updateInventoryStockQuanitity(string itemName, int retrievedQty) //Update inventory stock
        {
            string itemNumber = getItemNumberByItemName(itemName);
            InventoryStock i = context.InventoryStocks.Where(x => x.ItemNumber == itemNumber).First();
            int prevQty = (int)i.Quantity;
            i.Quantity = prevQty - retrievedQty;
            context.SaveChanges();
        }

        public void updateOutstandingStatus(string itemNumber, string depId) //Update outstanding status
        {
            OutstandingInfo o = context.OutstandingInfoes.Where(x => x.ItemNumber == itemNumber && x.DepartmentID == depId).FirstOrDefault();
            o.Status = "Received";
            context.SaveChanges();
        }

        public void updatePartialOutStanding(string itemNumber, string depId, int partialOutstanding) //Update status for outstanding fulfiled partially
        {
            OutstandingInfo o = context.OutstandingInfoes.Where(x => x.ItemNumber == itemNumber && x.DepartmentID == depId).FirstOrDefault();
            o.Status = "Partial Received";
            o.PartialPendingQty = partialOutstanding;
            context.SaveChanges();
        }
        public void createNewDisbursement(Disbursement d) //To create new disbursement
        {

            context.Disbursements.Add(d);
            context.SaveChanges();
        }

        public string getDepIdByReqId(string reqId) //To get dep id 
        {
            var qry = from r in context.Requisitions
                      join e in context.Employees on r.RequisitionID equals reqId
                      where (r.EmpID == e.EmpID)
                      select e.DepartmentID;

            string depId = qry.ToList().FirstOrDefault().ToString();

            return depId;
        }
        public string getDepIdByDepName(string depName) //to get dep Id
        {
            Department d = context.Departments.Where(x => x.DepartmentName == depName).First();
            return d.DepartmentID;
        }
        public List<string> getRequisitionIdByDepAndItem(string depId, string itemNumber)  //To get req Id by particular dep n item
        {

            var qry = from r in context.Requisitions
                      join rd in context.RequisitionDetails on r.RequisitionID equals rd.RequisitionID
                      join e in context.Employees on r.EmpID equals e.EmpID
                      where (r.Status != "Pending" && e.DepartmentID == depId && rd.ItemNumber == itemNumber)
                      select r.RequisitionID;

            List<string> rLst = qry.ToList();
            return rLst;
        }

        public int getOrderQuantityByReqIdAndItemNumber(string reqId, string itemNumber) //To get orded qty
        {
            RequisitionDetail rd = context.RequisitionDetails.Where(x => x.RequisitionID == reqId && x.ItemNumber == itemNumber).FirstOrDefault();
            return (int)rd.ItemQuantity;
        }
    }
}
