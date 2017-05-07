using DataAccess;
using DataAccess.DepartmentDA;
using Model;
using System;
using System.Collections.Generic;

namespace BusinessLogic.DepartmentBL
{
    public class AssignDepartmentRepresentativeBL
    {
        AssignDepartmentRepresentativeDA da = new AssignDepartmentRepresentativeDA();
        EmployeeBO ebo;
        DepartmentBO dbo;

        public int assignDepartmentRepresentative(string empId)
        {
            da.assignDepRep(empId);
            return 0;
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

        public EmployeeBO getDepartmentRep(string deptId)
        {
            Employee e = da.getDeptRep(deptId);
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