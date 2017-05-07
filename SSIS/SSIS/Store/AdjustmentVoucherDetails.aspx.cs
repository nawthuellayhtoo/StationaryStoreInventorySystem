using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.StoreBL;
using Model;
using System.Data;

namespace SSIS
{
    public partial class AdjustmentVoucherDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["employee"] != null)
                    {
                        EmployeeBO ebo = (EmployeeBO)Session["employee"];
                        string empId = ebo.EmployeeId;
                        string title = ebo.EmployeeTitle;

                        string poId = Request.QueryString["VoucherId"]; // pass voucherid from another page
                        lbVoucherID.Text = poId;

                        AdjustmentVoucherListDetailsBL avldBL = new AdjustmentVoucherListDetailsBL();

                        List<AdjustmentBO> abo = new List<AdjustmentBO>();
                        abo = avldBL.getAdjustmentVoucherListById(poId);
                        //get list of adjustment voucher list by voucher id
                        foreach (AdjustmentBO item in abo)
                        {
                            lbApplierName.Text = item.Employee.EmployeeName;
                            lbDateValue.Text = String.Format("{0:dd/MMM/yyyy}", item.VoucherDate);
                            lbCostValue.Text = item.TotalPrice.ToString();
                            lbStatusValue.Text = item.AdjustmentStatus;
                        }

                        DataTable dt = new DataTable();
                        dt.Columns.AddRange(new DataColumn[4] {
                            new DataColumn("Item Number",typeof(string)),
                            new DataColumn("Item Name",typeof(string)),
                            new DataColumn("Quantity Adjustment",typeof(string)),
                            new DataColumn("Reason",typeof(string))});

                        List<AdjustmentDetailsBO> avList = avldBL.getAdjustmentVoucherListDetails(poId);
                        foreach (AdjustmentDetailsBO item in avList)
                        {
                            dt.Rows.Add(item.ItemNumber, item.Item.ItemName, item.QuantityAdjustment, item.Reason);
                        }

                        gvInventoryAdjustmentDetailsList.DataSource = dt;
                        gvInventoryAdjustmentDetailsList.DataBind();

                        // Check to bind footable
                        if (gvInventoryAdjustmentDetailsList.Rows.Count != 0)
                        {
                            footableSettings();
                        }

                        if (title.Equals("Manager") || title.Equals("Supervisor"))
                        {
                            btnApprove.Visible = true;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                        }
                        bool isDelegate = avldBL.checkDelegate(title);
                        if (isDelegate == true)
                        {
                            if (lbStatusValue.Text.Equals("Approved"))
                            {
                                disableBtn();
                            }
                        }
                        else if (isDelegate == false)
                        {
                            string check = avldBL.checkSupApproval(lbVoucherID.Text); // checking about delegate.
                            if (title.Equals("Manager"))
                            {
                                if (lbStatusValue.Text.Equals("Approved"))
                                {
                                    disableBtn();
                                }
                            }
                            else if (title.Equals("Supervisor"))
                            {
                                if (lbStatusValue.Text.Equals("Approved") || check.Equals("Yes"))
                                {
                                    disableBtn();
                                }
                            }
                        }

                    }
                }
                catch (Exception x)
                {
                    System.Diagnostics.Debug.WriteLine(x);
                }
            }
        }

        protected void disableBtn() //show button is disable and change its color when approved
        {
            btnApprove.Enabled = false;
            btnApprove.Visible = false;
            btnApprove.CssClass = "btn btn-default btn-md";
            btnApprove.BackColor = System.Drawing.Color.Gray;
        }
        protected void footableSettings()
        {
            //Attribute to show the Plus Minus Button.
            gvInventoryAdjustmentDetailsList.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvInventoryAdjustmentDetailsList.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            gvInventoryAdjustmentDetailsList.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            //Adds THEAD and TBODY to GridView.
            gvInventoryAdjustmentDetailsList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/AdjustmentVoucherList.aspx");
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["employee"] != null)
                {
                    EmployeeBO ebo = (EmployeeBO)Session["employee"];
                    string empId = ebo.EmployeeId;
                    string title = ebo.EmployeeTitle;
                    AdjustmentVoucherListDetailsBL avldBL = new AdjustmentVoucherListDetailsBL();
                    bool isDelegate = avldBL.checkDelegate("Manager");
                    if (isDelegate == true)
                    {
                        bool check = avldBL.updateStatusDelegate(lbVoucherID.Text, "Manager", lbApplierName.Text, lbCostValue.Text);
                        // - @param1 : this is for updating selected voucher
                        // - @param2 : check who update the selected voucher
                        // - @param3 : for email purposes
                        if (check == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Adjustment Voucher has been Approved.')", true);
                            disableBtn();
                            lbStatusValue.Text = "Approved";
                        }
                        else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Approve voucher failed. Please try again.')", true); }
                    }
                    else if (isDelegate == false)
                    {
                        if (title.Equals("Manager"))// manager login
                        {
                            bool check = avldBL.updateStatus(lbVoucherID.Text, "Manager", lbApplierName.Text, lbCostValue.Text);
                            // - @param1 : this is for updating selected voucher
                            // - @param2 : check who update the selected voucher
                            // - @param3 : for email purposes
                            if (check == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Adjustment Voucher has been Approved.')", true);
                                disableBtn();
                                lbStatusValue.Text = "Approved";
                            }
                            else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Approve voucher failed. Please try again.')", true); }

                        }
                        else if (title.Equals("Supervisor")) // supervisor login
                        {
                            bool check = avldBL.updateStatus(lbVoucherID.Text, "Supervisor", lbApplierName.Text, lbCostValue.Text);
                            if (check == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Adjustment Voucher has been Approved.')", true);
                                disableBtn();
                                lbStatusValue.Text = "Approved";
                            }
                            else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Approve voucher failed. Please try again.')", true); }
                        }
                    }
                }
                // Check to bind footable
                if (gvInventoryAdjustmentDetailsList.Rows.Count != 0)
                {
                    footableSettings();
                }
            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x);
            }
        }
    }
}