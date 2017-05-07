using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using DataAccess;
using SSIS.Store;
using Model;
using BusinessLogic.StoreBL;
using System.Collections;
namespace SSIS
{
    public partial class GenerateReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {//populate department dropdownlist
                    GenerateReportBL grbl = new GenerateReportBL();
                    ddlDept.DataTextField = "DeptName";
                    ddlDept.DataValueField = "DeptId";
                    ddlDept.DataSource = grbl.getDepartmentList();
                    ddlDept.DataBind();

                    ddlDept.Visible = false;
                    lbDeptType.Visible = false;
                }
                catch (Exception x)
                {
                    System.Diagnostics.Debug.WriteLine(x);
                }
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {// option to pass the selected information to another page
            if (rbReport1.Checked == true)
            {
                Response.Redirect("~/Store/Report1.aspx?StartDate=" + tbStartDate.Text + "&EndDate=" + tbEndDate.Text);
            }
            else if (rbReport2.Checked == true)
            {
                Response.Redirect("~/Store/Report2.aspx?StartDate=" + tbStartDate.Text + "&EndDate=" + tbEndDate.Text + "&DeptId=" + ddlDept.SelectedValue);
            }
            else if (rbReport3.Checked == true)
            {
                Response.Redirect("~/Store/Report3.aspx?StartDate=" + tbStartDate.Text + "&EndDate=" + tbEndDate.Text);
            }
        }

        protected void Group1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbReport1.Checked == true)
            {
                ddlDept.Visible = false;
                lbDeptType.Visible = false;

            }
            else if (rbReport2.Checked == true)
            {
                GenerateReportBL grbl = new GenerateReportBL();
                ddlDept.DataTextField = "DeptName";
                ddlDept.DataValueField = "DeptId";
                ddlDept.DataSource = grbl.getDepartmentList();
                ddlDept.DataBind();
                ddlDept.Visible = true;
                lbDeptType.Visible = true;
            }
            else if (rbReport3.Checked == true)
            {
                ddlDept.Visible = false;
                lbDeptType.Visible = false;
            }
        }
    }
}
