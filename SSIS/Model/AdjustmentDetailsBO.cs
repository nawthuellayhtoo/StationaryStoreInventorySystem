namespace Model
{
    public class AdjustmentDetailsBO
    {
        private string adjustmentDetailsId;
        private string voucherId;
        private AdjustmentBO voucher;
        private string itemNumber;
        private InventoryStockBO item;
        private int? quantityAdjustment;
        private string supplierId;
        private SupplierBO supplier;
        private string reason;

        public SupplierBO Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }

        public InventoryStockBO Item
        {
            get { return item; }
            set { item = value; }
        }

        public AdjustmentBO Voucher
        {
            get { return voucher; }
            set { voucher = value; }
        }

        public string AdjustmentDetailsId
        {
            get
            {
                return adjustmentDetailsId;
            }

            set
            {
                adjustmentDetailsId = value;
            }
        }

        public string VoucherId
        {
            get
            {
                return voucherId;
            }

            set
            {
                voucherId = value;
            }
        }

        public string ItemNumber
        {
            get
            {
                return itemNumber;
            }

            set
            {
                itemNumber = value;
            }
        }

        public int? QuantityAdjustment
        {
            get
            {
                return quantityAdjustment;
            }

            set
            {
                quantityAdjustment = value;
            }
        }

        public string SupplierId
        {
            get
            {
                return supplierId;
            }

            set
            {
                supplierId = value;
            }
        }

        public string Reason
        {
            get
            {
                return reason;
            }

            set
            {
                reason = value;
            }
        }
    }
}
