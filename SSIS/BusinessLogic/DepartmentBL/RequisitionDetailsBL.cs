using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DepartmentDA;
using Model;

namespace BusinessLogic.DepartmentBL
{
    public class RequisitionDetailsBL
    {
        RequisitionDetailsDA rdda = new RequisitionDetailsDA();
        CreateRequisitionDA crda = new CreateRequisitionDA();

        public List<RetrievalRequisitionDetailsBO> getRequisitionDetailsByReqId(string reqId)
        {
            return rdda.getRequisitionDetailsByReqId(reqId);
        }

        public string getEmpComments(string reqId)
        {
            return rdda.getEmpComments(reqId);
        }

        public void updateRequisitionStatus(string requisitionId, string status)
        {
            rdda.updateRequisitionStatus(requisitionId, status);
        }

        public RetrievalRequisitionListBO getReqInfo(string reqId)
        {
            return rdda.getReqInfo(reqId);
        }

        public void resubmitRequisition(String requisitionId, string comment)
        {
            rdda.resubmitRequisition(requisitionId, comment);
        }

        public void resubmitRequisitionDetails(string requisitionDetailsId, int quantity)
        {
            rdda.resubmitRequisitionDetails(requisitionDetailsId, quantity);
        }

        public void deleteRequisition(string requisitionId)
        {
            rdda.deleteRequisition(requisitionId);
        }

        public string getRequisitionDetailsIdByItemName(string requisitionId, string itemName)
        {
            string itemNumber = crda.getItemNumberByItemName(itemName);
            return rdda.getRequisitionDetailsIdByItemNumber(requisitionId, itemNumber);
        }
    }
}
