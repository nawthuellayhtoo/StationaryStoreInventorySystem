using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RequisitionBO
/// </summary>
public class RequisitionBO
{
    private string requisitionId;
    private string empId;
    private string commentsByEmp;
    private string status;
    private DateTime? empRequisitionDate;
    private DateTime? approvalDate;
    private string commentsByHead;
    private EmployeeBO employee;
    private List<RequisitionDetailsBO> requisitionDetailsList;

    public List<RequisitionDetailsBO> RequisitionDetailsList
    {
        get { return requisitionDetailsList; }
        set { requisitionDetailsList = value; }
    }


    public EmployeeBO Employee
    {
        get { return employee; }
        set { employee = value; }
    }

    public string RequisitionId
    {
        get
        {
            return requisitionId;
        }

        set
        {
            requisitionId = value;
        }
    }

    public string EmpId
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

    public string CommentsByEmp
    {
        get
        {
            return commentsByEmp;
        }

        set
        {
            commentsByEmp = value;
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

    public DateTime? EmpRequisitionDate
    {
        get
        {
            return empRequisitionDate;
        }

        set
        {
            empRequisitionDate = value;
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

    public string CommentsByHead
    {
        get
        {
            return commentsByHead;
        }

        set
        {
            commentsByHead = value;
        }
    }
}