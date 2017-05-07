using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess.StoreDA
{
    public class AdjustmentVoucherListDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        public List<AdjustmentBO> getAdjustmentVoucherListByClerk(string empId)
        {
            List<AdjustmentBO> i = new List<AdjustmentBO>();
            try
            {
                var query = (from x in context.Adjustments
                             join y in context.Employees on x.EmpID equals y.EmpID
                             orderby x.AdjustmentStatus descending, x.VoucherDate descending
                             where x.EmpID.Equals(empId)
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
        public List<AdjustmentBO> getAdjustmentVoucherListMan()
        {
            List<AdjustmentBO> i = new List<AdjustmentBO>();
            try
            {
                var query = (from x in context.Adjustments
                             join y in context.Employees on x.EmpID equals y.EmpID
                             orderby x.AdjustmentStatus descending, x.VoucherDate descending
                             where x.AuthorisedBySupervisor.Equals("Yes") && x.TotalPrice >= 250
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
        public List<AdjustmentBO> getAdjustmentVoucherListSup()
        {
            List<AdjustmentBO> i = new List<AdjustmentBO>();
            try
            {
                var query = (from x in context.Adjustments
                             join y in context.Employees on x.EmpID equals y.EmpID
                             orderby x.AdjustmentStatus descending, x.VoucherDate descending
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
