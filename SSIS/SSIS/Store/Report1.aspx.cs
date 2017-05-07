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
    public partial class Report1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {// dataset method to get dataset data and pass into the microsoft report
                string StartDate = Request.QueryString["StartDate"];
                string EndDate = Request.QueryString["EndDate"];

                string query = "SELECT i.ItemCategory, pod.Quantity as TotalQuantity FROM PurchaseOrderDetails pod, PurchaseOrder po, InventoryStock i "
                + "WHERE pod.POID=po.POID AND i.ItemNumber=pod.ItemNumber AND po.ApprovalDate between '" + StartDate + "' AND '" + EndDate + "' AND po.Status='Approved'";
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=SA43Team2StoreDB;Integrated Security=True;");
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("CategoryQuantityDT");

                //clear connection and da after da.fill
                da.Fill(dt);
                con.Close();
                da.Dispose();

                //put data into reportviewer
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                ReportViewer1.LocalReport.Refresh();

                lbStartDateValue.Text = StartDate;
                lbEndDateValue.Text = EndDate;
                lbDept.Visible = false;
                lbDeptValue.Visible = false;
                lbGenerateDateValue.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now);
            }

        }
}
}