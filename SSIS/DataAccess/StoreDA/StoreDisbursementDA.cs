using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess.StoreDA
{
    public class StoreDisbursementDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        public List<Department> getDistinctDepList() //To get distinct departments
        {
            List<Department> dLst = context.Departments.Where(x => x.DepartmentName != "Stationery Store").Select(x => x).Distinct().ToList();
            return dLst;
        }

        public List<string> getDistinctDepartmentNameList()// to get distinct dep name list
        {

            List<string> dLst = context.Departments.Where(x => x.DepartmentName != "Stationery Store").Select(x => x.DepartmentName).Distinct().ToList();
            return dLst;

        }

        public string getDepIdByDepName(string depName)  //To get dep Id
        {
            Department d = context.Departments.Where(x => x.DepartmentName == depName).FirstOrDefault();
            return d.DepartmentID;

        }
        public Department getDepartmentByDepId(string depId) //To get department
        {
            Department d = context.Departments.Where(x => x.DepartmentID == depId).First();
            return d;
        }

        public Employee getEmployeeByEmpId(string repId) //To get rep details
        {
            Employee e = context.Employees.Where(x => x.EmpID == repId).FirstOrDefault();
            return e;
        }
        public string getDepRepByDepId(string depId)  //To get dep rep
        {
            Department d = getDepartmentByDepId(depId);
            Employee e = getEmployeeByEmpId(d.RepID);
            return e.EmpName;
        }

        public CollectionPointDetail getCollectionPointByDepId(string depId)  //To get collection point
        {
            Department d = getDepartmentByDepId(depId);
            string collectionPointId = d.CollectionPointID;
            CollectionPointDetail c = context.CollectionPointDetails.Where(x => x.CollectionPointID == collectionPointId).FirstOrDefault();
            return c;

        }
        public string getCollectionpoint(string depId)//To get collection point object
        {
            CollectionPointDetail cp = getCollectionPointByDepId(depId);
            return cp.CollectionPoint;
        }

        public string getCollectionTime(string depId)//To get collection time
        {
            CollectionPointDetail cp = getCollectionPointByDepId(depId);
            return cp.CollectionTime;

        }
        public List<DisbursementListBO> getDisursementListByDepId(string depId) //to get disbursement list
        {

            List<string> itemLst = getDistictItemList(depId);
            List<DisbursementListBO> dlboLst = new List<DisbursementListBO>();
            foreach (string s in itemLst)
            {
                List<Disbursement> disLst = getDisbursementListByDepAndItem(depId, s);
                List<OutstandingInfo> outstandingLst = getOutstandingListByDepAndItem(depId, s);
                DisbursementListBO dlo = new DisbursementListBO();
                dlo.ItemName = getItemNameByItemNumber(s);
                dlo.ItemUOM = getUOMByItemNumber(s);
                dlo.OrderQuantity = 0;
                dlo.OutstandingQuantity = 0;
                dlo.DisbursementQuantity = 0;
                foreach (Disbursement d in disLst)
                {
                    if (d != null)
                    {
                        dlo.OrderQuantity = dlo.OrderQuantity + d.OrderQuantity;
                        dlo.DisbursementQuantity = dlo.DisbursementQuantity + d.DisbursementQuantity;

                    }
                }

                foreach (OutstandingInfo o in outstandingLst)
                {
                    if (o != null)
                    {
                        if (o.Status == "Received")
                        {
                            dlo.OutstandingQuantity = dlo.OutstandingQuantity + o.Quantity;
                            dlo.DisbursementQuantity = dlo.DisbursementQuantity + o.Quantity;
                        }
                        else if (o.Status == "Partial Received")
                        {
                            dlo.OutstandingQuantity = dlo.OutstandingQuantity + o.Quantity;
                            dlo.DisbursementQuantity = dlo.DisbursementQuantity + (o.Quantity - o.PartialPendingQty);
                        }
                    }
                }

                dlboLst.Add(dlo);
            }


            return dlboLst;

        }
        public string getUOMByItemNumber(string itemNumber)  //To get UOM
        {
            InventoryStock i = context.InventoryStocks.Where(x => x.ItemNumber == itemNumber).FirstOrDefault();
            return i.ItemUOM;
        }
        public List<Disbursement> getDisbursementListByDepAndItem(string depId, string itemNumber)//To get disbursement list
        {
            var qry = from x in context.Disbursements where x.Status == "Ready for collection" && x.DepartmentID == depId && x.ItemNumber == itemNumber select x;
            List<Disbursement> dLst = qry.ToList<Disbursement>();
            return dLst;
        }

        public List<string> getDisbursementItemListByDepId(string depId)  //Get disbursed items
        {
            var qry = from x in context.Disbursements where x.Status == "Ready for collection" && x.DepartmentID == depId select x;
            List<Disbursement> dLst = qry.ToList<Disbursement>();
            List<string> dlboLst = new List<string>();
            foreach (Disbursement d in dLst)
            {

                dlboLst.Add(d.InventoryStock.ItemNumber);
            }
            return dlboLst;
        }

        public List<OutstandingInfo> getOutstandingListByDepAndItem(string depId, string itemNumber) //To get outstanding qty
        {
            var qry = from x in context.OutstandingInfoes where x.DepartmentID == depId && x.Status != "pending" && x.ItemNumber == itemNumber select x;
            List<OutstandingInfo> oLst = qry.ToList();
            return oLst;

        }

        public List<string> getOutstandingItemListByDep(string depId) //To get outstanding item list
        {
            var qry = from x in context.OutstandingInfoes where x.DepartmentID == depId && x.Status != "pending" select x.ItemNumber;
            List<string> oLst = qry.ToList();
            return oLst;

        }

        public List<string> getDistictItemList(string depId) //To get distinct item list which needs to be retrieved (ordered + outstanding)
        {
            List<string> disbursedItemList = getDisbursementItemListByDepId(depId);
            List<string> outstandingItemList = getOutstandingItemListByDep(depId);

            foreach (string s in outstandingItemList)
            {
                disbursedItemList.Add(s);
            }

            List<string> finalLst = disbursedItemList.Distinct().ToList();
            return finalLst;
        }
        public string getItemNumberByItemName(string itemName)// To get item number
        {
            InventoryStock i = context.InventoryStocks.Where(x => x.ItemName.Equals(itemName)).FirstOrDefault();
            return i.ItemNumber;
        }

        public string getItemNameByItemNumber(string itemNumber) //To get item name
        {
            InventoryStock i = context.InventoryStocks.Where(x => x.ItemNumber.Equals(itemNumber)).FirstOrDefault();
            return i.ItemName;
        }
    }
}
