using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DepartmentBO
/// </summary>
public class DepartmentBO
{
    private string deptId;
    private string deptName;
    private string deptRep;
    private string deptCollectionPoint;
    private List<DisbursementBO> disbursementList;
    private List<EmployeeBO> employeeList;
    private List<OutstandingInfoBO> outstandingList;

    public string DeptId
    {
        get
        {
            return deptId;
        }

        set
        {
            deptId = value;
        }
    }

    public string DeptName
    {
        get
        {
            return deptName;
        }

        set
        {
            deptName = value;
        }
    }

    public string DeptRep
    {
        get
        {
            return deptRep;
        }

        set
        {
            deptRep = value;
        }
    }

    public string DeptCollectionPoint
    {
        get
        {
            return deptCollectionPoint;
        }

        set
        {
            deptCollectionPoint = value;
        }
    }

    public List<DisbursementBO> DisbursementList
    {
        get
        {
            return disbursementList;
        }

        set
        {
            disbursementList = value;
        }
    }

    public List<EmployeeBO> EmployeeList
    {
        get
        {
            return employeeList;
        }

        set
        {
            employeeList = value;
        }
    }

    public List<OutstandingInfoBO> OutstandingList
    {
        get
        {
            return outstandingList;
        }

        set
        {
            outstandingList = value;
        }
    }
}