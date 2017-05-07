using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.StoreBL;
using Model;
using System.Data;

namespace SSIS
{
    public partial class AdjustmentVoucherList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    EmployeeBO ebo = (EmployeeBO)Session["employee"];
                    string empId = ebo.EmployeeId;
                    string title = ebo.EmployeeTitle;
                    //get session from the login to verify
                    if (Session["employee"] != null)
                    {
                        if (title.Equals("Manager"))
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.AddRange(new DataColumn[5] {
                            new DataColumn("VoucherId", typeof(string)),
                            new DataColumn("EmployeeName", typeof(string)),
                            new DataColumn("VoucherDate",typeof(string)),
                            new DataColumn("TotalPrice",typeof(string)),
                            new DataColumn("AdjustmentStatus",typeof(string))});
                            AdjustmentVoucherListBL avlBL = new AdjustmentVoucherListBL();
                            bool isDelegate = avlBL.checkDelegate("Supervisor");
                            //Check if rights is assigned
                            List<AdjustmentBO> avList = null;
                            if (isDelegate == true)
                            {
                                avList = avlBL.getAdjustmentVoucherListSup();
                                if (avList.Count()==0)
                                {
                                    lblNoVoucher.Text = "No Adjustment Voucher List";
                                }
                                else
                                {
                                    foreach (AdjustmentBO item in avList)
                                    {
                                        dt.Rows.Add(item.VoucherId, item.Employee.EmployeeName, String.Format("{0:dd/MMM/yyyy}", item.VoucherDate), item.TotalPrice, item.AdjustmentStatus);
                                    }

                                    gvInventoryAdjustmentList.DataSource = dt;
                                    gvInventoryAdjustmentList.DataBind();
                                    footableSettings();
                                }
                               
                            }
                            else if (isDelegate == false)
                            {
                                avList = avlBL.getAdjustmentVoucherListMan();
                                if (avList.Count() == 0)
                                {
                                    lblNoVoucher.Text = "No AdjustmentVoucherList";
                                }
                                else
                                {
                                    foreach (AdjustmentBO item in avList)
                                    {
                                        dt.Rows.Add(item.VoucherId, item.Employee.EmployeeName, String.Format("{0:dd/MMM/yyyy}", item.VoucherDate), item.TotalPrice, item.AdjustmentStatus);
                                    }

                                    gvInventoryAdjustmentList.DataSource = dt;
                                    gvInventoryAdjustmentList.DataBind();
                                    footableSettings();
                                }    
                            }
                        }
                        else if (title.Equals("Supervisor"))
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.AddRange(new DataColumn[5] {
                            new DataColumn("VoucherId", typeof(string)),
                            new DataColumn("EmployeeName", typeof(string)),
                            new DataColumn("VoucherDate",typeof(string)),
                            new DataColumn("TotalPrice",typeof(string)),
                            new DataColumn("AdjustmentStatus",typeof(string))});
                            AdjustmentVoucherListBL avlBL = new AdjustmentVoucherListBL();
                            List<AdjustmentBO> avList = avlBL.getAdjustmentVoucherListSup();
                            if (avList.Count() == 0)
                            {
                                lblNoVoucher.Text = "No AdjustmentVoucherList";
                            }
                            else
                            {
                                foreach (AdjustmentBO item in avList)
                                {
                                    dt.Rows.Add(item.VoucherId, item.Employee.EmployeeName, String.Format("{0:dd/MMM/yyyy}", item.VoucherDate), item.TotalPrice, item.AdjustmentStatus);
                                }

                                gvInventoryAdjustmentList.DataSource = dt;
                                gvInventoryAdjustmentList.DataBind();
                                footableSettings();
                            }
                        }
                        else
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.AddRange(new DataColumn[5] {
                            new DataColumn("VoucherId", typeof(string)),
                            new DataColumn("EmployeeName", typeof(string)),
                            new DataColumn("VoucherDate",typeof(string)),
                            new DataColumn("TotalPrice",typeof(string)),
                            new DataColumn("AdjustmentStatus",typeof(string))});
                            AdjustmentVoucherListBL avlBL = new AdjustmentVoucherListBL();
                            List<AdjustmentBO> avList = avlBL.getAdjustmentVoucherListByClerk(empId);
                            if (avList.Count() == 0)
                            {
                                lblNoVoucher.Text = "No AdjustmentVoucherList";
                            }
                            else
                            {
                                foreach (AdjustmentBO item in avList)
                                {
                                    dt.Rows.Add(item.VoucherId, item.Employee.EmployeeName, String.Format("{0:dd/MMM/yyyy}", item.VoucherDate), item.TotalPrice, item.AdjustmentStatus);
                                }

                                gvInventoryAdjustmentList.DataSource = dt;
                                gvInventoryAdjustmentList.DataBind();
                                footableSettings();
                            }
                        }
                    }
                }
                catch (Exception x)
                {
                    System.Diagnostics.Debug.WriteLine(x);
                }
            }
        }

        protected void gvInventoryAdjustmentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        { // same logic as above, but it for pagination
            try
            {
                EmployeeBO ebo = (EmployeeBO)Session["employee"];
                string empId = ebo.EmployeeId;
                string title = ebo.EmployeeTitle;

                if (Session["employee"] != null)
                {
                    if (title.Equals("Manager")) // manager login
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.AddRange(new DataColumn[5] {
                            new DataColumn("VoucherId", typeof(string)),
                            new DataColumn("EmployeeName", typeof(string)),
                            new DataColumn("VoucherDate",typeof(string)),
                            new DataColumn("TotalPrice",typeof(string)),
                            new DataColumn("AdjustmentStatus",typeof(string))});
                        AdjustmentVoucherListBL avlBL = new AdjustmentVoucherListBL();
                        bool isDelegate = avlBL.checkDelegate(title);
                        List<AdjustmentBO> avList = null;
                        if (isDelegate == true) //if delegate is true
                        {
                            avList = avlBL.getAdjustmentVoucherListSup(); //bind data for supervisor
                        }
                        else if (isDelegate == false) //if delegate is false
                        {
                            avList = avlBL.getAdjustmentVoucherListMan();//bind data for manager
                        }

                        foreach (AdjustmentBO item in avList)
                        {
                            dt.Rows.Add(item.VoucherId, item.Employee.EmployeeName, String.Format("{0:dd/MMM/yyyy}", item.VoucherDate), item.TotalPrice, item.AdjustmentStatus);
                        }
                        gvInventoryAdjustmentList.PageIndex = e.NewPageIndex;
                        gvInventoryAdjustmentList.DataSource = dt;
                        gvInventoryAdjustmentList.DataBind();
                        footableSettings();
                    }
                    else if (title.Equals("Supervisor")) // Supervisor login
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.AddRange(new DataColumn[5] {
                            new DataColumn("VoucherId", typeof(string)),
                            new DataColumn("EmployeeName", typeof(string)),
                            new DataColumn("VoucherDate",typeof(string)),
                            new DataColumn("TotalPrice",typeof(string)),
                            new DataColumn("AdjustmentStatus",typeof(string))});
                        AdjustmentVoucherListBL avlBL = new AdjustmentVoucherListBL();
                        List<AdjustmentBO> avList = avlBL.getAdjustmentVoucherListSup();
                        foreach (AdjustmentBO item in avList)
                        {
                            dt.Rows.Add(item.VoucherId, item.Employee.EmployeeName, String.Format("{0:dd/MMM/yyyy}", item.VoucherDate), item.TotalPrice, item.AdjustmentStatus);
                        }
                        gvInventoryAdjustmentList.PageIndex = e.NewPageIndex;
                        gvInventoryAdjustmentList.DataSource = dt;
                        gvInventoryAdjustmentList.DataBind();
                        footableSettings();
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.AddRange(new DataColumn[5] {
                            new DataColumn("VoucherId", typeof(string)),
                            new DataColumn("EmployeeName", typeof(string)),
                            new DataColumn("VoucherDate",typeof(string)),
                            new DataColumn("TotalPrice",typeof(string)),
                            new DataColumn("AdjustmentStatus",typeof(string))});
                        AdjustmentVoucherListBL avlBL = new AdjustmentVoucherListBL();
                        List<AdjustmentBO> avList = avlBL.getAdjustmentVoucherListByClerk(empId);
                        foreach (AdjustmentBO item in avList)
                        {
                            dt.Rows.Add(item.VoucherId, item.Employee.EmployeeName, String.Format("{0:dd/MMM/yyyy}", item.VoucherDate), item.TotalPrice, item.AdjustmentStatus);
                        }
                        gvInventoryAdjustmentList.PageIndex = e.NewPageIndex;
                        gvInventoryAdjustmentList.DataSource = dt;
                        gvInventoryAdjustmentList.DataBind();
                        footableSettings();
                    }
                }
            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine(x);
            }
        }

        protected void footableSettings()
        {
            //Attribute to show the Plus Minus Button.
            gvInventoryAdjustmentList.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            gvInventoryAdjustmentList.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            gvInventoryAdjustmentList.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            gvInventoryAdjustmentList.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            //Adds THEAD and TBODY to GridView.
            gvInventoryAdjustmentList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}