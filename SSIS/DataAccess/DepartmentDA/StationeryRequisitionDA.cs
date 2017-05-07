using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess.DepartmentDA
{
    public class StationeryRequisitionDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        public List<RetrievalRequisitionListBO> getRequisitionListByEmpId(string empId)
        {
            var q = (from r in context.Requisitions
                     where r.EmpID == empId
                     select new RetrievalRequisitionListBO
                     {
                         RetrievalrequisitionId = r.RequisitionID,
                         RetrievalempRequisitionDate = r.EmpRequisitionDate,
                         Retrievalstatus = r.Status,
                         RetrievalempName = r.Employee.EmpName
                     }).ToList();
            return q;
        }


        public List<RetrievalRequisitionListBO> getRequisitionListByDepId(string depId)
        {
            List<RetrievalRequisitionListBO> empIdlst = new List<RetrievalRequisitionListBO>();
            empIdlst = (from e in context.Employees
                        join d in context.Departments on e.DepartmentID equals d.DepartmentID
                        join r in context.Requisitions on e.EmpID equals r.EmpID
                        where d.DepartmentID == depId
                        select new RetrievalRequisitionListBO
                        {
                            RetrievalrequisitionId = r.RequisitionID,
                            RetrievalempRequisitionDate = r.EmpRequisitionDate,
                            Retrievalstatus = r.Status,
                            RetrievalempName = r.Employee.EmpName
                        }).ToList();
            return empIdlst;
        }

     }
}
