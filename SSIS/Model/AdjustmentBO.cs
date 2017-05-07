using System;
using System.Collections.Generic;

namespace Model
{
    public class AdjustmentBO
    {
        private string voucherId;
        private string empId;
        private EmployeeBO employee;
        private string adjustmentStatus;
        private DateTime? voucherDate;
        private string authorisedBySupervisor;
        private string authorisedByManager;
        private double? totalPrice;
        private List<AdjustmentDetailsBO> adjustmentDetailsList;

        public EmployeeBO Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        public string VoucherId
        {
            get { return voucherId; }
            set { voucherId = value; }
        }
        
        public string EmployeeId
        {
            get { return empId; }
            set { empId = value; }
        }
        
        public string AdjustmentStatus
        {
            get { return adjustmentStatus; }
            set { adjustmentStatus = value; }
        }
        
        public DateTime? VoucherDate
        {
            get { return voucherDate; }
            set { voucherDate = value; }
        }
        
        public string AuthorisedBySupervisor
        {
            get { return authorisedBySupervisor; }
            set { authorisedBySupervisor = value; }
        }

        public string AuthorisedByManager
        {
            get { return authorisedByManager; }
            set { authorisedByManager = value; }
        }
        
        public double? TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
        
        public List<AdjustmentDetailsBO> AdjustmentDetailsList
        {
            get { return adjustmentDetailsList; }
            set { adjustmentDetailsList = value; }
        }

    }
}