using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DepartmentDA
{
    public class AssignDepartmentRepresentativeDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        public Employee getDeptRep(string deptId)
        {
            Employee e = new Employee();
            Department d = context.Departments.Where(x => x.DepartmentID.Equals(deptId)).FirstOrDefault();

            e = context.Employees.Where(x => x.EmpID.Equals(d.RepID)).FirstOrDefault();

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
            Department d = new Department();
            list = context.Employees.Where(x => x.DepartmentID.Equals(deptId)
            && x.EmpTitle != ("Head")).ToList();
            return list;
        }

        public int assignDepRep(string empId)
        {
            Employee e = new Employee();
            e = context.Employees.Where(x => x.EmpID.Equals(empId)).FirstOrDefault();
            Department d = new Department();
            d = context.Departments.Where(x => x.DepartmentID.Equals(e.DepartmentID)).FirstOrDefault();
            d.RepID = empId;
            context.SaveChanges();
            return 0;
        }
    }
}