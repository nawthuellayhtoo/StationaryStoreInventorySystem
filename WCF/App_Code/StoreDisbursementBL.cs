using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StoreDisbursementBL
/// </summary>
public class StoreDisbursementBL
{
   
        StoreDisbursementDA sdda = new StoreDisbursementDA();
        DaToBoConversion dbConversion = new DaToBoConversion();

        public List<DepartmentBO> getDistictDepList()
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
        public List<string> getDistictDeptNameList()
        {
            List<string> nameLst = sdda.getDistinctDepartmentNameList();
            return nameLst;
        }

        public string getDepIdByDepName(string depName)
        {
            string depId = sdda.getDepIdByDepName(depName);
            return depId;
        }

        public string getDepRepName(string depId)
        {
            string repName = sdda.getDepRepByDepId(depId);
            return repName;
        }

        public string getCollectionPoint(string depId)
        {
            string collectionPoint = sdda.getCollectionpoint(depId);
            return collectionPoint;
        }

        public string getCollectionTime(string depId)
        {
            string collectionTime = sdda.getCollectionTime(depId);
            return collectionTime;
        }

        public List<DisbursementListBO> detDisbursementList(string depId)
        {
            List<DisbursementListBO> list = sdda.getDisursementListByDepId(depId);
            return list;
        }
    }