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
    public partial class Report3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string StartDate = Request.QueryString["StartDate"];
                string EndDate = Request.QueryString["EndDate"];
                string DeptId = Request.QueryString["DeptId"];
                string query = "SELECT ItemCategory,CASE SupplierID WHEN [Supplier1]     THEN [Price1]*[Quantity]"
                            + " WHEN [Supplier2] THEN [Price2]*[Quantity]"
                            + " ELSE [Price3]*[Quantity]"
                            + " END AS Price"
                            + " FROM View_2"
                            + " WHERE View_2.Status ='Approved' AND View_2.ApprovalDate BETWEEN '" + StartDate + "' AND '" + EndDate + "'";

                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=SA43Team2StoreDB;Integrated Security=True;");
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("DeptCategoryPrice");

                da.Fill(dt);
                con.Close();
                da.Dispose();
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dt));
                ReportViewer1.LocalReport.Refresh();

                lbStartDateValue.Text = StartDate;
                lbEndDateValue.Text = EndDate;
                lbDept.Visible = false;
                lbDeptValue.Visible = false;
                lbGenerateDateValue.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now);
            }
        }

        protected void btBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/GenerateReport.aspx");
        }
    }
}