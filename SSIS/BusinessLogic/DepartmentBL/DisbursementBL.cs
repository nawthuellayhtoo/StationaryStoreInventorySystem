using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.DepartmentDA;
using Model;
using System.Collections;


namespace BusinessLogic.DepartmentBL
{
    public class DisbursementBL
    {
        DisbursementDA d = new DisbursementDA();
        DisbursementBO dbo;
        InventoryStockBO ibo;


        public DisbursementBO convertDisbursementBO(Disbursement e)
        {
            dbo = new DisbursementBO();
            dbo.DepartmentId = e.DepartmentID;
            dbo.DisbursementId = e.DisbursementID;
            dbo.ItemNumber = e.ItemNumber;
            dbo.OrderQuantity = e.OrderQuantity;
            dbo.DisbursementQuantity = e.DisbursementQuantity;
            dbo.Status = e.Status;
            dbo.RequisitionId = e.RequisitionID;
            return dbo;
        }

        public InventoryStockBO convertInventoryStockBO(InventoryStock i)
        {
            ibo = new InventoryStockBO();
            ibo.ItemNumber = i.ItemNumber;
            ibo.ItemName = i.ItemName;
            ibo.Quantity = i.Quantity;
            ibo.ItemCategory = i.ItemCategory;
            ibo.ReorderLevel = i.ReorderLevel;
            ibo.ReorderQuantity = i.ReorderQuantity;
            ibo.ItemUOM = i.ItemUOM;
            ibo.Bin = i.Bin;
            ibo.Supplier1 = i.Supplier1;
            ibo.Supplier2 = i.Supplier2;
            ibo.Supplier3 = i.Supplier3;
            ibo.Price1 = (double)i.Price1;
            ibo.Price2 = (double)i.Price2;
            ibo.Price3 = (double)i.Price3;

            return ibo;
        }

        public List<DisbursedInventoryBO> getDisbursementListByDepId(string depId) //To get disbursemnt list 
        {
            List<DisbursedInventoryBO> dis = d.getDisListByDepId(depId);
            return dis;

        }

        public OutstandingInfoBO getOutstanding(string itemNumber, string depId) //To get outstanding by depId
        {
            OutstandingInfo oi = d.getOutstandingByItemNumberAndDepId(itemNumber, depId);
            OutstandingInfoBO o = convertOutstandingToOutstandingBO(oi);
            return o;
        }
        public OutstandingInfoBO convertOutstandingToOutstandingBO(OutstandingInfo o)
        {
            OutstandingInfoBO obo = new OutstandingInfoBO();
            obo.ItemNumber = o.ItemNumber;
            obo.Quantity = o.Quantity;
            obo.OutstandingId = o.OutstandingID;
            obo.DepartmentId = o.DepartmentID;
            obo.PartialPendingQuantity = o.PartialPendingQty;
            obo.Status = o.Status;

            return obo;
        }
        public void updateDisbursementStatus(List<ReadyForCollectionBO> rcboLst)// Update disbursement table
        {
            d.updateDisList(rcboLst);

        }

        public void updateOutstanding(string itemNumber, string depId, int quantity)//Update outstanding table
        {
            d.updateOutstnading(itemNumber, depId, quantity);
        }
    }
}
