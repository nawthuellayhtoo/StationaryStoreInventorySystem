using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class DisbursementListBO
    {
        private string itemName;
        private string itemUOM;
        private int? orderQuantity;
        private int? outstandingQuantity;
        private int? disbursementQuantity;

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

        public int? OutstandingQuantity
        {
            get
            {
                return outstandingQuantity;
            }

            set
            {
                outstandingQuantity = value;
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
    }
}
