namespace Model
{
    public class PurchaseOrderDetailsBO
    {
        private string poDetailsId;
        private string poId;
        private string supplierId;
        private string itemNumber;
        private int? quantity;
        private SupplierBO supplier;
        private InventoryStockBO item;

        public InventoryStockBO Item
        {
            get { return item; }
            set { item = value; }
        }
        
        public SupplierBO Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }
        
        public string PoDetailsId
        {
            get
            {
                return poDetailsId;
            }

            set
            {
                poDetailsId = value;
            }
        }

        public string PoId
        {
            get
            {
                return poId;
            }

            set
            {
                poId = value;
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

        public int? Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }
    }
}
