using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AdjustmentInfoBO
    {
        private string adjustmentDetailsId;
        private string voucherId;
        private AdjustmentBO voucher;
        private string itemNumber;
        private string itemName;
        private InventoryStockBO item;
        private int? quantityAdjustment;
        private string supplierId;
        private SupplierBO supplier;
        private string uom;
        private string reason;
        private string price;

        public string Price
        {
            get { return price; }
            set { price = value; }
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public string Uom
        {
            get { return uom; }
            set { uom = value; }
        }

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
