using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class LoginDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

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
            && x.EmpTitle!=("Head")).ToList();
            return list;
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
        public void updateDelegateStatus(string id)
        {
            Employee e = getEmployeeById(id);
            e.Delegate = 0;
            context.SaveChanges();
        }
    }
}
