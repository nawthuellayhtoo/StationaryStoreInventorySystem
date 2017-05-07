using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using Model;
using System.Data;
using BusinessLogic.DepartmentBL;

namespace SSIS
{
    public partial class RequisitionDetails : System.Web.UI.Page
    {
        List<RetrievalRequisitionDetailsBO> rrdbo = new List<RetrievalRequisitionDetailsBO>();

        RequisitionDetailsBL rdbl = new RequisitionDetailsBL();
        RetrievalRequisitionListBO rBO = new RetrievalRequisitionListBO();
        CreateRequisitionBL crbl = new CreateRequisitionBL();

        DataTable dt = new DataTable();

        string status = "";
        string requisitionId = "";
        string quantity = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string requisitionId = Request["retrievalrequisitionId"].ToString();

                EmployeeBO employeeBO = (EmployeeBO)Session["employee"];


                dt.Columns.AddRange(new DataColumn[3]
                {
                    new DataColumn("retrievalItemName"),
                    new DataColumn("retrievalQuantity"),
                    new DataColumn("retrievalUOM")
                });

                rrdbo = rdbl.getRequisitionDetailsByReqId(requisitionId);

                foreach (RetrievalRequisitionDetailsBO r in rrdbo)
                {
                    dt.Rows.Add(r.RetrievalItemName, r.RetrievalQuantity, r.RetrievalUOM);
                }

                Session["RequisitionDetails"] = dt;
                GridViewRequisitionDetails.DataSource = dt;
                GridViewRequisitionDetails.DataBind();

                if (GridViewRequisitionDetails.Rows.Count != 0)
                {
                    fooTableSetting();
                }

                rBO = rdbl.getReqInfo(requisitionId);

                lbRQID.Text = rBO.RetrievalrequisitionId;
                lbRQID.Text = requisitionId;
                LbEmpName.Text = rBO.RetrievalempName;
                LbEmpRequisitionDate.Text = Convert.ToDateTime(rBO.RetrievalempRequisitionDate).ToString("dd MMMM, yyyy");
                LbStatus.Text = rBO.Retrievalstatus;

                if (rBO.Retrievalstatus == "Approved")
                {
                    btnResubmitDisabled();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox tb = (TextBox)GridViewRequisitionDetails.Rows[i].FindControl("retrievalQuantity");
                        tb.ReadOnly = true;
                    }
                    tbEmployeeComments.ReadOnly = true;
                    btnApproveDisabled();
                    btnRejectDisabled();
                }

                if (rBO.Retrievalstatus == "Received")
                {
                    btnResubmitDisabled();
                    btnRejectDisabled();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox tb = (TextBox)GridViewRequisitionDetails.Rows[i].FindControl("retrievalQuantity");
                        tb.ReadOnly = true;
                    }
                    tbEmployeeComments.ReadOnly = true;
                    btnApproveDisabled();
                }


                if (rBO.Retrievalstatus == "Rejected")
                {
                    btnRejectDisabled();
                    btnApproveDisabled();
                    btnResubmitDisabled();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox tb = (TextBox)GridViewRequisitionDetails.Rows[i].FindControl("retrievalQuantity");
                        tb.ReadOnly = true;
                    }
                    tbEmployeeComments.ReadOnly = true;
                    tbDepartmentHeadComments.ReadOnly = true;
                }
                tbEmployeeComments.Text = rdbl.getEmpComments(requisitionId);


                if (employeeBO.EmployeeTitle == "Employee" && employeeBO.Delegate == 1)
                {
                    btnResubmitDisabled();
                    tbEmployeeComments.ReadOnly = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox tb = (TextBox)GridViewRequisitionDetails.Rows[i].FindControl("retrievalQuantity");
                        tb.ReadOnly = true;
                    }
                }
                else if (employeeBO.EmployeeTitle == "Employee" && employeeBO.Delegate == 0)
                {
                    btnApproveDisabled();
                    btnRejectDisabled();
                    tbDepartmentHeadComments.ReadOnly = true;
                }
                else if (employeeBO.EmployeeTitle == "Head")
                {
                    btnResubmitDisabled();
                    tbEmployeeComments.ReadOnly = true;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox tb = (TextBox)GridViewRequisitionDetails.Rows[i].FindControl("retrievalQuantity");
                        tb.ReadOnly = true;
                    }
                }
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            status = "Rejected";
            requisitionId = Request["retrievalrequisitionId"].ToString();
            rdbl.updateRequisitionStatus(requisitionId, status);
            LbStatus.Text = status;
            btnApprove.Enabled = true;
            btnApprove.Visible = true;
            btnReject.Enabled = false;
            btnReject.Visible = false;

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            status = "Approved";
            requisitionId = Request["retrievalrequisitionId"].ToString();
            rdbl.updateRequisitionStatus(requisitionId, status);
            LbStatus.Text = status;
            btnReject.Enabled = true;
            btnReject.Visible = true;
            btnApprove.Enabled = false;
            btnApprove.Visible = false;
        }

        protected void btnResubmit_Click(object sender, EventArgs e)
        {
            EmployeeBO ebo = (EmployeeBO)Session["employee"];
            requisitionId = Request["retrievalrequisitionId"].ToString();
            dt = (DataTable)Session["RequisitionDetails"];

            string sub = "Changing Requisition";
            string name = crbl.getHeadNameByDepId(ebo.Department.DeptId);
            string body = "Dear " + name + ", \n"
                       + "\n" + "I have changed my requisition \n below is the details: \n";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                quantity = ((TextBox)GridViewRequisitionDetails.Rows[i].FindControl("retrievalQuantity")).Text;
                string itemName = (string)dt.Rows[i][0];
                string requisitionDetailsId = rdbl.getRequisitionDetailsIdByItemName(requisitionId, itemName);
                int updatedQty = Convert.ToInt32(quantity);



                string itemNumber = crbl.getItemNumberByItemName(itemName);

                if (updatedQty != 0)
                {
                    body += "Item Name: " + itemName + ":\t Quantity: " + updatedQty + "\n";
                    rdbl.resubmitRequisition(requisitionId, tbEmployeeComments.Text);
                    rdbl.resubmitRequisitionDetails(requisitionDetailsId, updatedQty);
                }
                else
                {
                    body += "Previous Requisition for " + itemName + " is no longer needed.";
                    rdbl.deleteRequisition(requisitionId);
                }
            }

            body += "\n\n" + "Regards,"
                           + "\n" + ebo.EmployeeName;
            SendEmail sendEmail = new SendEmail();
            sendEmail.sendCPEmail(sub, body, crbl.getHeadEmailByDepId(ebo.Department.DeptId));

            Response.Redirect("~/Department/RequisitionList.aspx");
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Department/RequisitionList.aspx");
        }

        public void btnResubmitDisabled()
        {
            btnResubmit.Enabled = false;
            btnResubmit.Visible = false;
        }

        public void btnApproveDisabled()
        {
            btnApprove.Enabled = false;
            btnApprove.Visible = false;
        }

        public void btnRejectDisabled()
        {
            btnReject.Enabled = false;
            btnReject.Visible = false;
        }

        public void fooTableSetting()
        {
            //Attribute to show the Plus Minus Button.
            GridViewRequisitionDetails.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            GridViewRequisitionDetails.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            GridViewRequisitionDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}