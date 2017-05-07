using System;
using Model;
using BusinessLogic;
using System.Web;

namespace SSIS
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        LoginBL bl = new LoginBL();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["employee"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            else
            {
                EmployeeBO ebo = (EmployeeBO)Session["employee"];
                EmployeeBO storeManager = bl.getStoreManagerBO();
                EmployeeBO storeSupervisor = bl.getStoreSupervisorBO();
                string path = HttpContext.Current.Request.Url.AbsolutePath;
                string employeeDelegate = Convert.ToString(bl.checkDelegate(ebo));
                string storeManagerDelegate = Convert.ToString(bl.checkDelegate(storeManager));
                string storeSupervisorDelegate = Convert.ToString(bl.checkDelegate(storeSupervisor));

                if (path.Equals("~/Department/AssignDepartmentRepresentative.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Head")
                        && !(ebo.EmployeeTitle.Equals("Employee")
                        && employeeDelegate.Equals("True")))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Head")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Department/CatalogueList.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Head")
                        && !ebo.EmployeeTitle.Equals("Employee"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Head")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Department/ChangeCollectionPoint.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Employee"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    if (!ebo.EmployeeId.Equals(ebo.Department.DeptRep))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Department/CreateRequisition.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Employee"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    if (employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Department/DepartmentDelegate.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Head"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Department/ReadyForCollectionList.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Employee"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    if (!ebo.EmployeeId.Equals(ebo.Department.DeptRep))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Department/RequisitionDetails.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Head")
                        && !ebo.EmployeeTitle.Equals("Employee"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Head")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Department/RequisitionList.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Head")
                        && !ebo.EmployeeTitle.Equals("Employee"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Head")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("/Store/AdjustmentVoucherDetails.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Manager")
                        && !ebo.EmployeeTitle.Equals("Supervisor")
                        && !ebo.EmployeeTitle.Equals("Clerk"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Manager")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Supervisor")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/AdjustmentVoucherList.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Manager")
                        && !ebo.EmployeeTitle.Equals("Supervisor")
                        && !ebo.EmployeeTitle.Equals("Clerk"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Manager")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Supervisor")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/BulkUpdateSuppliers.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Manager")
                        && !(ebo.EmployeeTitle.Equals("Supervisor")
                        && storeManagerDelegate.Equals("True")
                        && storeSupervisorDelegate.Equals("False")))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/CreateAdjustmentVoucher.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Clerk"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/CreatePO.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Clerk"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/DisbursementList.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Manager")
                        && !ebo.EmployeeTitle.Equals("Supervisor")
                        && !ebo.EmployeeTitle.Equals("Clerk"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Manager")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Supervisor")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/Report1.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Manager")
                        && !ebo.EmployeeTitle.Equals("Supervisor")
                        && !ebo.EmployeeTitle.Equals("Clerk"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Manager")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Supervisor")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/Report2.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Manager")
                        && !ebo.EmployeeTitle.Equals("Supervisor")
                        && !ebo.EmployeeTitle.Equals("Clerk"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Manager")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Supervisor")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/GenerateReport.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Manager")
                        && !ebo.EmployeeTitle.Equals("Supervisor")
                        && !ebo.EmployeeTitle.Equals("Clerk"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Manager")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Supervisor")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/InventoryList.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Manager")
                        && !ebo.EmployeeTitle.Equals("Supervisor")
                        && !ebo.EmployeeTitle.Equals("Clerk"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Manager")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Supervisor")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/PODetails.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Manager")
                        && !ebo.EmployeeTitle.Equals("Supervisor")
                        && !ebo.EmployeeTitle.Equals("Clerk"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Manager")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Supervisor")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/POList.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Manager")
                        && !ebo.EmployeeTitle.Equals("Supervisor")
                        && !ebo.EmployeeTitle.Equals("Clerk"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Manager")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Supervisor")
                        && employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/RetrievalList.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Clerk"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/StoreDelegate.aspx"))
                {
                    if (!ebo.EmployeeTitle.Equals("Manager")
                        && !ebo.EmployeeTitle.Equals("Supervisor"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Manager")
                        && storeSupervisorDelegate.Equals(1))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (ebo.EmployeeTitle.Equals("Supervisor")
                        && storeManagerDelegate.Equals(1))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }

                if (path.Equals("~/Store/UpdateSupplier.aspx"))
                {

                    if (!ebo.EmployeeTitle.Equals("Manager")
                        && !(ebo.EmployeeTitle.Equals("Supervisor")
                        && storeManagerDelegate.Equals("True")
                        && storeSupervisorDelegate.Equals("False")))
                    {
                        Response.Redirect("~/Error.aspx");
                    }

                    else if (employeeDelegate.Equals("True"))
                    {
                        Response.Redirect("~/Error.aspx");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            EmployeeBO ebo = (EmployeeBO)Session["employee"];

            lbEmployeeTitle.Text = ebo.EmployeeTitle;
            lbEmployeeId.Text = ebo.EmployeeId;
            lbEmployeeDelegate.Text = Convert.ToString(bl.checkDelegate(ebo));
            lbRepId.Text = ebo.Department.DeptRep;

            if (!ebo.Department.DeptId.Equals("STORE"))
            {
                EmployeeBO head = bl.getDeptHeadByDepId(ebo.EmployeeDept);
                lbHeadDelegate.Text = Convert.ToString(bl.checkDelegate(head));
            }

            else if (ebo.Department.DeptId.Equals("STORE"))
            {
                EmployeeBO storeManager = bl.getStoreManagerBO();
                EmployeeBO storeSupervisor = bl.getStoreSupervisorBO();
                lbStoreManagerDelegate.Text = Convert.ToString(bl.checkDelegate(storeManager));
                lbStoreSupervisorDelegate.Text = Convert.ToString(bl.checkDelegate(storeSupervisor));
            }
        }
    }
}