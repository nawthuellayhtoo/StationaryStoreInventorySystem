using System;
using BusinessLogic;
using BusinessLogic.StoreBL;
using Model;
using System.Web.UI;

namespace SSIS
{
    public partial class StoreDelegate : System.Web.UI.Page
    {
        SendEmail email = new SendEmail();
        StoreDelegateBL bl = new StoreDelegateBL();
        EmployeeBO ebo, storeSupervisor, storeManager;
        DateTime startDate, endDate;
        string empTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            ebo = (EmployeeBO)Session["employee"];
            storeSupervisor = bl.getStoreSupervisorBO();
            storeManager = bl.getStoreManagerBO();
            Page.DataBind();

            // checks whether manager or supervisor to determine controls

            if (!IsPostBack)
            {
                if (ebo.EmployeeTitle.Equals("Manager"))
                {
                    tbDelegate.Text = storeSupervisor.EmployeeName;
                }

                else if (ebo.EmployeeTitle.Equals("Supervisor"))
                {
                    tbDelegate.Text = storeManager.EmployeeName;
                }
            }

            if (ebo.Delegate.Equals(0))
            {
                btnDeactivate.Visible = false;
            }

            if (ebo.Delegate.Equals(1))
            {
                btnActivate.Visible = false;
                tbStartDate.Enabled = false;
                tbEndDate.Enabled = false;
                tbStartDate.Text = DateTime.Parse(Convert.ToString(ebo.DelegateStartDate)).ToString("yyyy-MM-dd");
                tbEndDate.Text = DateTime.Parse(Convert.ToString(ebo.DelegateEndDate)).ToString("yyyy-MM-dd");
            }
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {

            //activate delegation and send email notification

            ebo = (EmployeeBO)Session["employee"];
            empTitle = ebo.EmployeeTitle;
            storeSupervisor = bl.getStoreSupervisorBO();
            storeManager = bl.getStoreManagerBO();
            startDate = DateTime.Parse(tbStartDate.Text);
            endDate = DateTime.Parse(tbEndDate.Text);

            string sub = "Store Delegation";
            string body = null;
            string emailid = null;

            if (empTitle.Equals("Manager"))
            {
                emailid = storeSupervisor.EmployeeEmail;
                body = "Dear " + storeSupervisor.EmployeeName + ",\n" + "\n" + "Congratulations! You have been selected as my delegate from " + startDate + " to " + endDate + ".\n\n" + "Warmest Regards,\n" + ebo.EmployeeName + "\nStore Manager";
            }

            else if (empTitle.Equals("Supervisor"))
            {
                emailid = storeManager.EmployeeEmail;
                body = "Dear " + storeManager.EmployeeName + ",\n" + "\n" + "Congratulations! You have been selected as my delegate from " + startDate + " to " + endDate + ".\n\n" + "Warmest Regards,\n" + ebo.EmployeeName + "\nStore Supervisor";
            }

            email.sendCPEmail(sub, body, emailid);

            bl.activate(empTitle, startDate, endDate);

            ebo.Delegate = 1;
            ebo.DelegateStartDate = startDate;
            ebo.DelegateEndDate = endDate;
            Session["employee"] = ebo;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            //deactivation of delegation and setting new session delegate value

            ebo = (EmployeeBO)Session["employee"];
            bl.deactivate(ebo.EmployeeTitle);

            ebo.Delegate = 0;
            Session["employee"] = ebo;
            Response.Redirect(Request.RawUrl);
        }
    }
}