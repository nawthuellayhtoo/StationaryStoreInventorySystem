using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.DepartmentBL;
using BusinessLogic.StoreBL;
using Model;

namespace SSIS
{
    public partial class ChangeCollectionPoint : System.Web.UI.Page
    {
        string deptid;
        EmployeeBO ebo;
        string cp;
        CollectionPointBL cbl = new CollectionPointBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            CollectionPointBL cbl = new CollectionPointBL();
            ebo = (EmployeeBO)Session["employee"];
            deptid = ebo.EmployeeDept;

            if (!IsPostBack)
            {
                
                rblCollectionPoint.DataSource = cbl.getAllCollectionPointList();
                rblCollectionPoint.SelectedValue = cbl.getCollectionPoint(deptid);
                rblCollectionPoint.DataBind();
            }
            cp = rblCollectionPoint.SelectedValue;
            tbDepName.Text = cbl.getCollectionTime(cp);
            String currentPoint = cbl.getCollectionPoint(deptid);
            String current = cbl.getCollectionPoint(deptid).ToString()+ " ("+ cbl.getCollectionTime(currentPoint) +")";
            lblCurrentCollectionPoint.Text = current;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ebo = (EmployeeBO)Session["employee"];
            deptid = ebo.EmployeeDept;
            string cp = rblCollectionPoint.SelectedValue;

            int status=cbl.updateCollectionPoint(deptid, cp);
            
            if (status > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated successfully')", true);
            }
            Response.Redirect("~/Department/ChangeCollectionPoint.aspx");

        }
    }
}