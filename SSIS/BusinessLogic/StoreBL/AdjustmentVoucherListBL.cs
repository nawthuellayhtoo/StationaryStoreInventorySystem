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
    public class AdjustmentVoucherListBL
    {
        AdjustmentVoucherListDA ada = new AdjustmentVoucherListDA();
        public List<AdjustmentBO> getAdjustmentVoucherListByClerk(string empId)
        {
            List<AdjustmentBO> abo = new List<AdjustmentBO>();
            abo = ada.getAdjustmentVoucherListByClerk(empId);
            return abo;
        }
        public List<AdjustmentBO> getAdjustmentVoucherListMan()
        {
            List<AdjustmentBO> abo = new List<AdjustmentBO>();
            abo = ada.getAdjustmentVoucherListMan();
            return abo;
        }
        public List<AdjustmentBO> getAdjustmentVoucherListSup()
        {
            List<AdjustmentBO> abo = new List<AdjustmentBO>();
            abo = ada.getAdjustmentVoucherListSup();
            return abo;
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
