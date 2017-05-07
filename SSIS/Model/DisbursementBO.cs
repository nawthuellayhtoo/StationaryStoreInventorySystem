namespace Model
{
    public class DisbursementBO
    {
        private string disbursementId;
        private string departmentId;
        private string itemNumber;
        private int? orderQuantity;
        private int? disbursementQuantity;
        private string status;
        private string requisitionId;
        private DepartmentBO department;
        private InventoryStockBO item;
        private RequisitionBO requisition;

        public RequisitionBO Requisition
        {
            get { return requisition; }
            set { requisition = value; }
        }

        public InventoryStockBO Item
        {
            get { return item; }
            set { item = value; }
        }

        public DepartmentBO Department
        {
            get { return department; }
            set { department = value; }
        }

        public string DisbursementId
        {
            get
            {
                return disbursementId;
            }

            set
            {
                disbursementId = value;
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

        public int? OrderQuantity
        {
            get
            {
                return orderQuantity;
            }

            set
            {
                orderQuantity = value;
            }
        }

        public int? DisbursementQuantity
        {
            get
            {
                return disbursementQuantity;
            }

            set
            {
                disbursementQuantity = value;
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

        public string RequisitionId
        {
            get
            {
                return requisitionId;
            }

            set
            {
                requisitionId = value;
            }
        }
    }
}
