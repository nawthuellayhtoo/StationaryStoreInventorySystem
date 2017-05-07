namespace Model
{
    public class RequisitionDetailsBO
    {
        private string requisitionDetailsId;
        private string requisitionId;
        private string itemNumber;
        private int? itemQuantity;
        private InventoryStockBO item;

        public InventoryStockBO Item
        {
            get { return item; }
            set { item = value; }
        }


        public string RequisitionDetailsId
        {
            get
            {
                return requisitionDetailsId;
            }

            set
            {
                requisitionDetailsId = value;
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

        public int? ItemQuantity
        {
            get
            {
                return itemQuantity;
            }

            set
            {
                itemQuantity = value;
            }
        }
    }
}
