using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess.StoreDA
{
    public class AdjustmentVoucherListDetailsDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        public List<AdjustmentBO> getAdjustmentVoucherById(string vouId)
        {
            List<AdjustmentBO> i = new List<AdjustmentBO>();
            try
            {
                var query = (from x in context.Adjustments
                             join y in context.Employees on x.EmpID equals y.EmpID
                             orderby x.AdjustmentStatus descending
                             where x.VoucherID.Equals(vouId)
                             select new { x.VoucherID, x.EmpID, y.EmpName, x.VoucherDate, x.TotalPrice, x.AdjustmentStatus }).ToList();
                foreach (var q in query)
                {
                    AdjustmentBO b = new AdjustmentBO();
                    b.VoucherId = q.VoucherID;
                    b.EmployeeId = q.EmpID;
                    b.Employee = new EmployeeBO();
                    b.Employee.EmployeeName = q.EmpName;
                    b.VoucherDate = q.VoucherDate;
                    b.TotalPrice = (double)q.TotalPrice;
                    b.AdjustmentStatus = q.AdjustmentStatus;
                    i.Add(b);
                }
            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x);
            }
            return i;
        }

        public List<AdjustmentDetailsBO> getAdjustmentVoucherListDetails(string vouId)
        {
            List<AdjustmentDetailsBO> i = new List<AdjustmentDetailsBO>();
            try
            {
                var query = (from x in context.AdjustmentDetails
                             join y in context.InventoryStocks on x.ItemNumber equals y.ItemNumber
                             join z in context.Adjustments on x.VoucherID equals z.VoucherID
                             where x.VoucherID.Equals(vouId) && z.VoucherID.Equals(vouId)
                             select new { x.VoucherID, x.ItemNumber, y.ItemName, x.QuantityAdjustment, x.Reason }).ToList();
                foreach (var q in query)
                {
                    AdjustmentDetailsBO b = new AdjustmentDetailsBO();
                    b.VoucherId = q.VoucherID;
                    b.ItemNumber = q.ItemNumber;
                    b.Item = new InventoryStockBO();
                    b.Item.ItemName = q.ItemName;
                    b.QuantityAdjustment = q.QuantityAdjustment;
                    b.Reason = q.Reason;
                    i.Add(b);
                }
            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x);
            }
            return i;
        }
        public string getApplier(string empID) // for email
        {
            string name = String.Empty;
            var query = (from x in context.Adjustments
                         join y in context.Employees on x.EmpID equals y.EmpID
                         where x.EmpID.Equals(empID) && x.EmpID.Equals(y.EmpID)
                         select new { y.EmpName }).Distinct().First();
            name = query.EmpName;
            return name;
        }
        public EmployeeBO getApproverSup(string title) // for email
        {
            var query = (from x in context.Employees
                         where x.EmpTitle.Equals(title)
                         select new { x.EmpName, x.Email }).Distinct().First();
            EmployeeBO ebo = new EmployeeBO();
            ebo.EmployeeName = query.EmpName;
            ebo.EmployeeEmail = query.Email;
            return ebo;
        }

        public EmployeeBO getApproverMan(string title) // for email
        {
            var query = (from x in context.Employees
                         where x.EmpTitle.Equals(title)
                         select new { x.EmpName, x.Email }).Distinct().First();
            EmployeeBO ebo = new EmployeeBO();
            ebo.EmployeeName = query.EmpName;
            ebo.EmployeeEmail = query.Email;
            return ebo;
        }

        public string checkSupApproval(string voId) //check to enable button
        {
            string i = "No";
            var query = (from x in context.Adjustments
                         where x.VoucherID.Equals(voId)
                         select new { x.AuthorisedBySupervisor }).Distinct().First();
            i = query.AuthorisedBySupervisor;
            return i;
        }
        public Boolean updateStatusMan(string voId)
        {
            bool status = false;
            Adjustment vo = context.Adjustments.Where(x => x.VoucherID == voId).FirstOrDefault();
            vo.AdjustmentStatus = "Approved";
            vo.AuthorisedByManager = "Yes";

            if (context.SaveChanges() > 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }
        public Boolean updateStatusSup(string voId, string adjstatus)
        {
            bool status = false;
            Adjustment vo = context.Adjustments.Where(x => x.VoucherID == voId).FirstOrDefault();
            vo.AdjustmentStatus = adjstatus;
            vo.AuthorisedBySupervisor = "Yes";

            if (context.SaveChanges() > 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }
        public EmployeeBO checkDelegate(string title)
        {
            EmployeeBO ebo = new EmployeeBO();
            var query = (from x in context.Employees
                         where x.EmpTitle.Equals(title)
                         select new { x.Delegate, x.DelegateStartDate, x.DelegateEndDate }).Distinct().First();
            ebo.Delegate = query.Delegate;
            ebo.DelegateStartDate = query.DelegateStartDate.GetValueOrDefault();
            ebo.DelegateEndDate = query.DelegateEndDate.GetValueOrDefault();

            return ebo;
        }
    }
}
