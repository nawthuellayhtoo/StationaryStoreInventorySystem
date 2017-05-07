using BusinessLogic;
using BusinessLogic.DepartmentBL;
using Model;
using System;

namespace SSIS
{
    public partial class DepartmentDelegate : System.Web.UI.Page
    {
        SendEmail email = new SendEmail();
        DepartmentDelegateBL bl = new DepartmentDelegateBL();
        string empId;
        EmployeeBO ebo, delegatedEmployee;

        protected void Page_Load(object sender, EventArgs e)
        {
            ebo = (EmployeeBO)Session["employee"];
            Page.DataBind();
            
            //populate the dropdown list with employees from the logged in head's dept

            if (!IsPostBack)
            {
                ddlEmployee.DataSource = bl.getEmployeeListByDepartmentId(ebo.Department.DeptId);
                ddlEmployee.DataTextField = "EmployeeName";
                ddlEmployee.DataValueField = "EmployeeId";
                ddlEmployee.DataBind();
            }

            //check delegate for the controls' visiblity

            if (ebo.Delegate.Equals(0))
            {
                btnDeactivate.Visible = false;
            }

            if (ebo.Delegate.Equals(1))
            {
                btnActivate.Visible = false;
                ddlEmployee.Enabled = false;
                tbStartDate.Enabled = false;
                tbEndDate.Enabled = false;

                delegatedEmployee = bl.getDelegatedEmployee(ebo.EmployeeDept);
                ddlEmployee.SelectedValue = Convert.ToString(delegatedEmployee.EmployeeId);
                tbStartDate.Text = DateTime.Parse(Convert.ToString(ebo.DelegateStartDate)).ToString("yyyy-MM-dd");
                tbEndDate.Text = DateTime.Parse(Convert.ToString(ebo.DelegateEndDate)).ToString("yyyy-MM-dd");
            }
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            ebo = (EmployeeBO)Session["employee"];
            empId = ddlEmployee.SelectedValue;

            DateTime startDate = DateTime.Parse(tbStartDate.Text);
            DateTime endDate = DateTime.Parse(tbEndDate.Text);

            //activates delegation based on inputted values

            bl.activate(ebo.EmployeeDept, empId, startDate, endDate);

            //sending of email notification to delegated employee

            delegatedEmployee = bl.getDelegatedEmployee(ebo.Department.DeptId);
            string sub, body, emailid;

            sub = "Department Head Delegation";
            body = "Dear " + delegatedEmployee.EmployeeName + ",\n" + "\n" + "Congratulations! You have been selected as my delegate from " + tbStartDate.Text + " to " + tbEndDate.Text + ".\n\n" + "Warmest Regards,\n" + ebo.EmployeeName + "\nDepartment Head"; ;
            emailid = delegatedEmployee.EmployeeEmail;

            email.sendCPEmail(sub, body, emailid);

            ebo.Delegate = 1;
            ebo.DelegateStartDate = startDate;
            ebo.DelegateEndDate = endDate;
            Session["employee"] = ebo;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            //deactivation of employee delegation and setting new session delegate value
            ebo = (EmployeeBO)Session["employee"];
            delegatedEmployee = bl.getDelegatedEmployee(ebo.EmployeeDept);
            bl.deactivate(ebo, delegatedEmployee);

            ebo.Delegate = 0;
            Session["employee"] = ebo;
            Response.Redirect(Request.RawUrl);
        }
    }
}