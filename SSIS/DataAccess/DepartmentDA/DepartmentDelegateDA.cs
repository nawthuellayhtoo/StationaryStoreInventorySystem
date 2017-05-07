using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DepartmentDA
{
    public class DepartmentDelegateDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        public int activateDelegate(string deptId, string empId, DateTime delegateStart, DateTime delegateEnd)
        {
            Employee h = getHeadByDepId(deptId);
            Employee e = getEmployeeById(empId);
            h.Delegate = 1;
            h.DelegateStartDate = delegateStart;
            h.DelegateEndDate = delegateEnd;
            e.Delegate = 1;
            context.SaveChanges();
            return 0;
        }

        public int deactivateDelegate(EmployeeBO head, EmployeeBO employee)
        {
            EmployeeBO h = head;
            EmployeeBO e = employee;

            string hid = h.EmployeeId;
            string eid = e.EmployeeId;

            Employee H = context.Employees.Where(x => x.EmpID.Equals(hid)).FirstOrDefault();
            Employee E = context.Employees.Where(x => x.EmpID.Equals(eid)).FirstOrDefault();

            H.Delegate = 0;
            E.Delegate = 0;

            context.SaveChanges();
            return 0;
        }

        public Employee getDelegatedEmp(string deptId)
        {
            Employee e = new Employee();
            Department d = new Department();
            d = context.Departments.Where(x => x.DepartmentID.Equals(deptId)).FirstOrDefault();
            e = context.Employees.Where(x => x.DepartmentID.Equals(deptId)
            && x.Delegate.Equals(1)
            && x.EmpTitle.Equals("Employee")).FirstOrDefault();
            return e;
        }

        public Employee getEmployeeById(string userId)
        {
            Employee e = new Employee();
            e = context.Employees.Where(x => x.EmpID == userId).FirstOrDefault();
            return e;
        }

        public Employee getHeadByDepId(string deptId)
        {
            Employee e = new Employee();
            Department d = getDepartmentById(deptId);
            e = context.Employees.Where(x => x.DepartmentID == deptId && x.EmpTitle.Equals("Head") && x.DepartmentID == d.DepartmentID).FirstOrDefault();
            return e;
        }

        public Department getDepartmentById(string deptId)
        {
            Department d = new Department();
            d = context.Departments.Where(x => x.DepartmentID == deptId).FirstOrDefault();
            return d;
        }

        public List<Employee> getEmpListByDeptId(string deptId)
        {
            List<Employee> list = new List<Employee>();
            list = context.Employees.Where(x => x.DepartmentID.Equals(deptId)
            && x.EmpTitle != ("Head")).ToList();
            return list;
        }
    }
}