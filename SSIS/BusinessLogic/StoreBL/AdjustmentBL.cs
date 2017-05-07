using System;
using DataAccess;
using DataAccess.StoreDA;
using Model;

namespace BusinessLogic.StoreBL
{
    public class AdjustmentBL
    {
        AdjustmentDA da = new AdjustmentDA();
        AdjustmentBO abo;

        public AdjustmentBO convertAdjustmentBO(Adjustment av)
        {
            abo = new AdjustmentBO();
            return abo;
        }

        public AdjustmentBO getAdjustmentByVoucherId(string adjustmentId)
        {
            Adjustment av = da.getAdjustmentById(adjustmentId);
            AdjustmentBO abo = convertAdjustmentBO(av);

            return abo;
        }
    }
}
