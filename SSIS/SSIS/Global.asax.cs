using System;

namespace SSIS
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            System.Web.UI.ScriptManager.ScriptResourceMapping.AddDefinition
    ("jquery",
     new System.Web.UI.ScriptResourceDefinition
     {
         Path = "~/scripts/jquery-1.12.4.min.js",
         DebugPath = "~/scripts/jquery-1.12.4.js",
         CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.12.4.min.js",
         CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.12.4.js"
     }
    );
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["employee"] = null;
            Session["stockBOlst"] = null;
            Session["RequisitionDetails"] = null;
            Session["AddRequisitionTable"] = null;

            Session["createPO"] = null;
            Session["count"] = 0;
            Session["gvCount"] = 0;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Session["employee"] = null;
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}