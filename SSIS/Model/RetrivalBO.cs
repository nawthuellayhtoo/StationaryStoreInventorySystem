using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RetrivalBO
    {
        string bin;
        string itemName;
        int needed;
        int retrived;

        public string Bin
        {
            get
            {
                return bin;
            }

            set
            {
                bin = value;
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
        public int Retrived
        {
            get
            {
                return retrived;
            }

            set
            {
                retrived = value;
            }
        }

    }
}
