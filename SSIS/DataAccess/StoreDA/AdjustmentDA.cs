using System.Linq;

namespace DataAccess.StoreDA
{
    public class AdjustmentDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        public Adjustment getAdjustmentById(string voucherId)
        {
            Adjustment av = new Adjustment();
            av = context.Adjustments.Where(x => x.VoucherID == voucherId).FirstOrDefault();
            return av;
        }
    }
}
