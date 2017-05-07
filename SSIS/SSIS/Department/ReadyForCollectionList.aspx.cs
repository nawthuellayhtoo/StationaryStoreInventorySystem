using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessLogic.DepartmentBL;
using Model;
using System.Collections;
namespace SSIS
{
    public partial class ReadyForCollectionList : System.Web.UI.Page
    {
        string deptid;
        EmployeeBO ebo;
        DisbursementBL dbl = new DisbursementBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            ebo = (EmployeeBO)Session["employee"];
            deptid = ebo.EmployeeDept;
            if (!IsPostBack)
            {
                List<DisbursedInventoryBO> lst = dbl.getDisbursementListByDepId(deptid);
                if (lst.Count() == 0)
                {
                    btnUpdate.Visible = false;
                    lblStatus.Text = "No Ready For Collection List";
                    cbAll.Visible = false;
                }
                else
                {
                    GridView1.DataSource = lst;
                    GridView1.DataBind();
                    GridView1.Columns[1].Visible = false;
                    GridView1.Columns[4].Visible = false;
                    GridView1.Columns[7].Visible = false;
                    fooTableSetting();
                }

            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateDisbursement();
            Response.Redirect("~/Department/ReadyForCollectionList.aspx");
        }

        public void UpdateDisbursement()
        {
            ebo = (EmployeeBO)Session["employee"];
            deptid = ebo.EmployeeDept;
            List<ReadyForCollectionBO> rcboLst = new List<ReadyForCollectionBO>();
            List<OutstandingInfoBO> oLst = new List<OutstandingInfoBO>();
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox c = (CheckBox)row.FindControl("choose");


                    if (c.Checked)
                    {
                        TextBox t = (TextBox)row.FindControl("tbDisbursementQty");
                        string orderedQTy = row.Cells[6].Text;                                                       //3rd feb
                        string distributedQty = t.Text;                                                             //3rd feb
                        if (Convert.ToInt32(row.Cells[3].Text) != 0)                                               //3rd feb
                        {

                            ReadyForCollectionBO rcbo = new ReadyForCollectionBO();
                            rcbo.DepId = deptid;

                            rcbo.ItemNumber = row.Cells[1].Text;
                            rcbo.ReqisitionId = row.Cells[7].Text;
                            rcbo.DisbursedQuantity = Convert.ToInt32(t.Text);
                            rcboLst.Add(rcbo);
                        }
                        else                                                                                      //3rd feb
                        {
                            OutstandingInfoBO o = dbl.getOutstanding(row.Cells[1].Text, deptid);
                           
                                o.Quantity = Convert.ToInt32(orderedQTy)+o.Quantity - Convert.ToInt32(distributedQty);

                            

                            dbl.updateOutstanding(row.Cells[1].Text, deptid, (int)o.Quantity);
                        }

                    }
                }
            }

            dbl.updateDisbursementStatus(rcboLst);
        }

        protected void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox c = (CheckBox)row.FindControl("choose");
                    c.Checked = true;
                }
            }
        }
        public void fooTableSetting()
        {
            //Attribute to show the Plus Minus Button.
            GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
  
            //Adds THEAD and TBODY to GridView.
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}