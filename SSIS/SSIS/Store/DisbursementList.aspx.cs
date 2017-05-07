using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.StoreBL;
using Model;

namespace SSIS
{
    public partial class DisbursementList : System.Web.UI.Page
    {
        StoreDisbursementBL sdbl = new StoreDisbursementBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlDepartment.DataSource = sdbl.getDistictDepList();
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "DeptId";
                ddlDepartment.DataBind();
                ddlDepartment.Items.FindByValue("COMM").Selected = true;
                string depId = sdbl.getDepIdByDepName("Commerce Dept");
                tbDeptRep.Text = sdbl.getDepRepName(depId).ToString();
                tbCollectionPoint.Text = sdbl.getCollectionPoint(depId).ToString();
                tbCollectionTime.Text = sdbl.getCollectionTime(depId).ToString();

                List<DisbursementListBO> dlboList= sdbl.detDisbursementList(depId);
                gvDisbursement.DataSource = dlboList;
                gvDisbursement.DataBind();
                if (dlboList.Count()==0)
                {
                    lblDisbursementInfo.Text = "No Disbursement List";
                }
                if (gvDisbursement.Rows.Count != 0)
                {                
                    fooTableSetting();
                }           
            }
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string depId = ddlDepartment.SelectedValue;
            tbDeptRep.Text = sdbl.getDepRepName(depId).ToString();
            tbCollectionPoint.Text = sdbl.getCollectionPoint(depId).ToString();
            tbCollectionTime.Text = sdbl.getCollectionTime(depId).ToString();
            List<DisbursementListBO> dlboList = sdbl.detDisbursementList(depId);
            gvDisbursement.DataSource = dlboList;
            gvDisbursement.DataBind();
            if (dlboList.Count() == 0)
            {
                lblDisbursementInfo.Text = "No Disbursement List";
            }
            if (gvDisbursement.Rows.Count != 0)
            {
                fooTableSetting();
            }
        }

        protected void gvDisbursement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string depId = ddlDepartment.SelectedValue;
            gvDisbursement.PageIndex = e.NewPageIndex;
            List<DisbursementListBO> dlboList = sdbl.detDisbursementList(depId);
            gvDisbursement.DataSource = dlboList;
            gvDisbursement.DataBind();
            if (dlboList.Count() == 0)
            {
                lblDisbursementInfo.Text = "No Disbursement List";
            }
            if (gvDisbursement.Rows.Count != 0)
            {
                fooTableSetting();
            }
         }

        public void fooTableSetting()
        {
            //Attribute to show the Plus Minus Button.
            gvDisbursement.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvDisbursement.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            gvDisbursement.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            gvDisbursement.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}