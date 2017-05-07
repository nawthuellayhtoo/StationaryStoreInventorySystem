using System;
using System.Collections.Generic;

namespace Model
{
    public class PurchaseOrderBO
    {
        private string poId;
        private DateTime? deliveryDate;
        private DateTime? dateOfOrder;
        private string requestorId;
        private string commentByClerk;
        private string commentBySupervisor;
        private string approverId;
        private DateTime? approvalDate;
        private string status;
        private EmployeeBO requestor;
        private EmployeeBO approver;
        private List<PurchaseOrderDetailsBO> purchaseOrderDetailsList;

        public List<PurchaseOrderDetailsBO> PurchaseOrderDetailsList
        {
            get { return purchaseOrderDetailsList; }
            set { purchaseOrderDetailsList = value; }
        }


        public EmployeeBO Approver
        {
            get { return approver; }
            set { approver = value; }
        }


        public EmployeeBO Requestor
        {
            get { return requestor; }
            set { requestor = value; }
        }


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

        public DateTime? DeliveryDate
        {
            get
            {
                return deliveryDate;
            }

            set
            {
                deliveryDate = value;
            }
        }

        public DateTime? DateOfOrder
        {
            get
            {
                return dateOfOrder;
            }

            set
            {
                dateOfOrder = value;
            }
        }

        public string RequestorId
        {
            get
            {
                return requestorId;
            }

            set
            {
                requestorId = value;
            }
        }

        public string CommentByClerk
        {
            get
            {
                return commentByClerk;
            }

            set
            {
                commentByClerk = value;
            }
        }
        public string CommentBySupervisor
        {
            get
            {
                return commentBySupervisor;
            }

            set
            {
                commentBySupervisor = value;
            }
        }

        public string ApproverId
        {
            get
            {
                return approverId;
            }

            set
            {
                approverId = value;
            }
        }

        public DateTime? ApprovalDate
        {
            get
            {
                return approvalDate;
            }

            set
            {
                approvalDate = value;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
    }
}
