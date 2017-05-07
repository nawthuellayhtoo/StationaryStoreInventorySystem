using DataAccess;
using DataAccess.StoreDA;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.StoreBL
{
     public class StoreDisbursementBL
    {
        StoreDisbursementDA sdda = new StoreDisbursementDA();
        DaToBoConversion dbConversion = new DaToBoConversion();

        public List<DepartmentBO> getDistictDepList()// To get distinct dept
        {
            List<Department> dLst = sdda.getDistinctDepList();
            List<DepartmentBO> dboLst = new List<DepartmentBO>();
            foreach (Department d in dLst)
            {
                DepartmentBO dbo = dbConversion.convertDeptBO(d);
                dboLst.Add(dbo);
            }

            return dboLst;
        }
        public List<string> getDistictDeptNameList() //To get distinct deptname list
        {
            List<string> nameLst = sdda.getDistinctDepartmentNameList();
            return nameLst;
        }

        public string getDepIdByDepName(string depName) //To get depId by depName
        {
            string depId = sdda.getDepIdByDepName(depName);
            return depId;
        }

        public string getDepRepName(string depId)  //To get dep rep name 
        {
            string repName = sdda.getDepRepByDepId(depId);
            return repName;
        }

        public string getCollectionPoint(string depId)//To get dept collection point
        {
            string collectionPoint = sdda.getCollectionpoint(depId);
            return collectionPoint;
        }

        public string getCollectionTime(string depId)//To get dept collection time
        {
            string collectionTime = sdda.getCollectionTime(depId);
            return collectionTime;
        }

        public List<DisbursementListBO> detDisbursementList(string depId)//To get the disbursement list 
        {
            List<DisbursementListBO> list = sdda.getDisursementListByDepId(depId);
            return list;
        }
    }
}
