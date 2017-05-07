namespace Model
{
    public class OutstandingInfoBO
    {
        private string outstandingId;
        private string itemNumber;
        private string departmentId;
        private int? quantity;
        private string status;
        private int? partialPendingQuantity;
        private InventoryStockBO item;
        private DepartmentBO department;

        public DepartmentBO Department
        {
            get { return department; }
            set { department = value; }
        }

        public InventoryStockBO Item
        {
            get { return item; }
            set { item = value; }
        }

        public string OutstandingId
        {
            get
            {
                return outstandingId;
            }

            set
            {
                outstandingId = value;
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

        public string DepartmentId
        {
            get
            {
                return departmentId;
            }

            set
            {
                departmentId = value;
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
        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
        public int? PartialPendingQuantity
        {
            get
            {
                return partialPendingQuantity;
            }

            set
            {
                partialPendingQuantity = value;
            }
        }
    }
}
