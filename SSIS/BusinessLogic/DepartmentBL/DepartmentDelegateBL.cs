using DataAccess;
using DataAccess.DepartmentDA;
using Model;
using System;
using System.Collections.Generic;

namespace BusinessLogic.DepartmentBL
{
    public class DepartmentDelegateBL
    {
        DepartmentDelegateDA da = new DepartmentDelegateDA();
        EmployeeBO ebo;
        DepartmentBO dbo;

        public int activate(string deptId, string empId, DateTime delegateStart, DateTime delegateEnd)
        {
            da.activateDelegate(deptId, empId, delegateStart, delegateEnd);
            return 0;
        }

        public int deactivate(EmployeeBO head, EmployeeBO employee)
        {
            da.deactivateDelegate(head, employee);
            return 0;
        }

        public EmployeeBO getDelegatedEmployee(string deptId)
        {
            Employee e = da.getDelegatedEmp(deptId);
            ebo = new EmployeeBO();
            ebo = convertEmployeeBO(e);
            return ebo;
        }

        public List<EmployeeBO> getEmployeeListByDepartmentId(string deptId)
        {
            List<Employee> list = da.getEmpListByDeptId(deptId);
            List<EmployeeBO> listbo = new List<EmployeeBO>();

            foreach (Employee e in list)
            {
                EmployeeBO ebo = convertEmployeeBO(e);
                listbo.Add(ebo);
            }

            return listbo;
        }

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

        public DepartmentBO getDeptByEmpDeptId(string deptId)
        {
            Department dept = da.getDepartmentById(deptId);
            DepartmentBO dbo = convertDeptBO(dept);
            return dbo;
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
    }
}