using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RetrievalRequisitionDetailsBO
    {
        private string retrievalItemName;
        private int? retrievalQuantity;
        private string retrievalUOM;

        public string RetrievalItemName
        {
            get
            {
                return retrievalItemName;
            }

            set
            {
                retrievalItemName = value;
            }
        }

        public int? RetrievalQuantity
        {
            get
            {
                return retrievalQuantity;
            }

            set
            {
                retrievalQuantity = value;
            }
        }

        public string RetrievalUOM
        {
            get
            {
                return retrievalUOM;
            }

            set
            {
                retrievalUOM = value;
            }
        }
    }
}
