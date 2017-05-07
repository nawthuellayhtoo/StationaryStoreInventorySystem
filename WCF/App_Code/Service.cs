using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

//SSIS services
public class Service : IService
{
    /*Retrieval list business layer reference*/
    RetrivalListBL n = new RetrivalListBL();

    /*Disbursement business layer reference*/
    DisbursementBL k = new DisbursementBL();

    /*Disbursement business layer reference*/
    DisbursementBL dbl = new DisbursementBL();

    /*StoreDisbursement business layer reference*/
    StoreDisbursementBL sbl = new StoreDisbursementBL();

    /*getting current department representative */
    public WCFEmployee currentRep(string deptId)
    {
        Employee e = EmployeeOp.currentRep(deptId);
        return WCFEmployee.Make(e.EmpID, e.DepartmentID, e.EmpTitle, e.EmpName, e.Email, e.Delegate, 1, e.DelegateStartDate.ToString(), e.DelegateEndDate.ToString());
    }

    /*get employee by username and password*/
    public WCFEmployee getEmployee(String un, String psd)
    {
        try
        {
            Employee e = EmployeeOp.Authenticate(un, psd);
            return WCFEmployee.Make(e.EmpID, e.DepartmentID, e.EmpTitle, e.EmpName, e.Email, e.Delegate, 1, e.DelegateStartDate.ToString(), e.DelegateEndDate.ToString());
        }
        catch
        {
            return WCFEmployee.Make(null, null, null, null, null, 0, 0, null, null);
        }
    }

    /*get employee list*/
    public List<string> GetDeptList()
    {
        return EmployeeOp.GetDept();
    }

    /*get collection point by deparment id*/
    public List<string> getCollectionPoint(string deptId)
    {
        return EmployeeOp.getCollectionPoint(deptId);

    }

    /*get collection time by deparment id*/
    public List<string> getCollectionTime(string deptId)
    {
        return EmployeeOp.getCollectionTime(deptId);
    }


    /* get retrieval list */
    public List<WCFRetrieval> getDataSource()
    {
        List<RetrivalBO> l = n.getDataSource();
        List<WCFRetrieval> m = new List<WCFRetrieval>();

        foreach (RetrivalBO b in l)
        {
            m.Add(WCFRetrieval.Make1(b.Bin, b.ItemName, b.Needed, b.Retrived));
        }
        return m;
    }

    /*get item amount break down list*/
    public List<WCFBreakdown> getBreakDownList(string itemName)
    {

        List<BreakDownByDepBO> l = n.getBreakDownLstByName(itemName);
        List<WCFBreakdown> m = new List<WCFBreakdown>();
        foreach (BreakDownByDepBO b in l)
        {
            m.Add(WCFBreakdown.Make2(b.Dep, b.Needed, b.Outstanding, b.Actual));
        }
        return m;
    }

    /*Get disbursement list by department id */
    public List<WCFDisbursedInventory> getDisListByDepId(string deptId)
    {

        List<DisbursedInventoryBO> l = k.getDisbursementListByDepId(deptId);

        List<WCFDisbursedInventory> m = new List<WCFDisbursedInventory>();

        foreach (DisbursedInventoryBO b in l)
        {
            m.Add(WCFDisbursedInventory.Make3(b.ItemName, b.ItemNumber, b.ItemUOM, b.OrderQuantity, b.DisbursementQuantity, b.OutstandingQuantity, b.RequisitionId));
        }
        return m;

    }

    /*update item amount break down list*/
    public List<WCFBreakdown> updateBreakDown(string itemName, int retrieved)
    {

        List<BreakDownByDepBO> l = n.updateBreakDownLst(itemName, retrieved);
        List<WCFBreakdown> m = new List<WCFBreakdown>();
        foreach (BreakDownByDepBO b in l)
        {
            m.Add(WCFBreakdown.Make2(b.Dep, b.Needed, b.Outstanding, b.Actual));
        }
        return m;
    }

    /* get disbursement list*/
    public List<WCFDisbursementBL> getDisbursementList(string deptId)
    {
        List<DisbursementListBO> l = sbl.detDisbursementList(deptId);
        List<WCFDisbursementBL> m = new List<WCFDisbursementBL>();
        foreach (DisbursementListBO dlbo in l)
        {
            m.Add(WCFDisbursementBL.Make4(dlbo.ItemName, dlbo.ItemUOM, dlbo.OrderQuantity, dlbo.OutstandingQuantity, dlbo.DisbursementQuantity));
        }
        return m;
    }

    /*get requisition list*/
    public List<WCFRequisition> getRequisition(string deptId)
    {
        return RequisitionOp.getRequisition(deptId);
    }

    /*get requistion details by requisition id*/
    public List<WCFRequisitionDetail> getRequisitionDetails(string reqId)
    {
        return RequisitionOp.getRequisitionDetail(reqId);
    }

    /*get deparment representative id*/
    public List<string> getRepId()
    {
        return EmployeeOp.getRepresentativeId();
    }

    /*get deparment head*/
    public WCFEmployee getDepHead(string deptId)
    {
        Employee e = EmployeeOp.getDepHead(deptId);
        return WCFEmployee.Make(e.EmpID, e.DepartmentID, e.EmpTitle, e.EmpName, e.Email, e.Delegate, 1, e.DelegateStartDate.ToString(), e.DelegateEndDate.ToString());
    }

    /*get delegated employee*/
    public WCFEmployee getDelEmployee(string deptId)
    {
        Employee e = EmployeeOp.getDelEmployee(deptId);
        return WCFEmployee.Make(e.EmpID, e.DepartmentID, e.EmpTitle, e.EmpName, e.Email, e.Delegate, 1, e.DelegateStartDate.ToString(), e.DelegateEndDate.ToString());
    }

    /*update approved requisition */
    public string ApproveRequisition(string comment, string reqId)
    {
        return RequisitionOp.ApproveRequisition(comment, reqId);

    }

    /*update rejected requisition */
    public string RejectRequisition(string comment, string reqId)
    {
        return RequisitionOp.RejectRequisition(comment, reqId);
    }

    /*get employee list by department id*/
    public List<WCFEmployee> getEmpByDep(string deptId)
    {
        List<Employee> e1 = EmployeeOp.getEmployee(deptId);
        List<WCFEmployee> l = new List<WCFEmployee>();
        foreach (Employee e in e1)
        {
            l.Add(WCFEmployee.Make(e.EmpID, e.DepartmentID, e.EmpTitle, e.EmpName, e.Email, e.Delegate, 1, e.DelegateStartDate.ToString(), e.DelegateEndDate.ToString()));
        }

        return l;
    }

    /*update delegated employee*/
    public void delegateEmp(string DeptId, string EmpName, string startDate, string endDate)
    {
        EmployeeOp.delegateEmp(DeptId, EmpName, startDate, endDate);
    }

    /*update department representative by department id and employee name*/
    public void UpdateRep(string deptId, string EmpName)
    {
        EmployeeOp.UpdateRep(deptId, EmpName);

    }

    /*update dedelegated employee*/
    public void DedelegateEmp(string EmpName, string DeptId)
    {
        EmployeeOp.DedelegateEmp(EmpName, DeptId);
    }

    /*get purchase order list*/
    public List<WCFPOList> getPOList()
    {
        List<WCFPOList> wcfPoList = new List<WCFPOList>();
        wcfPoList = PurchaseOrderOp.getPOList();
        return wcfPoList;
    }

    /*get purchase order detail*/
    public List<WCFPODetails> getPODetails(string PONumber)
    {
        return PurchaseOrderOp.getPODetails(PONumber);
    }

    /*update purchase order */
    public string updatePO(string POID, string status, string commentBySupervisor)
    {
        PurchaseOrder po = new PurchaseOrder();
        po.POID = POID;
        po.Status = status;
        po.CommentsBySupervisor = commentBySupervisor;
        return PurchaseOrderOp.updatePO(po);
    }
}
