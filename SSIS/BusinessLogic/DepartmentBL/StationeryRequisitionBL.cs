using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DepartmentDA;
using DataAccess;
using Model;
using BusinessLogic;


namespace BusinessLogic.DepartmentBL
{
    public class StationeryRequisitionBL
    {
        StationeryRequisitionDA srda = new StationeryRequisitionDA();
        RequisitionBO rbo;

        public RequisitionBO convertRequisitionBO(Requisition r)
        {
            rbo = new RequisitionBO();
            rbo.RequisitionId = r.RequisitionID;
            rbo.EmpId = r.EmpID;
            rbo.CommentsByEmp = r.CommentsByEmp;
            rbo.Status = r.Status;
            rbo.EmpRequisitionDate = r.EmpRequisitionDate;
            rbo.ApprovalDate = r.ApprovalDate;
            rbo.CommentsByHead = r.CommentsByHead;
            return rbo;
        }
        public List<RetrievalRequisitionListBO> getRequisitionListByEmpId(string empId)
        {
            List<RetrievalRequisitionListBO> rbo = new List<RetrievalRequisitionListBO>();
            foreach (RetrievalRequisitionListBO r in srda.getRequisitionListByEmpId(empId))
            {
                rbo.Add(r);
            }
            return rbo;
        }

        public List<RetrievalRequisitionListBO> getRequisitionListByDepId(string depId)
        {
            List<RetrievalRequisitionListBO> rbo = new List<RetrievalRequisitionListBO>();

            foreach (RetrievalRequisitionListBO r in srda.getRequisitionListByDepId(depId))
            {
                rbo.Add(r);
            }
            return rbo;
        }
    }
}
