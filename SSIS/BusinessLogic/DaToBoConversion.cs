using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class DaToBoConversion
    {
        EmployeeBO ebo;
        DepartmentBO dbo;
        DisbursementBO dbbo;
        LoginDA da = new LoginDA();

        public DepartmentBO convertDeptBO(Department d)
        {
            dbo = new DepartmentBO();
            dbo.DeptId = d.DepartmentID;
            dbo.DeptName = d.DepartmentName;
            dbo.DeptRep = d.RepID;
            dbo.DeptCollectionPoint = d.CollectionPointID;
            return dbo;
        }

        public EmployeeBO convertEmployeeBO(Employee e)
        {
            ebo = new EmployeeBO();
            ebo.EmployeeId = e.EmpID;
            ebo.EmployeeDept = e.DepartmentID;
            ebo.Department = getDeptByDeptId(ebo.EmployeeDept);
            ebo.EmployeeTitle = e.EmpTitle;
            ebo.EmployeeName = e.EmpName;
            ebo.EmployeeEmail = e.Email;
            ebo.Delegate = e.Delegate;
            ebo.DelegateStartDate = (DateTime)e.DelegateStartDate;
            ebo.DelegateEndDate = (DateTime)e.DelegateEndDate;
            ebo.Password = e.Password;
            return ebo;
        }
        public DepartmentBO getDeptByDeptId(String deptId)
        {
            Department dept = da.getDepartmentById(deptId);
            DepartmentBO dbo = convertDeptBO(dept);
            return dbo;
        }

        public DisbursementBO convertDisbursement(Disbursement d)
        {
            dbbo = new DisbursementBO();
            dbbo.DisbursementId = d.DisbursementID;
            dbbo.DepartmentId = d.DepartmentID;
            dbbo.Department = getDeptByDeptId(dbbo.DepartmentId);
            dbbo.DisbursementQuantity = d.DisbursementQuantity;
            dbbo.ItemNumber = d.ItemNumber;
            dbbo.OrderQuantity = d.OrderQuantity;
            dbbo.RequisitionId = d.RequisitionID;
            dbbo.Status = d.Status;
            return dbbo;
        }

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
    }
}
