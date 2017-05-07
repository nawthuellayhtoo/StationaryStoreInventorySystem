using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using Model;
using BusinessLogic.DepartmentBL;
using System.Data;

namespace SSIS
{
    public partial class CatalogueList : System.Web.UI.Page
    {
        CatalogueListBL cbl = new CatalogueListBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<InventoryStockBO> iboList = new List<InventoryStockBO>();
                iboList = cbl.getCatalogList();

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3]
                {
                new DataColumn("itemName"),
                new DataColumn("itemCategory"),
                new DataColumn("itemUOM")
                });

                foreach (InventoryStockBO i in iboList)
                {
                    dt.Rows.Add(i.ItemName, i.ItemCategory, i.ItemUOM);
                }
                if (dt.Rows.Count != 0)
                {
                    GridViewcatalogueList.DataSource = dt;
                    GridViewcatalogueList.DataBind();
                    fooTableSetting();                 
                }
                else
                {
                    lblNoCatalogueList.Text = "No Catalogue List";
                }
            }
           
        }
        public void fooTableSetting()
        {
            //Attribute to show the Plus Minus Button.
            GridViewcatalogueList.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            GridViewcatalogueList.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            GridViewcatalogueList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void GridViewcatalogueList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewcatalogueList.PageIndex = e.NewPageIndex;
            List<InventoryStockBO> iboList = new List<InventoryStockBO>();
            iboList = cbl.getCatalogList();

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3]
            {
                new DataColumn("itemName"),
                new DataColumn("itemCategory"),
                new DataColumn("itemUOM")
            });

            foreach (InventoryStockBO i in iboList)
            {
                dt.Rows.Add(i.ItemName, i.ItemCategory, i.ItemUOM);
            }
            if (dt.Rows.Count != 0)
            {
                GridViewcatalogueList.DataSource = dt;
                GridViewcatalogueList.DataBind();
                fooTableSetting();
            }
        }
    }
}
