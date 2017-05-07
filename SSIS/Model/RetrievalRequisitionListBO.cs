using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RetrievalRequisitionListBO
    {
        private string retrievalrequisitionId;
        private string retrievalstatus;
        private string retrievalempName;
        private DateTime? retrievalempRequisitionDate;
        public string RetrievalrequisitionId
        {
            get
            {
                return retrievalrequisitionId;
            }

            set
            {
                retrievalrequisitionId = value;
            }
        }

        public string Retrievalstatus
        {
            get
            {
                return retrievalstatus;
            }

            set
            {
                retrievalstatus = value;
            }
        }

        public string RetrievalempName
        {
            get
            {
                return retrievalempName;
            }

            set
            {
                retrievalempName = value;
            }
        }

        public DateTime? RetrievalempRequisitionDate
        {
            get
            {
                return retrievalempRequisitionDate;
            }

            set
            {
                retrievalempRequisitionDate = value;
            }
        }
    }
}
