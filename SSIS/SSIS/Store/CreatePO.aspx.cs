using BusinessLogic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIS
{
    public partial class CreatePO : System.Web.UI.Page
    {
        PurchaseOrderBL purchaseOrderBL;
        string poCountStr;
        string podIdCountStr;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            int count = Convert.ToInt32(Session["count"]);
            if (!IsPostBack)
            {
                Page.DataBind();
                purchaseOrderBL = new PurchaseOrderBL();
                ddlCategory.DataSource = purchaseOrderBL.getCategoryList();
                ddlCategory.DataBind();

                ListItem liCategory = new ListItem("Select Category", "-1");
                ddlCategory.Items.Insert(0, liCategory);

                ListItem liItemName = new ListItem("Select Item Name", "-1");
                ddlItemName.Items.Insert(0, liItemName);

                ListItem liSupplier = new ListItem("Select Supplier", "-1");
                ddlSupplier.Items.Insert(0, liSupplier);

                ddlItemName.Enabled = false;
                ddlSupplier.Enabled = false;
                AddDefaultFirstRecord();
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(Session["count"]);
            if (ddlCategory.SelectedIndex == 0 && count == 0)
            {
                ddlItemName.Enabled = false;
                ddlItemName.SelectedIndex = 0;

                ddlSupplier.Enabled = false;
                ddlSupplier.SelectedIndex = 0;

                tbUOM.Text = "";
                tbPrice.Text = "";
                tbQuantity.Text = "";
            }
            else if (ddlCategory.SelectedIndex == 0 && count > 0)
            {
                ddlItemName.Enabled = false;
                ddlItemName.SelectedIndex = 0;

                ddlSupplier.Enabled = false;
                ddlSupplier.SelectedItem.Text = (String)Session["ddlValue"];

                tbUOM.Text = "";
                tbPrice.Text = "";
                tbQuantity.Text = "";
            }
            else if (ddlCategory.SelectedIndex != 0 && count > 0)
            {
                ddlItemName.Enabled = true;
                purchaseOrderBL = new PurchaseOrderBL();
                string category = ddlCategory.SelectedItem.Text;
                string supplier = (String)Session["ddlValue"];

                List<InventoryStockBO> list = purchaseOrderBL.getItemListByCategoryAndSupplier(category, supplier);
                List<String> l1 = purchaseOrderBL.getItemNameList(list);
                ddlItemName.DataSource = l1;
                ddlItemName.DataBind();

                ListItem liItemName = new ListItem("Select Item Name", "-1");
                ddlItemName.Items.Insert(0, liItemName);

                ddlSupplier.Enabled = false;
                ddlSupplier.SelectedItem.Text = (String)Session["ddlValue"];
                tbUOM.Text = "";
                tbPrice.Text = "";
                tbQuantity.Text = "";
            }
            else
            {
                ddlItemName.Enabled = true;
                purchaseOrderBL = new PurchaseOrderBL();
                string category = ddlCategory.SelectedItem.Text;
                List<InventoryStockBO> list = purchaseOrderBL.getItemListByCategory(category);
                List<String> l1 = purchaseOrderBL.getItemNameList(list);
                ddlItemName.DataSource = l1;
                ddlItemName.DataBind();

                ListItem liItemName = new ListItem("Select Item Name", "-1");
                ddlItemName.Items.Insert(0, liItemName);

                ddlSupplier.SelectedIndex = 0;
                ddlSupplier.Enabled = false;
                tbUOM.Text = "";
                tbPrice.Text = "";
                tbQuantity.Text = "";
            }
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(Session["count"]);
            if (ddlItemName.SelectedIndex == 0 && count == 0)
            {
                ddlSupplier.Enabled = false;
                ddlSupplier.SelectedIndex = 0;

                tbUOM.Text = "";
                tbPrice.Text = "";
                tbQuantity.Text = "";

            }
            else if (ddlItemName.SelectedIndex != 0 && count > 0)
            {
                purchaseOrderBL = new PurchaseOrderBL();
                string itemName = ddlItemName.SelectedItem.Text;
                InventoryStockBO ins = new InventoryStockBO();
                ins = purchaseOrderBL.getUOMByItem(itemName);
                tbUOM.Text = ins.ItemUOM.ToString();

                ddlSupplier.Enabled = false;
                string supplier = (String)Session["ddlValue"];
                ddlSupplier.SelectedItem.Text = supplier;
                PurchaseOrderDetailsJoinBO pod = new PurchaseOrderDetailsJoinBO();
                pod = purchaseOrderBL.getPriceBySupplier(itemName, supplier);
                tbPrice.Text = pod.Price.ToString();

                tbQuantity.Text = "";
            }
            else
            {
                purchaseOrderBL = new PurchaseOrderBL();
                string itemName = ddlItemName.SelectedItem.Text;
                InventoryStockBO ins = new InventoryStockBO();
                ins = purchaseOrderBL.getUOMByItem(itemName);
                tbUOM.Text = ins.ItemUOM.ToString();

                ddlSupplier.Enabled = true;
                List<String> supplierList = new List<string>();
                InventoryStockBO bo = new InventoryStockBO();
                bo = purchaseOrderBL.getSupplierByItem(itemName);
                supplierList.Add(bo.Supplier1);
                supplierList.Add(bo.Supplier2);
                supplierList.Add(bo.Supplier3);

                ddlSupplier.DataSource = supplierList;
                ddlSupplier.DataBind();

                ListItem liSupplier = new ListItem("Select Supplier", "-1");
                ddlSupplier.Items.Insert(0, liSupplier);
                tbPrice.Text = "";
                tbQuantity.Text = "";
            }
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(Session["count"]);
            purchaseOrderBL = new PurchaseOrderBL();
            PurchaseOrderDetailsJoinBO ins = new PurchaseOrderDetailsJoinBO();
            string itemName = ddlItemName.SelectedItem.Text;
            string supplier = ddlSupplier.SelectedItem.Text;
            ins = purchaseOrderBL.getPriceBySupplier(itemName, supplier);
            tbPrice.Text = ins.Price.ToString();
            Session["ddlValue"] = ddlSupplier.SelectedItem.Text;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["count"] = 1;
            AddNewRecordRowToGrid();
            string supplierName = (String)Session["ddlValue"];
            purchaseOrderBL = new PurchaseOrderBL();
            List<InventoryStockBO> list = purchaseOrderBL.getCategoryBySupplier(supplierName);
            List<String> l1 = purchaseOrderBL.getCategoryList(list);
            HashSet<String> hs = new HashSet<string>(l1);

            ddlCategory.DataSource = hs;
            ddlCategory.DataBind();
            ListItem liCategory = new ListItem("Select Category", "-1");
            ddlCategory.Items.Insert(0, liCategory);
            ddlCategory.SelectedIndex = 0;
            ddlItemName.Enabled = false;
            ddlItemName.SelectedIndex = 0;

            ddlSupplier.Enabled = false;
            ddlSupplier.SelectedItem.Text = (String)Session["ddlValue"];

            tbUOM.Text = "";
            tbPrice.Text = "";
            tbQuantity.Text = "";
        }

        List<PurchaseOrderDetailsJoinBO> gvDataList = new List<PurchaseOrderDetailsJoinBO>();

        private void AddDefaultFirstRecord()
        {
            gvCreatePurchaseOrderList.Visible = false;
            DataTable dt = new DataTable();
            DataRow dr;
            dt.TableName = "createPO";
            dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemUOM", typeof(string)));
            dt.Columns.Add(new DataColumn("Supplier", typeof(string)));
            dt.Columns.Add(new DataColumn("Price", typeof(double)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(int)));
            dt.Columns.Add(new DataColumn("TotalPrice", typeof(double)));
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            Session["createPO"] = dt;
            gvCreatePurchaseOrderList.DataSource = dt;
            gvCreatePurchaseOrderList.DataBind();
        }

        private void AddNewRecordRowToGrid()//add data to grid view
        {
            if (Session["createPO"] != null && ddlCategory.SelectedIndex != 0 && ddlItemName.SelectedIndex != 0 && ddlSupplier.SelectedIndex != 0)
            {
                gvCreatePurchaseOrderList.Visible = true;
                DataTable dtCurrentTable = (DataTable)Session["createPO"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count == 0)
                {
                    string supplier = ddlSupplier.SelectedItem.Text;
                    string itemNumber = ddlItemName.SelectedItem.Text;
                    int quantity = Convert.ToInt32(tbQuantity.Text);
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["ItemName"] = itemNumber;
                    drCurrentRow["ItemUOM"] = tbUOM.Text;
                    drCurrentRow["Supplier"] = supplier;
                    drCurrentRow["Price"] = Convert.ToDouble(tbPrice.Text);
                    drCurrentRow["Quantity"] = quantity;
                    double total = Convert.ToDouble(tbPrice.Text) * Convert.ToInt32(tbQuantity.Text);
                    drCurrentRow["TotalPrice"] = total;
                    dtCurrentTable.Rows.Add(drCurrentRow);
                }
                else if (dtCurrentTable.Rows.Count == 1)
                {
                    if ((ddlItemName.SelectedItem.Text).Equals(dtCurrentTable.Rows[0][0]))//checks if the selected value already exists to grid view, if so add quantity to the existing one for count==1
                    {
                        dtCurrentTable.Rows[0]["Quantity"] = Convert.ToInt32(dtCurrentTable.Rows[0]["Quantity"]) + Convert.ToInt32(tbQuantity.Text);
                        double totalPrice = Convert.ToDouble(tbPrice.Text) * Convert.ToDouble(dtCurrentTable.Rows[0]["Quantity"]);
                        dtCurrentTable.Rows[0]["TotalPrice"] = totalPrice;
                    }
                    else
                    {
                        string supplier = ddlSupplier.SelectedItem.Text;
                        string itemNumber = ddlItemName.SelectedItem.Text;
                        int quantity = Convert.ToInt32(tbQuantity.Text);
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["ItemName"] = itemNumber;
                        drCurrentRow["ItemUOM"] = tbUOM.Text;
                        drCurrentRow["Supplier"] = supplier;
                        drCurrentRow["Price"] = Convert.ToDouble(tbPrice.Text);
                        drCurrentRow["Quantity"] = quantity;
                        double total = Convert.ToDouble(tbPrice.Text) * Convert.ToInt32(tbQuantity.Text);
                        drCurrentRow["TotalPrice"] = total;
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }
                }
                else if (dtCurrentTable.Rows.Count > 1)
                {
                    int i = 0;
                    for (i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        if ((ddlItemName.SelectedItem.Text).Equals(dtCurrentTable.Rows[i][0]))//checks if the selected value already exists to grid view, if so add quantity to the existing on for count >1
                        {
                            dtCurrentTable.Rows[i]["Quantity"] = Convert.ToInt32(dtCurrentTable.Rows[i]["Quantity"]) + Convert.ToInt32(tbQuantity.Text);
                            double totalPrice = Convert.ToDouble(tbPrice.Text) * Convert.ToDouble(dtCurrentTable.Rows[i]["Quantity"]);
                            dtCurrentTable.Rows[i]["TotalPrice"] = totalPrice;
                            break;
                        }
                    }
                    if (i == dtCurrentTable.Rows.Count && (string)dtCurrentTable.Rows[i - 1][1] != tbQuantity.Text)
                    {
                        string supplier = ddlSupplier.SelectedItem.Text;
                        string itemNumber = ddlItemName.SelectedItem.Text;
                        int quantity = Convert.ToInt32(tbQuantity.Text);
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["ItemName"] = itemNumber;
                        drCurrentRow["ItemUOM"] = tbUOM.Text;
                        drCurrentRow["Supplier"] = supplier;
                        drCurrentRow["Price"] = Convert.ToDouble(tbPrice.Text);
                        drCurrentRow["Quantity"] = quantity;
                        double total = Convert.ToDouble(tbPrice.Text) * Convert.ToInt32(tbQuantity.Text);
                        drCurrentRow["TotalPrice"] = total;
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }
                }

                if (dtCurrentTable.Rows[0][0].ToString() == "")
                {
                    dtCurrentTable.Rows[0].Delete();
                    dtCurrentTable.AcceptChanges();
                }
                Session["gvCount"] = dtCurrentTable.Rows.Count;
                Session["createPO"] = dtCurrentTable;
                gvCreatePurchaseOrderList.DataSource = dtCurrentTable;
                gvCreatePurchaseOrderList.DataBind();
                footableSettings();

            }
            else if (Session["createPO"] != null || ddlCategory.SelectedIndex != 0 || ddlItemName.SelectedIndex != 0 || ddlSupplier.SelectedIndex != 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Please select item's criteria first');window.location ='CreatePO.aspx';",
                                true);
                Session["count"] = 0;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataTable datatbl = new DataTable();
            if (Session["createPO"] != null && Convert.ToInt32(Session["gvCount"]) == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Please select item's criteria first');window.location ='CreatePO.aspx';",
                                true);
                Session["count"] = 0;
            }
            else
            {
                EmployeeBO ebo = (EmployeeBO)Session["employee"];
                datatbl = (DataTable)Session["createPO"];
                purchaseOrderBL = new PurchaseOrderBL();
                PurchaseOrderBO poLastValue = new PurchaseOrderBO();
                PurchaseOrderBO pobo = new PurchaseOrderBO();
                poLastValue = purchaseOrderBL.countTotalPO();
                string idSeperator = poLastValue.PoId.ToString().Substring(2);//gets the last PurchaseOrder ID from database
                int valuePod = Convert.ToInt32(idSeperator);
                if (valuePod < 100)
                {
                    valuePod = valuePod + 1;
                    poCountStr = "PO0" + valuePod;
                }
                else
                {
                    valuePod = valuePod + 1;
                    poCountStr = "PO" + valuePod;
                }
                pobo.PoId = poCountStr;
                string date = tbDeliveryDate.Text;
                DateTime dt = DateTime.Parse(tbDeliveryDate.Text);
                pobo.DeliveryDate = dt;
                pobo.DateOfOrder = DateTime.Today;
                string eId = ebo.EmployeeId;
                pobo.RequestorId = eId;
                pobo.CommentByClerk = Request.Form["tbClerkComments"];
                EmployeeBO empBo = purchaseOrderBL.getSupervisorId();//get supervisor ID of the logged in employee
                pobo.ApproverId = empBo.EmployeeId;
                pobo.Status = "Pending";
                Boolean flagPO = purchaseOrderBL.createPurchaseOrder(pobo);
                Boolean flagPOD = false;
                foreach (DataRow dr in datatbl.Rows)
                {
                    PurchaseOrderDetailsBO pod = new PurchaseOrderDetailsBO();
                    pod.PoId = poCountStr;
                    PurchaseOrderDetailsBO podLastValue = new PurchaseOrderDetailsBO();
                    podLastValue = purchaseOrderBL.countTotalPODId();
                    string idSeperate = podLastValue.PoDetailsId.ToString().Substring(2);//gets the last PurchaseOrderDetails ID from database
                    int valuePoId = Convert.ToInt32(idSeperate);
                    if (valuePoId < 100)
                    {
                        valuePoId = valuePoId + 1;
                        podIdCountStr = "PD0" + valuePoId;
                    }
                    else
                    {
                        valuePoId = valuePoId + 1;
                        podIdCountStr = "PD" + valuePoId;
                    }
                    pod.PoDetailsId = podIdCountStr;
                    pod.SupplierId = ddlSupplier.SelectedItem.Text;
                    string itemName = purchaseOrderBL.getItemNumberByName(dr["ItemName"].ToString());
                    pod.ItemNumber = itemName;

                    pod.Quantity = Convert.ToInt32(dr["Quantity"]);
                    flagPOD = purchaseOrderBL.createPurchaseOrderDetails(pod);
                }
                if (flagPO && flagPOD)//checks if both PurchaseOrder and PurchaseOrderDetails are updated to database. if so, send an email to the supervisor
                {
                    SendEmailSubmit(poCountStr);
                }
                else
                {
                    string script = "alert('There is some issue in sending email. We apologize for this!');";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
                }

                Response.Redirect("~/Store/POList.aspx");
            }
        }
        protected void footableSettings()
        {
            gvCreatePurchaseOrderList.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvCreatePurchaseOrderList.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvCreatePurchaseOrderList.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            gvCreatePurchaseOrderList.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
            gvCreatePurchaseOrderList.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
            //Adds THEAD and TBODY to GridView.
            gvCreatePurchaseOrderList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void gvCreatePurchaseOrderList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           // Page.DataBind();
            int index = Convert.ToInt32(e.RowIndex);
            int gvDeleteCount = 0;
            DataTable dt = (DataTable)Session["createPO"];
            dt.Rows[index].Delete();
            Session["createPO"] = dt;
            gvCreatePurchaseOrderList.DataSource = dt;
            gvCreatePurchaseOrderList.DataBind();
            gvDeleteCount = Convert.ToInt32(Session["gvCount"]);
            gvDeleteCount = gvDeleteCount - 1;
            Session["gvCount"] = gvDeleteCount;
            if (gvDeleteCount == 0)
            {
                Session["count"] = 0;
                Response.Redirect("~/Store/CreatePO.aspx", false);
            }
        }

        public void SendEmailSubmit(string poCountStr)
        {
            EmployeeBO ebo = (EmployeeBO)Session["employee"];
            EmployeeBO empBo = purchaseOrderBL.getSupervisorId();
            SendEmail se = new SendEmail();
            string sub = "Purchase Order Created";
            string body = "Dear " + empBo.EmployeeName + ", \n"
                        + "\n" + "A new Purchase Order with ID: " + poCountStr + " has been created by " + ebo.EmployeeName + " and is waiting for approval."
                        + "\n" + "(This is an auto-generated email, please do not reply.)"
                        + "\n\n\n" + "Regards,"
                        + "\n" + "Admin";
            se.sendCPEmail(sub, body, empBo.EmployeeEmail);
        }
    }
}