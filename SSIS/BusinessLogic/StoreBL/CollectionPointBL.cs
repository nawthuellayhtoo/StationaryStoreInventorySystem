using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DepartmentDA;
using System.Net;
using System.Net.Mail;
using Model;
using DataAccess;
using BusinessLogic;

namespace BusinessLogic.StoreBL
{
    public class CollectionPointBL
    {
        CollectionPointDA cda = new CollectionPointDA();
        EmployeeBO ebo;
        DepartmentBO dbo;

        public List<string> getAllCollectionPointList()
        {
            List<string> l = new List<string>();
            l = cda.getCollectionPointList();

            return l;
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
        public DepartmentBO getDeptByEmpDeptId(String deptId)
        {
            Department dept = cda.getDepartmentById(deptId);
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

        public string getCollectionPoint(string depId)
        {
            string cp = cda.getCollectionPointValue(depId);
            return cp;
        }

        public string getCollectionTime(string cp)
        {
            string time = cda.getCollectionTime(cp);
            return time;
        }

        public int updateCollectionPoint(string depId, string cp)
        {
            bool update = cda.updateCollectionPoint(depId, cp);
            List<Employee> l = cda.getClerkEmail();
            List<EmployeeBO> clerk = new List<EmployeeBO>();
            EmployeeBO e1;
            SendEmail se = new SendEmail();
            string sub = "Change in Collection Point";
            foreach (Employee e in l)
            {
                e1 = convertEmployeeBO(e);
                clerk.Add(e1);
            }

            if (update)
            {
                foreach (EmployeeBO b in clerk)
                {
                   string name = b.EmployeeName;


                    string body = "Dear " + name + ", \n"
                        + "\n" + "The Collection Point is to changed to " + cp
                        + "\n\n\n" + "Regards,"
                        + "\n" + "Admin";
                    se.sendCPEmail(sub, body, b.EmployeeEmail);
                }
            }

            return 1;
        }
    }
}
