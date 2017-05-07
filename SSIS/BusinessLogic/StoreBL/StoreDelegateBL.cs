using DataAccess;
using DataAccess.StoreDA;
using Model;
using System;

namespace BusinessLogic.StoreBL
{
    public class StoreDelegateBL
    {
        StoreDelegateDA da = new StoreDelegateDA();
        EmployeeBO ebo;
        DepartmentBO dbo;

        public EmployeeBO convertEmployeeBO(Employee e)
        {
            ebo = new EmployeeBO();
            ebo.EmployeeId = e.EmpID;
            ebo.EmployeeDept = e.DepartmentID;
            ebo.Department = getDeptByEmpDeptId(ebo.EmployeeDept);
            ebo.EmployeeTitle = e.EmpTitle;
            ebo.EmployeeName = e.EmpName;
            ebo.EmployeeEmail = e.Email;
            ebo.Delegate = e.Delegate;
            ebo.DelegateStartDate = (DateTime)e.DelegateStartDate;
            ebo.DelegateEndDate = (DateTime)e.DelegateEndDate;
            ebo.Password = e.Password;
            return ebo;
        }

        public DepartmentBO convertDeptBO(Department d)
        {
            dbo = new DepartmentBO();
            dbo.DeptId = d.DepartmentID;
            dbo.DeptName = d.DepartmentName;
            dbo.DeptRep = d.RepID;
            dbo.DeptCollectionPoint = d.CollectionPointID;
            return dbo;
        }

        public EmployeeBO getStoreManagerBO()
        {
            Employee sm = da.getStoreManager();
            EmployeeBO smbo = convertEmployeeBO(sm);
            return smbo;
        }

        public EmployeeBO getStoreSupervisorBO()
        {
            Employee ss = da.getStoreSupervisor();
            EmployeeBO ssbo = convertEmployeeBO(ss);
            return ssbo;
        }

        public DepartmentBO getDeptByEmpDeptId(string deptId)
        {
            Department dept = da.getDepartmentById(deptId);
            DepartmentBO dbo = convertDeptBO(dept);
            return dbo;
        }

        public int activate(string empTitle, DateTime delegateStart, DateTime delegateEnd)
        {
            da.activateDelegate(empTitle, delegateStart, delegateEnd);
            return 0;
        }

        public int deactivate(string empTitle)
        {
            da.deactivateDelegate(empTitle);
            return 0;
        }
    }
}