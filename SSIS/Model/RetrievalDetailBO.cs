using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RetrievalDetailBO
    {
        RetrivalBO rbo;
        List<BreakDownByDepBO> bdboLst;

        public RetrivalBO Rbo
        {
            get { return rbo; }
            set { rbo = value; }
        }

        public List<BreakDownByDepBO> BdboLst
        {
            get { return bdboLst; }
            set { bdboLst = value; }
        }

    }
}
