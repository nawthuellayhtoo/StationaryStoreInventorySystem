using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BusinessLogic;
using BusinessLogic.DepartmentBL;
using System.Data;

namespace SSIS
{
    public partial class RequisitionList : System.Web.UI.Page
    {
        StationeryRequisitionBL srbl = new StationeryRequisitionBL();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            EmployeeBO employeeBO = (EmployeeBO)Session["employee"];
            if (!IsPostBack)
            {      
                List<RetrievalRequisitionListBO> rrlbo = new List<RetrievalRequisitionListBO>();

                if (employeeBO.EmployeeTitle == "Head")
                {
                    rrlbo = srbl.getRequisitionListByDepId(employeeBO.Department.DeptId);
                }
                else if (employeeBO.EmployeeTitle == "Employee" && employeeBO.Delegate == 0)
                {
                    rrlbo = srbl.getRequisitionListByEmpId(employeeBO.EmployeeId);
                }
                else if (employeeBO.EmployeeTitle == "Employee" && employeeBO.Delegate == 1)
                {
                    rrlbo = srbl.getRequisitionListByDepId(employeeBO.Department.DeptId);
                }

                if (rrlbo.Count() == 0)
                {
                    LbNoRequisition.Text = "No Requisition Now";
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[4]
                    {
                new DataColumn("retrievalrequisitionId"),
                new DataColumn("retrievalempName"),
                new DataColumn("retrievalempRequisitionDate"),
                new DataColumn("retrievalstatus")
                    });

                    foreach (RetrievalRequisitionListBO r in rrlbo)
                    {
                        dt.Rows.Add(r.RetrievalrequisitionId, r.RetrievalempName, Convert.ToDateTime(r.RetrievalempRequisitionDate).ToString("dd MMMM, yyyy"), r.Retrievalstatus);
                    }
                    GridViewRequisitionList.DataSource = dt;
                    GridViewRequisitionList.DataBind();

                    Session["RequisitionDetails"] = rrlbo;

                    if (GridViewRequisitionList.Rows.Count != 0)
                    {
                        fooTableSetting();
                    }
                }
            }
          
        }

        protected void GridViewRequisitionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<RetrievalRequisitionListBO> rrlbo = new List<RetrievalRequisitionListBO>();
            EmployeeBO employeeBO = (EmployeeBO)Session["employee"];

            GridViewRequisitionList.PageIndex = e.NewPageIndex;

            if (employeeBO.EmployeeTitle == "Head")
            {
                rrlbo = srbl.getRequisitionListByDepId(employeeBO.Department.DeptId);
            }
            else if (employeeBO.EmployeeTitle == "Employee" && employeeBO.Delegate == 0)
            {
                rrlbo = srbl.getRequisitionListByEmpId(employeeBO.EmployeeId);
            }
            else if (employeeBO.EmployeeTitle == "Employee" && employeeBO.Delegate == 1)
            {
                rrlbo = srbl.getRequisitionListByDepId(employeeBO.Department.DeptId);
            }

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4]
            {
                new DataColumn("retrievalrequisitionId"),
                new DataColumn("retrievalempName"),
                new DataColumn("retrievalempRequisitionDate"),
                new DataColumn("retrievalstatus")
            });

            foreach (RetrievalRequisitionListBO r in rrlbo)
            {
                dt.Rows.Add(r.RetrievalrequisitionId, r.RetrievalempName, Convert.ToDateTime(r.RetrievalempRequisitionDate).ToString("dd MMMM, yyyy"), r.Retrievalstatus);
            }
            GridViewRequisitionList.DataSource = dt;
            GridViewRequisitionList.DataBind();

            Session["RequisitionDetails"] = rrlbo;

            if (GridViewRequisitionList.Rows.Count != 0)
            {
                fooTableSetting();
            }

        }

        public void fooTableSetting()
        {
            //Attribute to show the Plus Minus Button.
            GridViewRequisitionList.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            GridViewRequisitionList.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            GridViewRequisitionList.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            GridViewRequisitionList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}