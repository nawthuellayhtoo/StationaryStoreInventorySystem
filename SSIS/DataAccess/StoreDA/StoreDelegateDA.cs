using System;
using System.Linq;

namespace DataAccess.StoreDA
{
    public class StoreDelegateDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        public int activateDelegate(string empTitle, DateTime delegateStart, DateTime delegateEnd)
        {
            Employee e = getEmployeeByTitle(empTitle);
            e.Delegate = 1;
            e.DelegateStartDate = delegateStart;
            e.DelegateEndDate = delegateEnd;
            context.SaveChanges();
            return 0;
        }

        public int deactivateDelegate(string empTitle)
        {
            Employee e = getEmployeeByTitle(empTitle);
            e.Delegate = 0;
            context.SaveChanges();
            return 0;
        }

        public Employee getStoreManager()
        {
            Employee e = new Employee();
            e = context.Employees.Where(x => x.EmpTitle.Equals("Manager") && x.DepartmentID.Equals("STORE")).FirstOrDefault();
            return e;
        }

        public Employee getStoreSupervisor()
        {
            Employee e = new Employee();
            e = context.Employees.Where(x => x.EmpTitle.Equals("Supervisor") && x.DepartmentID.Equals("STORE")).FirstOrDefault();
            return e;
        }

        public Employee getEmployeeByTitle(string empTitle)
        {
            Employee e = new Employee();
            e = context.Employees.Where(x => x.EmpTitle.Equals(empTitle) && x.DepartmentID.Equals("STORE")).FirstOrDefault();
            return e;
        }

        public Department getDepartmentById(string deptId)
        {
            Department d = new Department();
            d = context.Departments.Where(x => x.DepartmentID == deptId).FirstOrDefault();
            return d;
        }
    }
}