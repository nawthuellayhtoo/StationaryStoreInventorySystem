using BusinessLogic;
using Model;
using System;
using System.Web.UI;

namespace SSIS
{
    public partial class Login : System.Web.UI.Page
    {

       protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoginBL bl = new LoginBL();
            string userid = tbUserId.Text;
            string password = tbPassword.Text;

            //verify user's login and redirect to correct page accordingly based on their title

            EmployeeBO ebo = bl.authenticate(userid, password);

            if (ebo != null)
            {
                Session["employee"] = ebo;

                bool isDelegate = bl.checkDelegate(ebo);

                if (ebo.EmployeeTitle == "Head")
                {
                    if (isDelegate == true)
                    {
                        Response.Redirect("~/Department/DepartmentDelegate.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/Department/RequisitionList.aspx");
                    }
                }

                if (ebo.EmployeeTitle == "Employee")
                {
                    EmployeeBO hbo = bl.getDeptHeadByDepId(ebo.EmployeeDept);

                    if (hbo != null)
                    {
                        bool isDelegateHead = bl.checkDelegate(hbo);

                        if (isDelegateHead.Equals(true))
                        {
                            Response.Redirect("~/Department/RequisitionList.aspx");
                        }

                        else
                        {
                            Response.Redirect("~/Department/CreateRequisition.aspx");
                        }
                    }
                }

                if (ebo.EmployeeTitle == "Manager")
                {
                    if (isDelegate == true)
                    {
                        Response.Redirect("~/Store/StoreDelegate.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/Store/AdjustmentVoucherList.aspx");
                    } 
                }

                if (ebo.EmployeeTitle == "Supervisor")
                {
                    if (isDelegate == true)
                    {
                        Response.Redirect("~/Store/StoreDelegate.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/Store/POList.aspx");
                    }
                }

                if (ebo.EmployeeTitle == "Clerk")
                {
                    Response.Redirect("~/Store/RetrievalList.aspx");
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Login unsuccessful. Please try again.')", true);
            }
        }
    }
}