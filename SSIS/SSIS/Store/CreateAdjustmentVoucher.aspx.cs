using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessLogic.StoreBL;
using System.Collections;
using Model;

namespace SSIS
{
    public partial class CreateAdjustmentVoucher : System.Web.UI.Page
    {
        ArrayList al;
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
                        CreateAdjustmentVoucherBL bl = new CreateAdjustmentVoucherBL();
                        //Category Control
                        ddlCategory.DataSource = bl.getDistinctCategory();
                        ddlCategory.DataBind();
                        ddlCategory.Items.Insert(0, new ListItem("-Select-", "NA"));

                        //ItemName Control
                        ddlItemName.Enabled = false;
                        ddlItemName.Width = 250;

                        //Textbox Control
                        tbUOM.Text = bl.getUOM(ddlCategory.SelectedValue);
                        int defaultQuantity = 0;
                        tbQuantity.Text = defaultQuantity.ToString();

                        //Supplier Control
                        ddlSupplier.Enabled = false;
                        ddlSupplier.Width = 200;
                        btnSubmit.Visible = false;

                        //Session for GridView
                        al = new ArrayList();  // initialization for ArrayList
                        Session["AdjustmentDetails"] = al; // Stores array into Session Object

                        gvInventoryAdjustmentList.Columns[0].Visible = false; // Hide gridview column
                        gvInventoryAdjustmentList.Columns[1].Visible = false; // Hide gridview column
                    }
                }
                catch (Exception x)
                {
                    System.Diagnostics.Debug.WriteLine(x);
                }
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Instantiate BL Object
                CreateAdjustmentVoucherBL bl = new CreateAdjustmentVoucherBL();
                //Item Name dropdownlist
                ddlItemName.DataTextField = "ItemName";
                ddlItemName.DataValueField = "ItemNumber";

                ddlItemName.DataSource = bl.getItemListByCategory(ddlCategory.SelectedValue);
                ddlItemName.DataBind();
                ddlItemName.Enabled = true;
                ddlItemName.Items.Insert(0, new ListItem("-Select-", "NA"));
                //Unit of Measure Textbox
                tbUOM.Text = bl.getUOM(ddlCategory.SelectedValue);

                //Clear Controls
                if (ddlCategory.SelectedValue.Equals("NA"))
                {
                    tbUOM.Text = String.Empty;
                    ddlItemName.Enabled = false;
                    ddlSupplier.Items.Clear();
                    ddlSupplier.Enabled = false;
                    tbQuantity.Text = String.Empty;
                }

                // Check to bind footable
                if (gvInventoryAdjustmentList.Rows.Count != 0)
                {
                    footableSettings();
                }
            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x);
            }
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CreateAdjustmentVoucherBL bl = new CreateAdjustmentVoucherBL();
                ddlSupplier.Enabled = true;
                //Bind the Supplier name into the dropdown and price of their item into value
                string ItemNumber = ddlItemName.SelectedValue;
                ArrayList al = new ArrayList();
                al.Add(bl.getSupplier1(ItemNumber));
                al.Add(bl.getSupplier2(ItemNumber));
                al.Add(bl.getSupplier3(ItemNumber));
                //Supplier Name dropdownlist
                ddlSupplier.DataTextField = "Supplier1";
                ddlSupplier.DataValueField = "Price1";

                ddlSupplier.DataSource = al;
                ddlSupplier.DataBind();

                // Check to bind footable
                if (gvInventoryAdjustmentList.Rows.Count != 0)
                {
                    footableSettings();
                }
            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x);
            }
        }

        protected void Clear() //Clear Everything.
        {
            ddlCategory.SelectedIndex = 0;
            ddlItemName.Items.Clear();
            tbUOM.Text = String.Empty;
            ddlItemName.Enabled = false;
            ddlSupplier.Items.Clear();
            ddlSupplier.Enabled = false;
            tbQuantity.Text = String.Empty;
            tbReason.Text = String.Empty;
            btnSubmit.Visible = false;
            Session.Remove("AdjustmentDetails");
            gvInventoryAdjustmentList.DataSource = al;
            gvInventoryAdjustmentList.DataBind();
            al = new ArrayList(); // bring arraylist back to life
            Session["AdjustmentDetails"] = al; // store it inside the session
        }
        protected void ClearControls() // Clear Controls on Adding
        {
            ddlCategory.SelectedIndex = 0;
            ddlItemName.Items.Clear();
            tbUOM.Text = String.Empty;
            ddlItemName.Enabled = false;
            ddlSupplier.Items.Clear();
            ddlSupplier.Enabled = false;
            tbQuantity.Text = String.Empty;
            tbReason.Text = String.Empty;
        }

        protected void footableSettings()
        {
            //Attribute to show the Plus Minus Button.
            gvInventoryAdjustmentList.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvInventoryAdjustmentList.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvInventoryAdjustmentList.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            gvInventoryAdjustmentList.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
            gvInventoryAdjustmentList.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
            //Adds THEAD and TBODY to GridView.
            gvInventoryAdjustmentList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (gvInventoryAdjustmentList.Rows.Count == 0) // check if gridview is empty
            {
                addToGridView(); // call add to gridview
            }
            else if (gvInventoryAdjustmentList.Rows.Count == 1) //check if there is one item in gridview
            {
                Label lbItemNumber = (Label)gvInventoryAdjustmentList.Rows[0].FindControl("lbItemNumber"); 
                //get itemnumer from gridview hidden field
                if (lbItemNumber.Text.Equals(ddlItemName.SelectedValue))
                {
                    ArrayList al = (ArrayList)Session["AdjustmentDetails"];
                    AdjustmentInfoBO abo = new AdjustmentInfoBO();
                    abo.ItemNumber = ddlItemName.SelectedValue;
                    abo.ItemName = ddlItemName.SelectedItem.Text;
                    abo.SupplierId = ddlSupplier.SelectedItem.Text;
                    int qty = Convert.ToInt32(tbQuantity.Text);
                    abo.QuantityAdjustment = qty;
                    abo.Uom = tbUOM.Text.Trim();
                    abo.Reason = tbReason.Text.Trim();
                    double price = Convert.ToInt32(ddlSupplier.SelectedValue) * qty;
                    if (price < 0) { price = price * -1; }
                    abo.Price = price.ToString();
                    //get all the item from fields

                    al.RemoveAt(0); //remove item from arraylist
                    al.Add(abo); //add the changed item into arraylist

                    gvInventoryAdjustmentList.DataSource = al;
                    gvInventoryAdjustmentList.DataBind();

                    //apply the footable settings
                    footableSettings();
                }
                else
                {
                    addToGridView();
                }
            }
            else if (gvInventoryAdjustmentList.Rows.Count > 1) // if gridview has more than 1 item
            {
                for (int i = 1; i <= gvInventoryAdjustmentList.Rows.Count; i++)
                {
                    Label lbItemNumber = (Label)gvInventoryAdjustmentList.Rows[i].FindControl("lbItemNumber");
                    if (lbItemNumber.Text.Equals(ddlItemName.SelectedValue))
                    {
                        ArrayList al = (ArrayList)Session["AdjustmentDetails"];
                        AdjustmentInfoBO abo = new AdjustmentInfoBO();
                        abo.ItemNumber = ddlItemName.SelectedValue;
                        abo.ItemName = ddlItemName.SelectedItem.Text;
                        abo.SupplierId = ddlSupplier.SelectedItem.Text;
                        int qty = Convert.ToInt32(tbQuantity.Text);
                        abo.QuantityAdjustment = qty;
                        abo.Uom = tbUOM.Text.Trim();
                        abo.Reason = tbReason.Text.Trim();
                        double price = Convert.ToInt32(ddlSupplier.SelectedValue) * qty;
                        if (price < 0) { price = price * -1; }
                        abo.Price = price.ToString();

                        al.RemoveAt(i);
                        al.Add(abo);
                        // process same as the condition above
                        gvInventoryAdjustmentList.DataSource = al;
                        gvInventoryAdjustmentList.DataBind();
                        //apply the footable settings
                        footableSettings();
                        break; // break the for loop if work is performed
                    }
                    else
                    {
                        addToGridView(); // at normal condition if no duplicate, do this
                    }
                }
            }
            ClearControls();
        }

        protected void addToGridView()
        {
            try
            { //adding under normal condition, wrote this method so that code will not repeat
                ArrayList al = (ArrayList)Session["AdjustmentDetails"];
                AdjustmentInfoBO abo = new AdjustmentInfoBO();
                abo.ItemNumber = ddlItemName.SelectedValue;
                abo.ItemName = ddlItemName.SelectedItem.Text;
                abo.SupplierId = ddlSupplier.SelectedItem.Text;
                int qty = Convert.ToInt32(tbQuantity.Text);
                abo.QuantityAdjustment = qty;
                abo.Uom = tbUOM.Text.Trim();
                abo.Reason = tbReason.Text.Trim();
                double price = Convert.ToInt32(ddlSupplier.SelectedValue) * qty;
                if (price < 0) { price = price * -1; }
                abo.Price = price.ToString();

                al.Add(abo);

                gvInventoryAdjustmentList.DataSource = al;
                gvInventoryAdjustmentList.DataBind();
                //apply the footable settings
                footableSettings();
                btnSubmit.Visible = true;

            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x);
            }
        }
        protected void gvInventoryAdjustmentList_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove") //if gridview (Remove) command is clicked
            {
                if (gvInventoryAdjustmentList.Rows.Count == 1)
                {
                    //array session
                    ArrayList al = (ArrayList)Session["AdjustmentDetails"];
                    int index = Convert.ToInt32(e.CommandArgument);
                    //remove the item from the array
                    al.RemoveAt(index);
                    Session["AdjustmentDetails"] = al;
                    gvInventoryAdjustmentList.DataSource = null;
                    gvInventoryAdjustmentList.DataBind();
                    btnSubmit.Visible = false;
                }
                else if (gvInventoryAdjustmentList.Rows.Count > 1)
                {
                    //array session
                    ArrayList al = (ArrayList)Session["AdjustmentDetails"];
                    int index = Convert.ToInt32(e.CommandArgument);
                    //remove the item from the array
                    al.RemoveAt(index);

                    //bind the gridview again
                    gvInventoryAdjustmentList.DataSource = al;
                    gvInventoryAdjustmentList.DataBind();

                    //apply the footable settings
                    footableSettings();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                CreateAdjustmentVoucherBL bl = new CreateAdjustmentVoucherBL();
                EmployeeBO ebo = (EmployeeBO)Session["employee"];
                string empId = ebo.EmployeeId;
                double totalPrice = 0;
                for (int r = 0; r < gvInventoryAdjustmentList.Rows.Count; r++)
                {
                    Label lbPrice = (Label)gvInventoryAdjustmentList.Rows[r].FindControl("lbPrice");
                    totalPrice += Convert.ToDouble(lbPrice.Text);
                }

                if (bl.createAdjustmentVoucher(empId, totalPrice) == true)
                {//if the insert for the Adjustment voucher is successful the perform the following
                    string voucherNum = bl.getAdjustmentVoucherId();
                    for (int r = 0; r < gvInventoryAdjustmentList.Rows.Count; r++)
                    {
                        string getADNum = bl.getAdjustmentDetailsVoucherId();
                        int calnum = getADNum.Length - 3;//get old primary key
                        int increADV = Convert.ToInt32(getADNum.Substring(3, calnum)) + 1;
                        string adNum = "AD0" + increADV.ToString(); //auto increment primary key
                        System.Diagnostics.Debug.Write(adNum);
                        Label lbItemNumber = (Label)gvInventoryAdjustmentList.Rows[r].FindControl("lbItemNumber");
                        bl.createAdjustmentVoucherDetails(adNum, voucherNum, lbItemNumber.Text, Convert.ToInt32(gvInventoryAdjustmentList.Rows[r].Cells[4].Text),
                        gvInventoryAdjustmentList.Rows[r].Cells[3].Text, gvInventoryAdjustmentList.Rows[r].Cells[6].Text); //insert into database
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Adjustment Voucher created successfully.')", true);

                    Clear(); //clear session and everything
                    Response.Redirect("~/Store/AdjustmentVoucherList.aspx");
                }
                else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Adjustment Voucher not created. Please try again.')", true); }

                //apply the footable settings
                footableSettings();
            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x);
            }
        }
    }
}