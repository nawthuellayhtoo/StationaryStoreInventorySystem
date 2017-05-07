using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{
    [OperationContract]
    [WebGet(UriTemplate = "/Rep?DepartmentId={deptId}", ResponseFormat = WebMessageFormat.Json)]
    WCFEmployee currentRep(string deptId);

    [OperationContract]
    [WebGet(UriTemplate = "/GetDepList", ResponseFormat = WebMessageFormat.Json)]
    List<string> GetDeptList();

    [OperationContract]
    [WebGet(UriTemplate = "/Authenticate?username={un}&password={psd}", ResponseFormat = WebMessageFormat.Json)]
    WCFEmployee getEmployee(String un,String psd);

    [OperationContract]
    [WebGet(UriTemplate = "/Requisition?DepartmentId={deptId}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFRequisition> getRequisition(String deptId);

    [OperationContract]
    [WebGet(UriTemplate = "/RequisitionDetail?RequisitionId={reqId}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFRequisitionDetail> getRequisitionDetails(string reqId);

    [OperationContract]
    [WebGet(UriTemplate = "/GetCP?DepartmentId={deptId}", ResponseFormat = WebMessageFormat.Json)]
    List<string> getCollectionPoint(string deptId);

    [OperationContract]
    [WebGet(UriTemplate = "/GetCT?DepartmentId={deptId}", ResponseFormat = WebMessageFormat.Json)]
    List<string> getCollectionTime(string deptId);

    [OperationContract]
    [WebGet(UriTemplate = "/UpdateRep?DepartmentId={deptId}&EmpName={EmpName}", ResponseFormat = WebMessageFormat.Json)]
    void UpdateRep(string deptId, string EmpName);

    [OperationContract]
    [WebGet(UriTemplate = "/Representatives", ResponseFormat = WebMessageFormat.Json)]
    List<string> getRepId();

    [OperationContract]
    [WebGet(UriTemplate = "/DepartmentHead?DepartmentId={deptId}", ResponseFormat = WebMessageFormat.Json)]
    WCFEmployee getDepHead(string deptId);

    [OperationContract]
    [WebGet(UriTemplate = "/ApproveRequisition?Comment={comment}&RequisitionId={reqId}", ResponseFormat = WebMessageFormat.Json)]
    string ApproveRequisition(string comment, string reqId);

    [OperationContract]
    [WebGet(UriTemplate = "/RejectRequisition?Comment={comment}&RequisitionId={reqId}", ResponseFormat = WebMessageFormat.Json)]
    string RejectRequisition(string comment, string reqId);

    [OperationContract]
    [WebGet(UriTemplate = "/EmpByDep?DepartmentId={deptId}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFEmployee> getEmpByDep(string deptId);

    [OperationContract]
    [WebGet(UriTemplate = "/DelegateEmp?DepartmentId={DeptId}&EmployeeName={EmpName}&StartDate={startDate}&EndDate={endDate}", ResponseFormat = WebMessageFormat.Json)]
    void delegateEmp(string DeptId, string EmpName, string startDate, string endDate);

    [OperationContract]
    [WebGet(UriTemplate = "/DeDelegateEmp?DepartmentId={DeptId}&EmployeeName={EmpName}", ResponseFormat = WebMessageFormat.Json)]
    void DedelegateEmp(string EmpName, string DeptId);

    [OperationContract]
    [WebGet(UriTemplate = "/PurchaseOrderList", ResponseFormat = WebMessageFormat.Json)]
    List<WCFPOList> getPOList();

    [OperationContract]
    [WebGet(UriTemplate = "/PurchaseOrderList/{PONumber}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFPODetails> getPODetails(string PONumber);

    [OperationContract]
    [WebGet(UriTemplate = "/PurchaseOrderList/{PONumber}/{status}/{commentBySupervisor=No Comment}", ResponseFormat = WebMessageFormat.Json)]
    string updatePO(string PONumber, string status, string commentBySupervisor);

    [OperationContract]
    [WebGet(UriTemplate = "/GetDelEmployee?DepartmentId={DeptId}", ResponseFormat = WebMessageFormat.Json)]
    WCFEmployee getDelEmployee(string DeptId);

    [OperationContract]
    [WebGet(UriTemplate = "/GetRetrievalList", ResponseFormat = WebMessageFormat.Json)]
    List<WCFRetrieval> getDataSource();

    [OperationContract]
    [WebGet(UriTemplate = "/GetBreakDownList?ItemName={itemName}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFBreakdown> getBreakDownList(string itemName);

    [OperationContract]
    [WebGet(UriTemplate = "/UpdateBreakDownList?ItemName={itemName}&Retrieved={retrieved}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFBreakdown> updateBreakDown(string itemName, int retrieved);

    [OperationContract]
    [WebGet(UriTemplate = "/GetDisList?DepartmentId={deptId}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFDisbursedInventory> getDisListByDepId(string deptId);

    [OperationContract]
    [WebGet(UriTemplate = "/GetDisbursementList?DepartmentId={deptId}", ResponseFormat = WebMessageFormat.Json)]
    List<WCFDisbursementBL> getDisbursementList(string deptId);

}

[DataContract]
public class WCFEmployee
{
    string empId;
    string departmentId;
    string empTitle;
    string empName;
    string email;
    int delegated;
    string delegateStartDate;
    string delegateEndDate;
    int auth;

    public WCFEmployee() { }

    public static WCFEmployee Make(string EmpId, string DepartmentId, string EmpTitle, string EmpName, string Email, int Delegate,int auth,string DelegateStartDate,string DelegateEndDate)
    {
        WCFEmployee e = new WCFEmployee();
        e.empId = EmpId;
        e.departmentId = DepartmentId;
        e.empTitle = EmpTitle;
        e.empName = EmpName;
        e.email = Email;
        e.delegated = Delegate;
        e.auth = auth;
        e.delegateStartDate = DelegateStartDate;
        e.delegateEndDate = DelegateEndDate;
     
        return e;
    }

    [DataMember]
    public string EmpId
    {
        get { return empId; }
        set { empId = value; }
    }

    [DataMember]
    public string DepartmentId
    {
        get { return departmentId; }
        set { departmentId = value; }
    }


    [DataMember]
    public string EmpTitle
    {
        get { return empTitle; }
        set { empTitle = value; }
    }

    [DataMember]
    public string EmpName
    {
        get { return empName; }
        set { empName = value; }
    }

    [DataMember]
    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    [DataMember]
    public int Delegate
    {
        get { return delegated; }
        set { delegated = value; }
    }

    [DataMember]
    public int Auth
    {
        get { return auth; }
        set { auth = value; }
    }

    [DataMember]
    public string DelegateStartDate
    {
        get { return delegateStartDate; }
        set { delegateStartDate = value; }
    }

    [DataMember]
    public string DelegateEndDate
    {
        get { return delegateEndDate; }
        set { delegateEndDate = value; }
    }

}


[DataContract]
public class WCFRequisition
{

    string requisitionId;
    string empName;
    string commentsEmp;
   

    [DataMember]
    public string RequisitionId
    {
        get { return requisitionId; }
        set { requisitionId = value; }
    }

    [DataMember]
    public string EmpName
    {
        get { return empName; }
        set { empName = value; }
    }

    [DataMember]
    public string CommentsEmp
    {
        get { return commentsEmp; }
        set { commentsEmp = value; }
    }


}



[DataContract]
public class WCFRequisitionDetail
{
    string itemName;
    string itemUOM;
    int quantity;

    [DataMember]
    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }


    [DataMember]
    public string ItemUOM
    {
        get { return itemUOM; }
        set { itemUOM = value; }
    }


    [DataMember]
    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }
}


[DataContract]
public class WCFPOList
{
    string pONumber;
    string generatedBy;
    string commentByClerk;

    public WCFPOList() { }

    [DataMember]
    public string PONumber
    {
        get
        {
            return pONumber;
        }

        set
        {
            pONumber = value;
        }
    }

    [DataMember]
    public string GeneratedBy
    {
        get
        {
            return generatedBy;
        }

        set
        {
            generatedBy = value;
        }
    }

    [DataMember]
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
}

[DataContract]
public class WCFPODetails
{
    string itemName;
    int? quantity;
    
    public WCFPODetails() { }


    [DataMember]
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

    [DataMember]
    public int? Quantity
    {
        get
        {
            return quantity;
        }

        set
        {
            quantity = value;
        }
    }

    
}


[DataContract]
public class WCFRetrieval
{
    string bin;
    string itemName;
    int needed;
    int retrieved;




    public static WCFRetrieval Make1(string bin, string itemName, int needed,int retrieved)
    {
        WCFRetrieval e = new WCFRetrieval();
        e.bin = bin;
        e.itemName = itemName;
        e.needed = needed;
        e.retrieved = retrieved;

        return e;

    }


    [DataMember]
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


    [DataMember]
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


    [DataMember]
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


    [DataMember]
    public int Retrieved
    {
        get
        {
            return retrieved;
        }

        set
        {
            retrieved = value;
        }

    }
}



[DataContract]
public class WCFBreakdown
{

    string deptName;
    int needed;
    int outstanding;
    int actual;



    public static WCFBreakdown Make2(string deptName, int needed, int outstanding, int actual)
    {
        WCFBreakdown e = new WCFBreakdown();
        e.deptName = deptName;
        e.outstanding = outstanding;
        e.needed = needed;
        e.actual = actual;

        return e;

    }


    [DataMember]
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


    [DataMember]
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

    [DataMember]
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

    [DataMember]
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

}



[DataContract]
public class WCFDisbursedInventory
{
    string itemName;
    string itemNumber;
    string itemUOM;
    int orderQuantity;
    int disbursementQuantity;
    int quantity;
    string reqId;





    public static WCFDisbursedInventory Make3(string itemName, string itemNumber, string itemUOM,  int orderQuantity, int disbursementQuantity, int quantity, string reqId)
    {
        WCFDisbursedInventory e = new WCFDisbursedInventory();
        e.itemName = itemName;
        e.itemNumber = itemNumber;
        e.itemUOM = itemUOM;
        e.orderQuantity = orderQuantity;
        e.disbursementQuantity = disbursementQuantity;
        e.quantity = quantity;
        e.reqId = reqId;

        return e;

    }


    [DataMember]
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

    [DataMember]
    public string ItemNumber
    {
        get
        {
            return itemNumber;
        }

        set
        {
            itemNumber = value;
        }

    }


    [DataMember]
    public string ItemUOM
    {
        get
        {
            return itemUOM;
        }

        set
        {
            itemUOM = value;
        }

    }


    [DataMember]
    public int OrderQuantity
    {
        get
        {
            return orderQuantity;
        }

        set
        {
            orderQuantity = value;
        }

    }

    [DataMember]
    public int DisbursementQuantity
    {
        get
        {
            return disbursementQuantity;
        }

        set
        {
            disbursementQuantity = value;
        }

    }

    [DataMember]
    public int Quantity
    {
        get
        {
            return quantity;
        }

        set
        {
            quantity = value;
        }

    }

    [DataMember]
    public string ReqId
    {
        get
        {
            return reqId;
        }

        set
        {
            reqId = value;
        }

    }
}

[DataContract]
public class WCFReadyForCollectionBO
{
    string depId;
    string itemNumber;
    string reqisitionId;
    int? disbursedQuantity;

    [DataMember]
    public string DepId
    {
        get
        {
            return depId;
        }

        set
        {
            depId = value;
        }
    }

    [DataMember]
    public string ItemNumber
    {
        get
        {
            return itemNumber;
        }

        set
        {
            itemNumber = value;
        }
    }

    [DataMember]
    public string ReqisitionId
    {
        get
        {
            return reqisitionId;
        }

        set
        {
            reqisitionId = value;
        }
    }

    [DataMember]
    public int? DisbursedQuantity
    {
        get
        {
            return disbursedQuantity;
        }

        set
        {
            disbursedQuantity = value;
        }
    }
  
    public static WCFReadyForCollectionBO Make3(string depId, string itemNumber, string reqisitionId, int? disbursedQuantity)
    {
        WCFReadyForCollectionBO e = new WCFReadyForCollectionBO();
        e.depId = depId;
        e.itemNumber = itemNumber;
        e.reqisitionId = reqisitionId;
        e.disbursedQuantity = disbursedQuantity;
        

        return e;

    }



  
}




[DataContract]
public class WCFDisbursementBL
{
    string itemName;
    string itemUOM;
    int? orderQuantity;
    int? outstandingQuantity;
    int? disbursementQuantity;

    [DataMember]
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

    [DataMember]
    public string ItemUOM
    {
        get
        {
            return itemUOM;
        }

        set
        {
            itemUOM = value;
        }
    }

    [DataMember]
    public int? OrderQuantity
    {
        get
        {
            return orderQuantity;
        }

        set
        {
            orderQuantity = value;
        }
    }

    [DataMember]
    public int? OutstandingQuantity
    {
        get
        {
            return outstandingQuantity;
        }

        set
        {
            outstandingQuantity = value;
        }
    }


    [DataMember]

    public int? DisbursementQuantity
    {
        get
        {
            return disbursementQuantity;
        }

        set
        {
            disbursementQuantity = value;
        }
    }


  
    public static WCFDisbursementBL Make4(string itemName, string itemUOM, int? orderQuantity, int? outstandingQuantity, int? disbursementQuantity)
    {
        WCFDisbursementBL e = new WCFDisbursementBL();
        e.itemName = itemName;
        e.itemUOM = itemUOM;
        e.orderQuantity = orderQuantity;
        e.outstandingQuantity = outstandingQuantity;
        e.disbursementQuantity = disbursementQuantity;


        return e;

    }

}













































