using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessLogic.StoreBL;
using Model;

namespace SSIS
{
    public partial class UpdateSupplier : System.Web.UI.Page
    {
        MaintainSupplierBL bl = new MaintainSupplierBL();
        InventoryStockBO selectedItem;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initializedData();
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            String astr = ddlCategory.SelectedItem.Text;

            List<InventoryStockBO> itemList = bl.getItemListByCategory(astr);

            List<String> itemNameList = bl.getItemNameList(itemList);
            ddlItemName.DataSource = itemNameList;
            ddlItemName.DataBind();

            showSupplierInfo(ddlItemName.SelectedItem.Text);
        }

        public void initializedData()
        {
            List<String> categoryLst = bl.getDistinctCategory();
            ddlCategory.DataSource = categoryLst;
            ddlCategory.DataBind();

            string str = categoryLst.First();
            ddlCategory.SelectedItem.Text = categoryLst.First();

            List<InventoryStockBO> itemList1 = bl.getItemListByCategory(ddlCategory.SelectedItem.Text);
            List<String> itemNameList1 = bl.getItemNameList(itemList1);
            ddlItemName.DataSource = itemNameList1;
            ddlItemName.DataBind();

            string itemName = itemNameList1.First();

            List<SupplierBO> supplierBOList = bl.getAllSupplier();
            ddlSupplier1.DataSource = supplierBOList;
            ddlSupplier1.DataTextField = "SupplierId";
            ddlSupplier1.DataValueField = "SupplierName";
            ddlSupplier1.DataBind();

            ddlSupplier2.DataSource = supplierBOList;
            ddlSupplier2.DataTextField = "SupplierId";
            ddlSupplier2.DataValueField = "SupplierName";
            ddlSupplier2.DataBind();

            ddlSupplier3.DataSource = supplierBOList;
            ddlSupplier3.DataTextField = "SupplierId";
            ddlSupplier3.DataValueField = "SupplierName";
            ddlSupplier3.DataBind();

            showSupplierInfo(itemName);
        }
        public void showSupplierInfo(string itemName)
        {
            selectedItem = bl.getItemByName(itemName);

            selectedItem.SupplierOne = bl.getSupplierBySupplierId(selectedItem.Supplier1);
            selectedItem.SupplierTwo = bl.getSupplierBySupplierId(selectedItem.Supplier2);
            selectedItem.SupplierThree = bl.getSupplierBySupplierId(selectedItem.Supplier3);

            ddlSupplier1.SelectedItem.Text = selectedItem.SupplierOne.SupplierId;
            tbSupplier1Name.Text = selectedItem.SupplierOne.SupplierName;
            tbSupplier1Price.Text = Convert.ToString(selectedItem.Price1);

            ddlSupplier2.SelectedItem.Text = selectedItem.SupplierTwo.SupplierId;
            tbSupplier2Name.Text = selectedItem.SupplierTwo.SupplierName;
            tbSupplier2Price.Text = Convert.ToString(selectedItem.Price2);

            ddlSupplier3.SelectedItem.Text = selectedItem.SupplierThree.SupplierId;
            tbSupplier3Name.Text = selectedItem.SupplierThree.SupplierName;
            tbSupplier3Price.Text = Convert.ToString(selectedItem.Price3);
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            showSupplierInfo(ddlItemName.SelectedItem.Text);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            selectedItem = bl.getItemByName(ddlItemName.SelectedItem.Text);

            selectedItem.Supplier1 = ddlSupplier1.SelectedItem.Text;
            selectedItem.Supplier2 = ddlSupplier2.SelectedItem.Text;
            selectedItem.Supplier3 = ddlSupplier3.SelectedItem.Text;

            selectedItem.SupplierOne = bl.getSupplierBySupplierId(selectedItem.Supplier1);
            selectedItem.SupplierTwo = bl.getSupplierBySupplierId(selectedItem.Supplier2);
            selectedItem.SupplierThree = bl.getSupplierBySupplierId(selectedItem.Supplier3);

            selectedItem.Price1 = Convert.ToDouble(tbSupplier1Price.Text);
            selectedItem.Price2 = Convert.ToDouble(tbSupplier2Price.Text);
            selectedItem.Price3 = Convert.ToDouble(tbSupplier3Price.Text);

            int status = bl.updateSupplier(selectedItem);

            if (status > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data saved successfully')", true);
            }
        }
        protected void ddlSupplier1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSupplier1Name.Text = ddlSupplier1.SelectedValue;
        }

        protected void ddlSupplier2_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSupplier2Name.Text = ddlSupplier2.SelectedValue;
        }

        protected void ddlSupplier3_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSupplier3Name.Text = ddlSupplier3.SelectedValue;
        }
    }
}