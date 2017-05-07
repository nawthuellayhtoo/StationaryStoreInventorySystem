using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccess.StoreDA;

namespace BusinessLogic.StoreBL
{
    public class GenerateReportBL
    {
        GenerateReportDA gbl = new GenerateReportDA();
        public List<DepartmentBO> getDepartmentList()
        {
            return gbl.getDepartmentList();
        }
    }
}
