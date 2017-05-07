using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.StoreDA;
using Model;
  namespace BusinessLogic.StoreBL
{
   public class AdjustmentVoucherListDetailsBL
    {
        AdjustmentVoucherListDetailsDA ada = new AdjustmentVoucherListDetailsDA();
        public List<AdjustmentBO> getAdjustmentVoucherListById(string vouId)
        {
            List<AdjustmentBO> abo = new List<AdjustmentBO>();
            abo = ada.getAdjustmentVoucherById(vouId);
            return abo;
        }
        public List<AdjustmentDetailsBO> getAdjustmentVoucherListDetails(string vouId)
        {
            List<AdjustmentDetailsBO> abo = new List<AdjustmentDetailsBO>();
            abo = ada.getAdjustmentVoucherListDetails(vouId);
            return abo;
        }
        public string getApplier(string empId)
        {
            return ada.getApplier(empId);
        }
        public EmployeeBO getApproverSup(string title)
        {
            return ada.getApproverSup(title);
        }
        public EmployeeBO getApproverMan(string title)
        {
            return ada.getApproverMan(title);
        }
        protected void SendEmail(string empName, string voId)
        {
            SendEmail se = new SendEmail();
            EmployeeBO eboSup = getApproverSup("Supervisor");
            EmployeeBO eboMan = getApproverMan("Manager");

            string sub = "Adjustment Voucher Created";
            string body = "Dear " + eboMan.EmployeeName + ", \n"
                        + "\n" + "An adjustment voucher " + voId + " has been created by " + empName + ", has been approved by " + eboSup.EmployeeName + " for your final approval."
                        + "\n" + "(This is an auto-generated email, please do not reply.)"
                        + "\n\n\n" + "Regards,"
                        + "\n" + "Admin";
            se.sendCPEmail(sub, body, eboMan.EmployeeEmail);
        }

        //this method is under normal circumstances where no delegation is in place.
        public Boolean updateStatus(string voId, string authorise, string empName, string costValue)
        {
            bool status = false;
            if (authorise.Equals("Manager"))
            {
                status = ada.updateStatusMan(voId);
            }
            else if (authorise.Equals("Supervisor"))
            {
                double price = Convert.ToDouble(costValue);
                if (price >= 250)
                {
                    status = ada.updateStatusSup(voId, "Pending");
                    if (status == true)
                    {
                        SendEmail(empName, voId); // if supervisor approve than send email to manager
                    }
                }
                else if (price < 250)
                {
                    status = ada.updateStatusSup(voId, "Approved");
                }
            }
            return status;
        }

        // this method controls if supervisor / manager is delegated and will update the db accordingly
        public Boolean updateStatusDelegate(string voId, string authorise, string empName, string costValue)
        {
            bool status = false;
            double price = Convert.ToDouble(costValue);
            if (price >= 250)
            {
                if (ada.updateStatusSup(voId, "Approved") == true && ada.updateStatusMan(voId) == true)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            else if (price < 250)
            {
                if (ada.updateStatusSup(voId, "Approved") == true)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            return status;
        }

        public string checkSupApproval(string voId) //check to enable button
        {
            return ada.checkSupApproval(voId);
        }
        public Boolean checkDelegate(string title)
        {
            EmployeeBO ebo = ada.checkDelegate(title);
            if (DateTime.Today >= ebo.DelegateStartDate)
            {
                if (ebo.DelegateEndDate >= DateTime.Today)
                {
                    if (ebo.Delegate.Equals(1))
                    {
                        if (title.Equals("Manager"))
                        {
                            return true;
                        }

                        else if (title.Equals("Supervisor"))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
