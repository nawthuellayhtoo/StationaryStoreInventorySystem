using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess.DepartmentDA
{
    public class CollectionPointDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();
        public List<string> getCollectionPointList()
        {
            List<string> l = new List<string>();
            l = (from x in context.CollectionPointDetails
                 select x.CollectionPoint).ToList();
            return l;
        }

        public string getCollectionPointValue(string depId)
        {
            string cpv = (from x in context.CollectionPointDetails
                          join y in context.Departments on x.CollectionPointID equals y.CollectionPointID
                          where y.DepartmentID.Equals(depId)
                          select x.CollectionPoint).First();

            return cpv;
        }

        public string getCollectionTime(string cp)
        {

            string ct = (from x in context.CollectionPointDetails
                         where x.CollectionPoint.Equals(cp)
                         select x.CollectionTime).First();
            return ct;
        }

        public string getCollectionId(string cp)
        {
            string d = (from x in context.CollectionPointDetails
                        where x.CollectionPoint.Equals(cp)
                        select x.CollectionPointID).First();

            return d;

        }
       public bool updateCollectionPoint(string depId, string cp)
        {
            bool r = false;
            string id;
            Department d = new Department();
            d = (from x in context.Departments
                 where x.DepartmentID.Equals(depId)
                 select x).First();

            if (d != null)
            {

                id = getCollectionId(cp);
                d.CollectionPointID = id;
                context.SaveChanges();
                r = true;
            }
            return r;
        }
        public List<Employee> getClerkEmail()
        {
            List<Employee> ec = (from x in context.Employees
                                 where x.EmpTitle.Equals("Clerk")
                                 select x).ToList();
            return ec;
        }
        public Department getDepartmentById(string deptId)
        {
            Department d = new Department();
            d = context.Departments.Where(x => x.DepartmentID == deptId).FirstOrDefault();
            return d;
        }
    }
}
