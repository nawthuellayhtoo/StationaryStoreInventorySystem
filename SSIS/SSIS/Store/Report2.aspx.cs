using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;

namespace SSIS.Store
{
    public partial class Report2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string StartDate = Request.QueryString["StartDate"];
                string EndDate = Request.QueryString["EndDate"];
                string DeptId = Request.QueryString["DeptId"];
                string query = "select i.ItemCategory, dp.DepartmentID, rd.ItemQuantity as TotalItemQuantity"
                                + " from Requisition r,"
                                     + "RequisitionDetails rd,"
                                     + "InventoryStock i,"
                                     + "Disbursement dm,"
                                     + "Department dp"
                                + " where i.ItemNumber = rd.ItemNumber"
                                + " and rd.RequisitionID = r.RequisitionID"
                                + " and r.RequisitionID = dm.RequisitionID"
                                + " and dm.DepartmentID = dp.DepartmentID"
                                + " and r.ApprovalDate between '" + StartDate + "' and '" + EndDate + "'"
                                + " and dm.DepartmentID = '" + DeptId + "'";
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=SA43Team2StoreDB;Integrated Security=True;");
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("DeptCategoryQty");

                da.Fill(dt);
                con.Close();
                da.Dispose();
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt));
                ReportViewer1.LocalReport.Refresh();

                lbStartDateValue.Text = StartDate;
                lbEndDateValue.Text = EndDate;
                lbDeptValue.Text = DeptId;
                lbGenerateDateValue.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now);
            }
        }

        protected void btBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/GenerateReport.aspx");
        }
    }
 }
