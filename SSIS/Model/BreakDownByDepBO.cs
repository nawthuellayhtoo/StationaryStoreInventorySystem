using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BreakDownByDepBO
    {
        string dep;
        int needed;
        int actual;
        int outstanding;

        public string Dep
        {
            get { return dep; }
            set { dep = value; }
        }

        public int Needed
        {
            get
            {
                return needed;
            }

            set
            {
                needed = value;
            }
        }
        public int Actual
        {
            get
            {
                return actual;
            }

            set
            {
                actual = value;
            }
        }

        public int Outstanding
        {
            get
            {
                return outstanding;
            }

            set
            {
                outstanding = value;
            }
        }
    }
}
