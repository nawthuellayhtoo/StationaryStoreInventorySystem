using BusinessLogic;
using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogic.StoreBL;

namespace SSIS
{
    public partial class POList : System.Web.UI.Page
    {
        PurchaseOrderBL purchaseOrderBL;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                doProcess();
            }
        }

        protected void gvPurchaseOrderList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void gvPurchaseOrderList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPurchaseOrderList.PageIndex = e.NewPageIndex;
            doProcess();
        }
        public void doProcess()
        {
            if (Session["employee"] != null)
            {
                EmployeeBO ebo = (EmployeeBO)Session["employee"];
                string empId = ebo.EmployeeId;
                string title = ebo.EmployeeTitle;
                purchaseOrderBL = new PurchaseOrderBL();
                if (title.Equals("Manager") || title.Equals("Supervisor"))
                {
                   
                   List< PurchaseOrderBO > polst=purchaseOrderBL.getPurchaseOrderListAll();
                    if (polst.Count!=0)
                    {
                        gvPurchaseOrderList.DataSource = polst;
                        gvPurchaseOrderList.DataBind();
                        footableSettings();
                    }
                    else
                    {
                        lblNoPOList.Text = "No Purchase Order List";
                    }              
                }
                else
                {
                    List < PurchaseOrderBO > polst = purchaseOrderBL.getPurchaseOrderList(empId);
                    gvPurchaseOrderList.DataSource = polst;
                    gvPurchaseOrderList.DataBind();
                    if (polst.Count != 0)
                    {
                        gvPurchaseOrderList.DataSource = polst;
                        gvPurchaseOrderList.DataBind();
                        footableSettings();
                    }
                    else
                    {
                        lblNoPOList.Text = "No Purchase Order List";
                    }
                }
            }
            else
            {
                Console.WriteLine("Something is wrong!!!!!!");
            }
        }
        protected void footableSettings()
        {
            gvPurchaseOrderList.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvPurchaseOrderList.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            gvPurchaseOrderList.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvPurchaseOrderList.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            //Adds THEAD and TBODY to GridView.
            gvPurchaseOrderList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

    }
}