using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess.StoreDA
{
    public class CreateAdjustmentVoucherDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        public List<string> getDistinctCategory()
        {
            List<string> i = new List<string>();

            var query = (from x in context.InventoryStocks
                         select new { x.ItemCategory }).Distinct().ToList();
            foreach (var q in query)
            {
                InventoryStockBO b = new InventoryStockBO();
                b.ItemCategory = q.ItemCategory;
                i.Add(b.ItemCategory);
            }
            return i;
        }

        public List<InventoryStockBO> getItemListByCategory(String category)
        {
            List<InventoryStockBO> i = new List<InventoryStockBO>();

            var query = (from x in context.InventoryStocks
                         where x.ItemCategory.Equals(category)
                         select new { x.ItemNumber, x.ItemName }).Distinct().ToList();

            foreach (var q in query)
            {
                InventoryStockBO b = new InventoryStockBO();
                b.ItemNumber = q.ItemNumber;
                b.ItemName = q.ItemName;
                i.Add(b);
            }
            return i;
        }

        public string getUOM(String itemNumber)
        {
            string i = String.Empty;
            var query = (from x in context.InventoryStocks
                         where x.ItemCategory.Equals(itemNumber)
                         select new { x.ItemUOM }).Distinct().ToList();

            foreach (var q in query)
            {
                i = q.ItemUOM;
            }
            return i;
        }

        public List<InventoryStockBO> getSupplierListFromItemNumber(String itemNumber)
        {
            List<InventoryStockBO> ilist = new List<InventoryStockBO>();
            InventoryStockBO i = new InventoryStockBO();
            var query = (from x in context.InventoryStocks
                         where x.ItemNumber.Equals(itemNumber)
                         select new { x.Supplier1, x.Supplier2, x.Supplier3, x.Price1, x.Price2, x.Price3 }).ToList();

            foreach (var q in query)
            {
                i.Supplier1 = q.Supplier1;
                i.Supplier2 = q.Supplier2;
                i.Supplier3 = q.Supplier3;
                i.Price1 = Convert.ToDouble(q.Price1);
                i.Price2 = Convert.ToDouble(q.Price2);
                i.Price3 = Convert.ToDouble(q.Price3);
                ilist.Add(i);
            }
            return ilist;
        }

        public string getAdjustmentVoucherId()
        {
            string i = String.Empty;
            var query = (from x in context.Adjustments
                         orderby x.VoucherID descending
                         select new { x.VoucherID }).First();
            i = query.VoucherID;
            return i;
        }
        public string getApplier(string empID)
        {
            string name = String.Empty;
            var query = (from x in context.Adjustments
                         join y in context.Employees on x.EmpID equals y.EmpID
                         where x.EmpID.Equals(empID) && x.EmpID.Equals(y.EmpID)
                         select new { y.EmpName }).Distinct().First();
            name = query.EmpName;
            return name;
        }
        public EmployeeBO getApprover(string title)
        {
            var query = (from x in context.Employees
                         where x.EmpTitle.Equals(title)
                         select new { x.EmpName, x.Email }).Distinct().First();
            EmployeeBO ebo = new EmployeeBO();
            ebo.EmployeeName = query.EmpName;
            ebo.EmployeeEmail = query.Email;
            return ebo;
        }
        public Boolean createAdjustmentVoucher(string vID, string empID, double totalPrice)
        {
            bool status = false;
            SA43Team2StoreDBEntities ctx = new SA43Team2StoreDBEntities();
            Adjustment abo = new Adjustment();
            abo.VoucherID = vID;
            abo.EmpID = empID;
            abo.AdjustmentStatus = "Pending";
            abo.VoucherDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            abo.AuthorisedBySupervisor = "No";
            abo.AuthorisedByManager = "No";
            abo.TotalPrice = Convert.ToDecimal(totalPrice);
            ctx.Adjustments.Add(abo);

            if (ctx.SaveChanges() > 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }
        public void createAdjustmentVoucherDetails(string adNum, string vID, string itemNum, string qty, string supp, string reason)
        {
            SA43Team2StoreDBEntities ctx = new SA43Team2StoreDBEntities();
            AdjustmentDetail ad = new AdjustmentDetail();
            ad.AdjustmentDetailsID = adNum;
            ad.VoucherID = vID;
            ad.ItemNumber = itemNum;
            ad.QuantityAdjustment = Convert.ToInt32(qty);
            ad.SupplierID = supp;
            ad.Reason = reason;
            ctx.AdjustmentDetails.Add(ad);

            ctx.SaveChanges();
        }

        public string getAdjustmentDetailsVoucherId()
        {
            string i = String.Empty;
            var query = (from x in context.AdjustmentDetails
                         orderby x.AdjustmentDetailsID descending
                         select new { x.AdjustmentDetailsID }).First();
            i = query.AdjustmentDetailsID;
            return i;
        }
    }
}
