using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PurchaseOrderDetailsJoinBO
    {
        private string poId;
        private string itemNumber;
        private string itemName;
        private int? quantity;
        private string itemUOM;
        private string supplier;
        private double price;
        private double totalPrice;

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
        public string ItemName
        {
            get
            {
                return itemName;
            }

            set
            {
                itemName = value;
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
        public string ItemUOM
        {
            get
            {
                return itemUOM;
            }

            set
            {
                itemUOM = value;
            }
        }
        public string Supplier
        {
            get
            {
                return supplier;
            }

            set
            {
                supplier = value;
            }
        }
        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }
        public double TotalPrice
        {
            get
            {
                return totalPrice;
            }

            set
            {
                totalPrice = value;
            }
        }
    }
}
