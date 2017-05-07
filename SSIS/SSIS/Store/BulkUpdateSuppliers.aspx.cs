using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using BusinessLogic.StoreBL;
namespace SSIS
{
    public partial class BulkUpdateSuppliers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        MaintainSupplierBL bl = new MaintainSupplierBL();
        DataSet dataSet = new DataSet();
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (fuSuppliers.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(fuSuppliers.FileName);
                    string filePath = Server.MapPath("UploadFiles") + "\\" + filename;
                    fuSuppliers.SaveAs(Server.MapPath("UploadFiles") + "\\" + filename);
                    lblStatus.Text = "File is uploaded.";

                    if (readExcelFile(Server.MapPath("UploadFiles") + "\\" + filename) == true)
                    {
                        Label1.Text = "Suppliers are updated!";
                        upload(dataSet);
                    }
                    else
                    {
                        Label1.Text = "Suppliers are not updated!";
                    }
                }
                catch (Exception ex)
                {
                  // lblStatus.Text = "Old File exists!";
                }
            }
            else
            {
                lblStatus.Text = "No file is chosen!";
            }
           
        }

        private bool readExcelFile(string strFilePath)
        {
            if (!File.Exists(strFilePath))
            {
                return false;
            }
            String strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + strFilePath + ";" + "Extended Properties='Excel 12.0;HDR=No'";
            OleDbConnection connExcel = new OleDbConnection(strExcelConn);
            OleDbCommand cmdExcel = new OleDbCommand();

            try
            {
                cmdExcel.Connection = connExcel;
                //Check if the Sheet Exists
                connExcel.Open();
                DataTable dtExcelSchema;
                //Get the Schema of the WorkBook
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                connExcel.Close();

                //Read Data from Sheet1
                connExcel.Open();

                OleDbDataAdapter da = new OleDbDataAdapter();
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                da.SelectCommand = cmdExcel;
                da.Fill(dataSet);

                connExcel.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                cmdExcel.Dispose();
                connExcel.Dispose();
            }
        }
        private void upload(DataSet dataSet)
        {
            List<InventoryStockBO> stockBOlst = new List<InventoryStockBO>();

            int count = dataSet.Tables[0].Rows.Count - 1;
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                for (int index = 1; index <= count; index++)
                {
                    InventoryStockBO stockBO = new InventoryStockBO();
                    stockBO.ItemNumber = dataSet.Tables[0].Rows[index].ItemArray[0].ToString();
                    stockBO.ItemName = dataSet.Tables[0].Rows[index].ItemArray[1].ToString();
                    stockBO.Quantity = Convert.ToInt32(dataSet.Tables[0].Rows[index].ItemArray[2].ToString());
                    stockBO.ItemCategory = dataSet.Tables[0].Rows[index].ItemArray[3].ToString();
                    stockBO.ReorderLevel = Convert.ToInt32(dataSet.Tables[0].Rows[index].ItemArray[4].ToString());
                    stockBO.ReorderQuantity = Convert.ToInt32(dataSet.Tables[0].Rows[index].ItemArray[5].ToString());

                    stockBO.ItemUOM = dataSet.Tables[0].Rows[index].ItemArray[6].ToString();
                    stockBO.Bin = dataSet.Tables[0].Rows[index].ItemArray[7].ToString();


                    stockBO.Supplier1 = dataSet.Tables[0].Rows[index].ItemArray[8].ToString();
                    stockBO.Supplier2 = dataSet.Tables[0].Rows[index].ItemArray[9].ToString();
                    stockBO.Supplier3 = dataSet.Tables[0].Rows[index].ItemArray[10].ToString();
                    stockBO.Price1 = Convert.ToDouble(dataSet.Tables[0].Rows[index].ItemArray[11]);
                    stockBO.Price2 = Convert.ToDouble(dataSet.Tables[0].Rows[index].ItemArray[12]);
                    stockBO.Price3 = Convert.ToDouble(dataSet.Tables[0].Rows[index].ItemArray[13]);

                    stockBOlst.Add(stockBO);
                }
                Session["stockBOlst"] = stockBOlst;
                int status = bl.updateStockSupplier(stockBOlst);
                GridView1.DataSource = stockBOlst;
                GridView1.DataBind();
                fooTableSetting();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<InventoryStockBO> stockBOlst= (List<InventoryStockBO>)Session["stockBOlst"];
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = stockBOlst;
            GridView1.DataBind();
            fooTableSetting();

        }
        public void fooTableSetting()
        {
            //Attribute to show the Plus Minus Button.
            GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "expand";
            GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "expand";
            GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[7].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}