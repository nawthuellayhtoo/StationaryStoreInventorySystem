using System;
using System.Collections.Generic;

namespace Model
{
    public class EmployeeBO
    {
        private string empId;
        private string empDept;
        private string empTitle;
        private string empName;
        private string empEmail;
        private int empDelegate;
        private DateTime delegateStartDate;
        private DateTime delegateEndDate;
        private string password;
        private List<PurchaseOrderBO> poList;
        private List<AdjustmentBO> adjustmentList;
        private List<RequisitionBO> requisitionList;
        private List<EmployeeBO> employeeList;
        private DepartmentBO department;

        public List<EmployeeBO> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; }
        }

        public DepartmentBO Department
        {
            get { return department; }
            set { department = value; }
        }

        public string EmployeeId
        {
            get
            {
                return empId;
            }

            set
            {
                empId = value;
            }
        }

        public string EmployeeDept
        {
            get
            {
                return empDept;
            }

            set
            {
                empDept = value;
            }
        }

        public string EmployeeTitle
        {
            get
            {
                return empTitle;
            }

            set
            {
                empTitle = value;
            }
        }

        public string EmployeeName
        {
            get
            {
                return empName;
            }

            set
            {
                empName = value;
            }
        }

        public string EmployeeEmail
        {
            get
            {
                return empEmail;
            }

            set
            {
                empEmail = value;
            }
        }

        public int Delegate
        {
            get
            {
                return empDelegate;
            }

            set
            {
                empDelegate = value;
            }
        }

        public DateTime DelegateStartDate
        {
            get
            {
                return delegateStartDate;
            }

            set
            {
                delegateStartDate = value;
            }
        }

        public DateTime DelegateEndDate
        {
            get
            {
                return delegateEndDate;
            }

            set
            {
                delegateEndDate = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public List<PurchaseOrderBO> POList
        {
            get
            {
                return poList;
            }

            set
            {
                poList = value;
            }
        }

        public List<AdjustmentBO> AdjustmentList
        {
            get
            {
                return adjustmentList;
            }

            set
            {
                adjustmentList = value;
            }
        }

        public List<RequisitionBO> RequisitionList
        {
            get
            {
                return requisitionList;
            }
            set
            {
                requisitionList = value;
            }
        }
    }
}