using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.StoreBL;
using System.Data;
using Model;

namespace SSIS
{
    public partial class RetrievalList : System.Web.UI.Page
    {
        RetrivalListBL rbl = new RetrivalListBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<RetrivalBO> rboList= rbl.getDataSource();
                if (rboList.Count()!=0)
                {
                    gvRetrivalList.DataSource = rboList;
                    gvRetrivalList.DataBind();
                    ViewState["error"] = "No";
                    footableSettings();
                }
                else
                {
                    lblListInfo.Text = "No Item to retrieve";
                    btnUpdate.Visible = false;
                }
            }
           // footableSettings();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ItemName = gvRetrivalList.DataKeys[e.Row.RowIndex].Value.ToString();

                GridView gvBreakeDownByDept = e.Row.FindControl("gvBreakeDownByDept") as GridView;
                gvBreakeDownByDept.DataSource = rbl.getBreakDownLstByName(ItemName);
                gvBreakeDownByDept.DataBind();
                gvBreakeDownByDept.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void tbTotalActual_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox tb = (TextBox)sender;

                if (tb.Text != "")
                {
                    Page.Validate();
                    if (IsValid)
                    {
                        int retrieved = Convert.ToInt32(tb.Text);
                        GridViewRow row = (GridViewRow)tb.Parent.Parent;

                        string itemName = gvRetrivalList.DataKeys[row.RowIndex].Value.ToString();
                        int stockQty = rbl.getStockQty(itemName);
                        int reorderLevel = rbl.getReorderLevel(itemName);
                        if (retrieved <= stockQty)
                        {
                            {
                                GridView gvBreakeDownByDept = row.FindControl("gvBreakeDownByDept") as GridView;
                                gvBreakeDownByDept.DataSource = rbl.updateBreakDownLst(itemName, retrieved);
                                gvBreakeDownByDept.DataBind();
                                gvBreakeDownByDept.HeaderRow.TableSection = TableRowSection.TableHeader;
                            }
                        }

                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Stock quantity is less that retrived quantity')", true);   //show error message
                            tb.Text = "";
                        }
                    }


                }
            }
            footableSettings();
            
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                List<RetrievalDetailBO> rLst = getGridViewData();
                if ((string)ViewState["error"] == "No")
                {
                    rbl.update(rLst);
                    Response.Redirect("~/STORE/RetrievalList.aspx");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('disburse quantity is not matching with retrieved quantity')", true);
               
                }
                footableSettings();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Some error')", true);
                Response.Redirect("~/STORE/RetrievalList.aspx");
            }
        }
        protected void tbActual_TextChanged(object sender, EventArgs e)
        {
            Page.Validate();
            footableSettings();
        }
        public List<RetrievalDetailBO> getGridViewData()
        {
            List<RetrievalDetailBO> rboLst = new List<RetrievalDetailBO>();
            foreach (GridViewRow gvRow in gvRetrivalList.Rows)
            {
                RetrievalDetailBO r = new RetrievalDetailBO();
                r.Rbo = new RetrivalBO();
                r.BdboLst = new List<BreakDownByDepBO>();
                r.Rbo.ItemName = gvRow.Cells[1].Text.ToString();
                r.Rbo.Needed = Convert.ToInt32(gvRow.Cells[2].Text);
                TextBox t = (TextBox)gvRow.Cells[3].FindControl("tbTotalActual");
                if (t.Text != "")
                {
                    r.Rbo.Retrived = Convert.ToInt32(t.Text);
                }
                else
                {
                    r.Rbo.Retrived = 0;
                }
                GridView gvBreakeDownByDept = gvRow.FindControl("gvBreakeDownByDept") as GridView;
                int totalDisbursed = 0;

                foreach (GridViewRow gv1Row in gvBreakeDownByDept.Rows)
                {
                    BreakDownByDepBO bd = new BreakDownByDepBO();
                    bd.Dep = gv1Row.Cells[0].Text;
                    bd.Outstanding = Convert.ToInt32(gv1Row.Cells[1].Text);
                    bd.Needed = Convert.ToInt32(gv1Row.Cells[2].Text);
                    TextBox tb = (TextBox)gv1Row.Cells[2].FindControl("tbActual");
                    if (tb.Text != "")
                    {
                        bd.Actual = Convert.ToInt32(tb.Text);
                        totalDisbursed = totalDisbursed + bd.Actual;
                    }
                    else
                    {
                        bd.Actual = 0;
                    }

                    r.BdboLst.Add(bd);
                }
                if (totalDisbursed == r.Rbo.Retrived)
                {
                    rboLst.Add(r);
                }
                else
                {

                    ViewState["error"] = "Yes";
                    break;
                }
            }

            return rboLst;

        }
        protected void footableSettings()
        {
            gvRetrivalList.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            //gvRetrivalList.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            //gvRetrivalList.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            //gvRetrivalList.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            //Adds THEAD and TBODY to GridView.
            gvRetrivalList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }


    }
}
