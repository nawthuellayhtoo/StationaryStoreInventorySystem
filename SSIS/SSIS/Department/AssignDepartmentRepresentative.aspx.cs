using BusinessLogic;
using BusinessLogic.DepartmentBL;
using Model;
using System;

namespace SSIS
{
    public partial class AssignDepartmentRepresentative : System.Web.UI.Page
    {
        SendEmail email = new SendEmail();
        AssignDepartmentRepresentativeBL bl = new AssignDepartmentRepresentativeBL();
        EmployeeBO ebo, rep;

        protected void Page_Load(object sender, EventArgs e)
        {
            ebo = (EmployeeBO)Session["employee"];

            //populate with employees from the head's dept

            if (!IsPostBack)
            {
                ddlAssignRep.DataSource = bl.getEmployeeListByDepartmentId(ebo.Department.DeptId);
                ddlAssignRep.DataTextField = "EmployeeName";
                ddlAssignRep.DataValueField = "EmployeeId";
                ddlAssignRep.DataBind();
            }

            rep = bl.getDepartmentRep(ebo.Department.DeptId);
            tbCurrentRep.Text = rep.EmployeeName;

        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            //assign of rep with email notification

            ebo = (EmployeeBO)Session["employee"];
            string sub, body, emailid, empId;

            rep = bl.getDepartmentRep(ebo.Department.DeptId);
            sub = "You have been selected as the Department Representative";
            emailid = rep.EmployeeEmail;
            body = "Dear " + rep.EmployeeName + ",\n" + "\n" + "Congratulations! You have been selected as the Department Representative.\n\n" + "Warmest Regards,\n" + ebo.EmployeeName + "\nDepartment Head";
            email.sendCPEmail(sub, body, emailid);

            empId = ddlAssignRep.SelectedValue;
            bl.assignDepartmentRepresentative(empId);

            Response.Redirect(Request.RawUrl);
        }
    }
}