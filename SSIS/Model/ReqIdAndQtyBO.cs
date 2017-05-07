using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ReqIdAndQtyBO
    {
        string reqId;
        int disbursedQty;

        public string ReqId
        {
            get { return reqId; }
            set { reqId = value; }
        }

        public int DisbursedQty
        {
            get { return disbursedQty; }
            set { disbursedQty = value; }
        }
    }
}
