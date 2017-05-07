using BusinessLogic;
using Model;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogic.StoreBL;
using System.Collections.Generic;

namespace SSIS
{
    public partial class InventoryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InventoryListBL bl = new InventoryListBL();

            List<InventoryStockBO> isBOList= bl.getInventoryList();
             if (isBOList.Count!=0)
               {
                gvInventoryList.DataSource = isBOList;
                gvInventoryList.DataBind();
                //gvInventoryList.Columns[0].Visible = false;

                //Attribute to show the Plus Minus Button.
                gvInventoryList.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.
                gvInventoryList.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                gvInventoryList.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                gvInventoryList.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                gvInventoryList.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
                gvInventoryList.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
                //Adds THEAD and TBODY to GridView.
                gvInventoryList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }else
            {
                lblNoInventoryList.Text = "No Inventory List";
            }
          
        }
        protected void gvInventoryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            InventoryListBL bl = new InventoryListBL();
            gvInventoryList.PageIndex = e.NewPageIndex;

            gvInventoryList.DataSource = bl.getInventoryList();
            gvInventoryList.DataBind();

            //Attribute to show the Plus Minus Button.
            gvInventoryList.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvInventoryList.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            gvInventoryList.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvInventoryList.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            gvInventoryList.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
            gvInventoryList.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
            //Adds THEAD and TBODY to GridView.
            gvInventoryList.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
    }
}