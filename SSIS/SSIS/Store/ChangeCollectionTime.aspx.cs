using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.StoreBL;
using BusinessLogic.DepartmentBL;
using Model;


namespace SSIS.Store
{
    public partial class ChangeCollectionTime : System.Web.UI.Page
    {
        CollectionTimeBL cbl = new CollectionTimeBL();
        string cp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList1.DataSource = cbl.getAllCollectionPointList();
                DropDownList1.DataBind();
                DropDownList2.DataSource = cbl.getAllCollectionTimeList();
                DropDownList2.DataBind();
                cp = DropDownList1.SelectedValue;
                DropDownList2.SelectedValue = cbl.getCollectionTime(cp);
            }
        }

         protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string ct = DropDownList2.SelectedValue;
            string cp1 = DropDownList1.SelectedValue;
            int status=cbl.updateCollectionTime(cp1, ct);
            if (status>0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated successfully')", true);
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cp = DropDownList1.SelectedValue;
            DropDownList2.SelectedValue = cbl.getCollectionTime(cp);
        }
    }
}