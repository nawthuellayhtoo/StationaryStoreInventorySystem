using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess.StoreDA
{
    public class GenerateReportDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();
        public List<DepartmentBO> getDepartmentList()
        {
            List<DepartmentBO> dbo = new List<DepartmentBO>();

            var query = (from x in context.Departments
                         select new { x.DepartmentID, x.DepartmentName }).ToList();
            foreach (var q in query)
            {
                DepartmentBO b = new DepartmentBO();
                b.DeptId = q.DepartmentID;
                b.DeptName = q.DepartmentName;
                dbo.Add(b);
            }
            return dbo;
        }
    }
}
