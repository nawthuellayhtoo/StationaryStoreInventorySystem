using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Model;
using DataAccess.DepartmentDA;

namespace BusinessLogic.DepartmentBL
{
    public class CreateRequisitionBL
    {
        CreateRequisitionDA crda = new CreateRequisitionDA();

        public Requisition convertRequisitionBO(RequisitionBO rbo)
        {
            Requisition r = new Requisition();
            r.RequisitionID = rbo.RequisitionId;
            r.EmpID = rbo.EmpId;
            r.CommentsByEmp = rbo.CommentsByEmp;
            r.Status = rbo.Status;
            r.EmpRequisitionDate = rbo.EmpRequisitionDate;
            r.ApprovalDate = rbo.ApprovalDate;
            r.CommentsByHead = rbo.CommentsByHead;
            return r;
        }

        public RequisitionDetail convertRequisitionDetailsBO(RequisitionDetailsBO rdbo)
        {
            RequisitionDetail rd = new RequisitionDetail();
            rd.RequisitionID = rdbo.RequisitionId;
            rd.RequisitionDetailsID = rdbo.RequisitionDetailsId;
            rd.ItemNumber = rdbo.ItemNumber;
            rd.ItemQuantity = rdbo.ItemQuantity;
            return rd;
        }

        public string getItemNumberByItemName(string itemName)
        {
            return crda.getItemNumberByItemName(itemName);
        }

        public string createRequisitionId()
        {
            string requisitionId = "";
            string lastRequisitionId = crda.getLastRequisitionId();
            int requisitionIdNumber = Convert.ToInt32(lastRequisitionId.Substring(2)) + 1;
            requisitionId = "RQ" + requisitionIdNumber;
            return requisitionId;
        }

        public string createRequisitionDetailsId()
        {
            string requisitionDetailsId = "";
            string lastRequisitionDetailsId = crda.getLastRequisitionDetailsId();
            int requisitionDetailsIdNumber = Convert.ToInt32(lastRequisitionDetailsId.Substring(3)) + 1;
            requisitionDetailsId = "RQD" + requisitionDetailsIdNumber;
            return requisitionDetailsId;
        }

        public void submitRequisition(RequisitionBO rbo)
        {
            crda.submitRequisition(convertRequisitionBO(rbo));

        }

        public void submitRequisitionDetails(RequisitionDetailsBO rdbo)
        {
            crda.submitRequisitionDetails(convertRequisitionDetailsBO(rdbo));

        }

        public string getHeadNameByDepId(string depId)
        {
            return crda.getHeadNameByDepId(depId);
        }

        public string getHeadEmailByDepId(string depId)
        {
            return crda.getHeadEmailByDepId(depId);
        }

        public int getReorderLevelByItemNumber(string itemNumber)
        {
            return crda.getReorderLevelByItemNumber(itemNumber);
        }

        public int getInventoryQtyByItemNumber(string itemNumber)
        {
            return crda.getInventoryQtyByItemNumber(itemNumber);
        }

        public List<string> getAllClerksEmail()
        {
            return crda.getAllClerksEmail();
        }
    }
}

