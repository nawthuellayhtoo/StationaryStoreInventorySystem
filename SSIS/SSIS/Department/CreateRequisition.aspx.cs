using BusinessLogic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using BusinessLogic.DepartmentBL;

namespace SSIS
{
    public partial class CreateRequisition : System.Web.UI.Page
    {
        DataTable dt;
        CreateRequisitionBL crbl = new CreateRequisitionBL();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    Session["AddRequisitionTable"] = null;
                    dt = new DataTable("AddRequisitionTable");
                    dt.Columns.Add("updateItemName", typeof(string));
                    dt.Columns.Add("updateQuantity", typeof(string));
             }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            LabelSubmit.Text = "";

            if (Session["AddRequisitionTable"] == null)
            {
                dt = new DataTable("AddRequisitionTable");
                dt.Columns.Add("updateItemName", typeof(string));
                dt.Columns.Add("updateQuantity", typeof(string));
            }
            else
            {
                dt = (DataTable)Session["AddRequisitionTable"];
            }

            if (Convert.ToInt32(tbQuantity.Text) == 0)
            {
                RequiredFieldValidatorForQuantity.Text = "Cannot be 0!";
            }
            else
            {
                if (dt.Rows.Count == 0)
                    addNewRow();
                else if (dt.Rows.Count == 1)
                {
                    if ((string)dt.Rows[0][0] == ddlItemName.SelectedValue.ToString())
                        addQuantity();
                    else
                        addNewRow();
                }
                else if (dt.Rows.Count > 1)
                {
                    int i;
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        if ((string)dt.Rows[i][0] == ddlItemName.SelectedValue.ToString())
                        {
                            addQuantity();
                            break;
                        }
                    }
                    if (i == dt.Rows.Count && (string)dt.Rows[i - 1][1] != tbQuantity.Text)
                        addNewRow();
                }
                GridViewCreateRequisition.DataSource = dt;
                GridViewCreateRequisition.DataBind();

                if (dt.Rows.Count != 0)
                {
                    fooTableSetting();
                }

                Session["AddRequisitionTable"] = dt;
            }
        }

        protected void GridViewCreateRequisition_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            dt = (DataTable)Session["AddRequisitionTable"];
            dt.Rows[index].Delete();
            Session["AddRequisitionTable"] = dt;
            GridViewCreateRequisition.DataSource = dt;
            GridViewCreateRequisition.DataBind();
            if (dt.Rows.Count != 0)
            {
                fooTableSetting();
            }
        }

        protected void BtbSubmit_Click(object sender, EventArgs e)
        {
            dt = (DataTable)Session["AddRequisitionTable"];
            if (dt.Rows.Count == 0)
            {
                LabelSubmit.Text = "Please add item first.";
            }
            else
            {
                LabelSubmit.Text = "";
                RequisitionBO rbo = new RequisitionBO();
                EmployeeBO ebo = (EmployeeBO)Session["employee"];
                dt = (DataTable)Session["AddRequisitionTable"];

                string requisitionId = crbl.createRequisitionId();
                rbo.RequisitionId = requisitionId;
                rbo.EmpId = ebo.EmployeeId;
                rbo.CommentsByEmp = Request.Form["tbEmployeeComments"];
                rbo.Status = "Pending";
                rbo.EmpRequisitionDate = DateTime.Today;
                crbl.submitRequisition(rbo);

                string sub = "New Requisition";
                string name = crbl.getHeadNameByDepId(ebo.Department.DeptId);
                string body = "Dear " + name + ", \n"
                           + "\n" + "I have made a new requisition for those items below: " + "\n";

                string bodyForOutOfStock = "Dear clerk, \n\n";

                foreach (DataRow dr in dt.Rows)
                {
                    RequisitionDetailsBO rdbo = new RequisitionDetailsBO();
                    rdbo.RequisitionId = requisitionId;
                    rdbo.RequisitionDetailsId = crbl.createRequisitionDetailsId();

                    string itemNumber = crbl.getItemNumberByItemName(dr["updateItemName"].ToString());
                    rdbo.ItemNumber = itemNumber;

                    int itemQuantity = Convert.ToInt32(dr["updateQuantity"].ToString());
                    rdbo.ItemQuantity = itemQuantity;

                    if (checkReorderLevel(itemNumber, itemQuantity) == false)
                    {
                        bodyForOutOfStock += "The item: " + dr["updateItemName"].ToString() + " is reaching the reorder level, Please place orders.\n";

                        Label1.Text += bodyForOutOfStock + "  " + itemNumber + " " + itemQuantity + "  " + checkReorderLevel(itemNumber, itemQuantity).ToString();
                    }

                    crbl.submitRequisitionDetails(rdbo);
                    body += "Item Name: " + dr["updateItemName"].ToString() + ":\t Quantity: " + dr["updateQuantity"].ToString() + "\n";
                }

                body += "\n\n" + "Regards," + "\n" + ebo.EmployeeName;

                SendEmail sendEmail = new SendEmail();
                sendEmail.sendCPEmail(sub, body, crbl.getHeadEmailByDepId(ebo.Department.DeptId));

                if (bodyForOutOfStock != "Dear clerk, \n\n")
                {
                    string subject = "items out of stock";
                    foreach (string email in crbl.getAllClerksEmail())
                    {
                        sendEmail.sendCPEmail(subject, bodyForOutOfStock, email);
                    }
                }
                Response.Redirect("~/Department/RequisitionList.aspx");
            }
        }

        public void addNewRow()
        {
            if (checkInventory(Convert.ToInt32(tbQuantity.Text)) == true)
            {
                DataRow dt_row = dt.NewRow();
                dt_row["updateItemName"] = ddlItemName.SelectedValue.ToString();
                dt_row["updateQuantity"] = tbQuantity.Text;
                dt.Rows.Add(dt_row);
            }

        }

        public void addQuantity()
        {
            if (checkInventory(Convert.ToInt32(tbQuantity.Text) + Convert.ToInt32(dt.Rows[0][1])) == true)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((string)dt.Rows[i][0] == ddlItemName.SelectedValue.ToString())
                    {
                        int newQuantity = Convert.ToInt32(dt.Rows[i][1]) + Convert.ToInt32(tbQuantity.Text);
                        dt.Rows[i][1] = newQuantity.ToString();
                    }
                }
            }
        }

        protected void tbQuantity_TextChanged(object sender, EventArgs e)
        {
            LbStatus.Text = "";
        }

        public bool checkInventory(int quantity)
        {
            bool enoughInventory = true;
            string itemNumber = crbl.getItemNumberByItemName(ddlItemName.SelectedValue.ToString());
            int inventoryQty = crbl.getInventoryQtyByItemNumber(itemNumber);
            int reorderLevel = crbl.getReorderLevelByItemNumber(itemNumber);
            if (inventoryQty - quantity < 0)
            {
                LbStatus.Text = "The item: " + ddlItemName.SelectedValue.ToString() +
                    " does not have enough quantity in the inventory \n If you still place the order, you may wait for 4-5 days.\n";
                enoughInventory = false;
            }
            return enoughInventory;
        }


        public bool checkReorderLevel(string itemNumber, int itemQuantity)
        {
            bool reorder = true;
            int inventoryQty = crbl.getInventoryQtyByItemNumber(itemNumber);
            int reorderLevel = crbl.getReorderLevelByItemNumber(itemNumber);
            if (inventoryQty - reorderLevel < itemQuantity)
                reorder = false;
            return reorder;
        }

        public void fooTableSetting()
        {
            //Attribute to show the Plus Minus Button.
            GridViewCreateRequisition.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            GridViewCreateRequisition.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            GridViewCreateRequisition.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}