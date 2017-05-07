using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.StoreDA;
using System.Net;
using System.Net.Mail;
using Model;
using DataAccess;

namespace BusinessLogic.StoreBL
{
    public class CollectionTimeBL
    {
        CollectionTimeDA cda = new CollectionTimeDA();
        EmployeeBO ebo;
        DepartmentBO dbo;
        public List<string> getAllCollectionPointList()
        {
            List<string> l = new List<string>();
            l = cda.getCollectionPointList();

            return l;
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
        public List<string> getAllCollectionTimeList()
        {
            List<string> l = new List<string>();
            l = cda.getCollectionTimeList();

            return l;
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

        public int updateCollectionTime(string cp, string ct)
        {
            bool update = cda.updateCollectionTime(cp, ct);

            string cpid = cda.getCPIdForCP(cp);
            List<string> repid = cda.getRepId(cpid);
            List<EmployeeBO> rep = new List<EmployeeBO>();
            Employee e1;
            EmployeeBO b;
            SendEmail se = new SendEmail();
            string sub = "Change in Collection Time";
            foreach (string r in repid)
            {
                e1 = cda.getRepInfo(r);
                b = convertEmployeeBO(e1);
                rep.Add(b);
            }

            if (update)
            {
                foreach (EmployeeBO b1 in rep)
                {
                    string name = b1.EmployeeName;
                    string body = "Dear " + name + ", \n"
                       + "\n" + "The Collection Time is to changed to " + ct + " for the Collection Point " + cp
                       + "\n\n\n" + "Regards,"
                       + "\n" + "Admin";
                    se.sendCPEmail(sub, body, b1.EmployeeEmail);
                }
            }

            return 1;
        }
    }
}
