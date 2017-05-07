using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccess;

namespace DataAccess.StoreDA
{
    public class CollectionTimeDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();
        public List<string> getCollectionPointList()
        {
            List<string> l = new List<string>();
            l = (from x in context.CollectionPointDetails
                 select x.CollectionPoint).ToList();
            return l;
        }

        public List<string> getCollectionTimeList()
        {
            List<string> l = new List<string>();
            l = (from x in context.CollectionPointDetails
                 select x.CollectionTime).Distinct().ToList();
            return l;
        }
        public string getCollectionTime(string cp)
        {

            string ct = (from x in context.CollectionPointDetails
                         where x.CollectionPoint.Equals(cp)
                         select x.CollectionTime).First();
            return ct;
        }
        public Department getDepartmentById(string deptId)
        {
            Department d = new Department();
            d = context.Departments.Where(x => x.DepartmentID == deptId).FirstOrDefault();

            return d;
        }
        public string getCollectionPointValue(string depId)
        {
            string cpv = (from x in context.CollectionPointDetails
                          join y in context.Departments on x.CollectionPointID equals y.CollectionPointID
                          where y.DepartmentID.Equals(depId)
                          select x.CollectionPoint).First();
            return cpv;
        }
        public string getCollectionId(string cp)
        {
            string d = (from x in context.CollectionPointDetails
                        where x.CollectionPoint.Equals(cp)
                        select x.CollectionPointID).First();
            return d;
        }

        public bool updateCollectionTime(string cp, string ct)
        {
            bool r = false;

            CollectionPointDetail d = new CollectionPointDetail();
            d = (from x in context.CollectionPointDetails
                 where x.CollectionPoint.Equals(cp)
                 select x).First();

            if (d != null)
            {

                d.CollectionTime = ct;
                context.SaveChanges();
                r = true;
            }
            return r;
        }
        public List<string> getClerkEmail()
        {
            List<string> ec = (from x in context.Employees
                               where x.EmpTitle.Equals("Clerk")
                               select x.Email).ToList();
            return ec;
        }
        public string getCPIdForCP(string cp)
        {
            string repid = (from x in context.CollectionPointDetails
                            where x.CollectionPoint.Equals(cp)
                            select x.CollectionPointID).First();

            return repid;

        }

        public List<string> getRepId(string cpid)
        {

            List<string> repid = (from x in context.Departments
                                  where x.CollectionPointID.Equals(cpid)
                                  select x.RepID).ToList();
            return repid;

        }

        public Employee getRepInfo(string r)
        {
            Employee b = (from x in context.Employees
                          where x.EmpID.Equals(r)
                          select x).First();
            return b;

        }
    }
}
