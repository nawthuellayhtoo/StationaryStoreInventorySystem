using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ReadyForCollectionBO
    {
        string depId;
        string itemNumber;
        string reqisitionId;
        int? disbursedQuantity;
        public string DepId
        {
            get
            {
                return depId;
            }

            set
            {
                depId = value;
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

        public string ReqisitionId
        {
            get
            {
                return reqisitionId;
            }

            set
            {
                reqisitionId = value;
            }
        }

        public int? DisbursedQuantity
        {
            get
            {
                return disbursedQuantity;
            }

            set
            {
                disbursedQuantity = value;
            }
        }
    }
}
