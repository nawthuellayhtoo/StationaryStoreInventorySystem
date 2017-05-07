using BusinessLogic;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIS
{
    public partial class PODetails : System.Web.UI.Page
    {
        PurchaseOrderBL purchaseOrderBL;
        string poId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                doProcess();
            }
        }

        public void doProcess()
        {
            if (Session["employee"] != null)
            {
                EmployeeBO ebo = (EmployeeBO)Session["employee"];
                string title = ebo.EmployeeTitle;
                int delegateValue = ebo.Delegate;
                poId = Request.QueryString["poId"];
                lblPOID.Text = poId;
                lblName.Text = ebo.EmployeeName;
                purchaseOrderBL = new PurchaseOrderBL();
                PurchaseOrderBO pbo = purchaseOrderBL.getStatusAndOrderDateDetails(poId);
                lblOrderDate.Text = String.Format("{0:dd/MMM/yyyy}", pbo.DateOfOrder);
                lblStatus.Text = pbo.Status;
                tbClerkComments.Text = pbo.CommentByClerk;
                tbSupervisorComments.Text = pbo.CommentBySupervisor;

                if (title.Equals("Supervisor") && (lblStatus.Text.Equals("Pending")))
                {
                    btnResubmit.Visible = false;
                    this.gvPurchaseOrderList.Columns[7].Visible = false;
                    tbClerkComments.Enabled = false;
                }
                else if (title.Equals("Manager") && (lblStatus.Text.Equals("Pending")) && delegateValue == 1)
                {
                    btnResubmit.Visible = false;
                    this.gvPurchaseOrderList.Columns[7].Visible = false;
                    tbClerkComments.Enabled = false;
                }
                else if (title.Equals("Clerk") && lblStatus.Text.Equals("Pending"))
                {
                    btnResubmit.Enabled = true;
                    tbSupervisorComments.Enabled = false;
                    this.gvPurchaseOrderList.Columns[7].Visible = true;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                }
                else
                {
                    this.gvPurchaseOrderList.Columns[7].Visible = false;
                    tbClerkComments.Enabled = false;
                    tbSupervisorComments.Enabled = false;
                    btnResubmit.Visible = false;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                }
                List<PurchaseOrderDetailsJoinBO> poDetailList = purchaseOrderBL.getPurchaseOrderDetailsList(poId);

                if (poDetailList.Count != 0)
                {
                    gvPurchaseOrderList.DataSource = poDetailList;
                    gvPurchaseOrderList.DataBind();
                    footableSettings();
                }
            }
            else
            {
                Console.WriteLine("Something is wrong!!!!!!");
            }
        }


        protected void gvPurchaseOrderList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPurchaseOrderList.EditIndex = e.NewEditIndex;
            doProcess();
        }

        protected void gvPurchaseOrderList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPurchaseOrderList.EditIndex = -1;
            doProcess();
        }

        protected void gvPurchaseOrderList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvPurchaseOrderList.Rows[e.RowIndex];
            poId = Request.QueryString["poId"];
            string itemNumber = (row.FindControl("lblItemNumber") as Label).Text;
            string quantity = (row.FindControl("txtQuantity") as TextBox).Text;
            int quan = Convert.ToInt32(quantity);
            purchaseOrderBL = new PurchaseOrderBL();

            purchaseOrderBL.updateQuantity(poId, quan, itemNumber);
            gvPurchaseOrderList.DataSource = purchaseOrderBL.getPurchaseOrderDetailsList(poId);
            gvPurchaseOrderList.EditIndex = -1;
            gvPurchaseOrderList.DataBind();
            footableSettings();
        }

        protected void gvPurchaseOrderList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            GridViewRow row = gvPurchaseOrderList.Rows[e.RowIndex];
            poId = Request.QueryString["poId"];
            string supplier = (row.FindControl("lblSupplier") as Label).Text;
            purchaseOrderBL = new PurchaseOrderBL();
            if (confirmValue == "Yes")
            {
                int id = purchaseOrderBL.deletePurchaseOrderDetails(poId, supplier);
                if (id == 1)
                {
                    Response.Redirect("~/Store/POList.aspx");
                }
                else
                {
                    gvPurchaseOrderList.DataSource = purchaseOrderBL.getPurchaseOrderDetailsList(poId);
                    gvPurchaseOrderList.EditIndex = -1;
                    gvPurchaseOrderList.DataBind();
                    footableSettings();
                }
            }
            else
            {
                gvPurchaseOrderList.DataSource = purchaseOrderBL.getPurchaseOrderDetailsList(poId);
                gvPurchaseOrderList.EditIndex = -1;
                gvPurchaseOrderList.DataBind();
                footableSettings();
            }
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/POList.aspx");
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            purchaseOrderBL = new PurchaseOrderBL();
            poId = Request.QueryString["poId"];
            string status = "Approved";
            string supervisorComment = tbSupervisorComments.Text;
            EmployeeBO ebo = (EmployeeBO)Session["employee"];
            string approverId = ebo.EmployeeId;
            Boolean flag = purchaseOrderBL.updateStatusByPoId(poId, status, supervisorComment, approverId);
            int value = 1;
            if (flag)
            {
                SendEmailApproveReject(poId, value);
            }
            else
            {
                string script = "alert('There is some issue in sending email. We apologize for this!');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
            Response.Redirect("~/Store/POList.aspx");
            List<PurchaseOrderBO> poboList = purchaseOrderBL.getPurchaseOrderListAll();
            if (poboList.Count!=0)
            {
                gvPurchaseOrderList.DataSource = poboList;
                gvPurchaseOrderList.DataBind();
                footableSettings();
            }
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            purchaseOrderBL = new PurchaseOrderBL();
            poId = Request.QueryString["poId"];
            string status = "Rejected";
            string supervisorComment = tbSupervisorComments.Text;
            EmployeeBO ebo = (EmployeeBO)Session["employee"];
            string approverId = ebo.EmployeeId;
            Boolean flag = purchaseOrderBL.updateStatusByPoId(poId, status, supervisorComment, approverId);
            int value = 2;
            if (flag)
            {
                SendEmailApproveReject(poId, value);
            }
            else
            {
                string script = "alert('There is some issue in sending email. We apologize for this!');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
            Response.Redirect("~/Store/POList.aspx");

            gvPurchaseOrderList.DataSource = purchaseOrderBL.getPurchaseOrderListAll();
            gvPurchaseOrderList.DataBind();
            footableSettings();
        }

        protected void btnResubmit_Click(object sender, EventArgs e)
        {

            purchaseOrderBL = new PurchaseOrderBL();
            poId = Request.QueryString["poId"];
            string clerkComment = tbClerkComments.Text;
            EmployeeBO ebo = (EmployeeBO)Session["employee"];
            string empId = ebo.EmployeeId;

            Boolean flag = purchaseOrderBL.resubmitPO(poId, clerkComment);

            if (flag)
            {
                SendEmailResubmit(poId);
            }
            else
            {
                string script = "alert('There is some issue in sending email. We apologize for this!');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
            Response.Redirect("~/Store/POList.aspx");

            gvPurchaseOrderList.DataSource = purchaseOrderBL.getPurchaseOrderList(empId);
            gvPurchaseOrderList.DataBind();
            footableSettings();
        }

        public void SendEmailResubmit(string poId)
        {
            EmployeeBO ebo = (EmployeeBO)Session["employee"];
            EmployeeBO empBo = purchaseOrderBL.getSupervisorId();
            SendEmail se = new SendEmail();
            string sub = "Purchase Order Resubmitted";
            string body = "Dear " + empBo.EmployeeName + ", \n"
                        + "\n" + "The Purchase Order with ID: " + poId + " has been resubmitted by" + ebo.EmployeeName + " and is waiting for approval."
                        + "\n" + "(This is an auto-generated email, please do not reply.)"
                        + "\n\n\n" + "Regards,"
                        + "\n" + "Admin";
            se.sendCPEmail(sub, body, ebo.EmployeeEmail);
        }

        public void SendEmailApproveReject(string poId, int value)
        {
            purchaseOrderBL = new PurchaseOrderBL();
            PurchaseOrderBO po = purchaseOrderBL.getStatusAndOrderDateDetails(poId);
            string requestorId = po.RequestorId;
            EmployeeBO emp = purchaseOrderBL.getRequestorName(requestorId);
            SendEmail se = new SendEmail();
            string subject = "";
            string body = "";
            if (value == 1)
            {
                subject = "Purchase Order Approved";
                body = "Dear " + emp.EmployeeName + ", \n"
                            + "\n" + "The Purchase Order with ID: " + poId + " requested by you has been approved."
                            + "\n" + "(This is an auto-generated email, please do not reply.)"
                            + "\n\n\n" + "Regards,"
                            + "\n" + "Admin";
            }
            else
            {
                subject = "Purchase Order Rejected";
                body = "Dear " + emp.EmployeeName + ", \n"
                            + "\n" + "The Purchase Order with ID: " + poId + " requested by you has been rejected. Please contact your supervisor for further information"
                            + "\n" + "(This is an auto-generated email, please do not reply.)"
                            + "\n\n\n" + "Regards,"
                            + "\n" + "Admin";
            }
            se.sendCPEmail(subject, body, emp.EmployeeEmail);
        }
        protected void footableSettings()
        {
            gvPurchaseOrderList.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvPurchaseOrderList.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvPurchaseOrderList.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            gvPurchaseOrderList.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
            gvPurchaseOrderList.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
            //Adds THEAD and TBODY to GridView.
            gvPurchaseOrderList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}