using System;
using DataAccess;
using DataAccess.StoreDA;
using Model;
using System.Collections.Generic;


namespace BusinessLogic.StoreBL
{
    public class CreateAdjustmentVoucherBL
    {
        CreateAdjustmentVoucherDA cav = new CreateAdjustmentVoucherDA();
        InventoryStockBO ibo;

        public InventoryStockBO convertInventoryStockBO(InventoryStock i)
        {
            ibo = new InventoryStockBO();
            ibo.ItemCategory = i.ItemCategory;

            return ibo;
        }
        public List<string> getDistinctCategory()
        {
            List<string> catList = new List<string>();
            catList = cav.getDistinctCategory();

            return catList;
        }

        public List<InventoryStockBO> getItemListByCategory(String category)
        {
            List<InventoryStockBO> catList = new List<InventoryStockBO>();
            catList = cav.getItemListByCategory(category);

            return catList;
        }

        public string getUOM(string itemNumber)
        {
            string catList = cav.getUOM(itemNumber);
            return catList;
        }

        public InventoryStockBO getSupplier1(String itemNumber)
        {
            List<InventoryStockBO> supList = new List<InventoryStockBO>();
            InventoryStockBO retList = new InventoryStockBO();
            InventoryStockBO ibo = new InventoryStockBO();
            supList = cav.getSupplierListFromItemNumber(itemNumber);
            foreach (InventoryStockBO inv in supList)
            {
                ibo.Supplier1 = inv.Supplier1;
                ibo.Price1 = inv.Price1;
                retList = ibo;
            }
            return retList;
        }

        public InventoryStockBO getSupplier2(String itemNumber)
        {
            List<InventoryStockBO> supList = new List<InventoryStockBO>();
            InventoryStockBO retList = new InventoryStockBO();
            InventoryStockBO ibo = new InventoryStockBO();
            supList = cav.getSupplierListFromItemNumber(itemNumber);
            foreach (InventoryStockBO inv in supList)
            {
                ibo.Supplier1 = inv.Supplier2;
                ibo.Price1 = inv.Price2;
                retList = ibo;
            }
            return retList;
        }

        public InventoryStockBO getSupplier3(String itemNumber)
        {
            List<InventoryStockBO> supList = new List<InventoryStockBO>();
            InventoryStockBO retList = new InventoryStockBO();
            InventoryStockBO ibo = new InventoryStockBO();
            supList = cav.getSupplierListFromItemNumber(itemNumber);
            foreach (InventoryStockBO inv in supList)
            {
                ibo.Supplier1 = inv.Supplier3;
                ibo.Price1 = inv.Price3;
                retList = ibo;
            }
            return retList;
        }

        public string getAdjustmentVoucherId()
        {
            string adjList = cav.getAdjustmentVoucherId();
            return adjList;
        }
        protected void SendEmail(string empID, string voId)
        {
            SendEmail se = new SendEmail();
            EmployeeBO eboSup = getApprover("Supervisor");

            string name = getApplier(empID);
            string sub = "Adjustment Voucher Created";
            string body = "Dear " + eboSup.EmployeeName + ", \n"
                        + "\n" + "An adjustment voucher " + voId + " has been created by " + name + " for your approval."
                        + "\n" + "(This is an auto-generated email, please do not reply.)"
                        + "\n\n\n" + "Regards,"
                        + "\n" + "Admin";
            se.sendCPEmail(sub, body, eboSup.EmployeeEmail);
        }
        public string getApplier(string empId)
        {
            return cav.getApplier(empId);
        }
        public EmployeeBO getApprover(string title)
        {
            return cav.getApprover(title);
        }
        public Boolean createAdjustmentVoucher(string empID, double totalPrice)
        {
            bool status = false;
            string preVNum = getAdjustmentVoucherId();
            int num = preVNum.Length - 2;
            int incre = Convert.ToInt32(preVNum.Substring(2, num)) + 1;
            string postNum = "V0" + incre.ToString();

            if (totalPrice < 0)
            {
                totalPrice = totalPrice * -1;
            }
            status = cav.createAdjustmentVoucher(postNum, empID, totalPrice);

            SendEmail(empID, postNum); // Apon creating Adjustment Voucher, Email will be sent.
            return status;
        }
        public void createAdjustmentVoucherDetails(string adNum, string vID, string itemNum, int qty, string supp, string reason)
        {
            if (reason.Equals("&nbsp;"))
            {
                cav.createAdjustmentVoucherDetails(adNum, vID, itemNum, qty.ToString(), supp, "NULL");
            }
            else
            {
                cav.createAdjustmentVoucherDetails(adNum, vID, itemNum, qty.ToString(), supp, reason);
            }
        }
        public string getAdjustmentDetailsVoucherId()
        {
            string adjList = cav.getAdjustmentDetailsVoucherId();
            return adjList;
        }
    }
}
