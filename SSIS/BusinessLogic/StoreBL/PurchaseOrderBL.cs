using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class PurchaseOrderBL
    {
        PurchaseOrderBO pobo;
        PurchaseOrderDA poda;

        public PurchaseOrderBO convertPurchaseOrderBO(PurchaseOrder po)
        {
            pobo = new PurchaseOrderBO();
            pobo.PoId = po.POID;
            pobo.DeliveryDate = po.DeliveryDate;
            pobo.DateOfOrder = po.DateOfOrder;
            pobo.RequestorId = po.RequestorID;
            pobo.CommentByClerk = po.CommentByClerk;
            pobo.ApproverId = po.ApproverID;
            pobo.ApprovalDate = po.ApprovalDate;
            pobo.Status = po.Status;
            return pobo;
        }

        public List<PurchaseOrderBO> getPurchaseOrderList(String userId)
        {
            poda = new PurchaseOrderDA();
            return poda.getPurchaseOrderById(userId);

        }

        public List<PurchaseOrderBO> getPurchaseOrderListAll()
        {
            poda = new PurchaseOrderDA();
            return poda.getPurchaseOrderAll();

        }

        public List<PurchaseOrderDetailsJoinBO> getPurchaseOrderDetailsList(String poId)
        {
            poda = new PurchaseOrderDA();
            return poda.getPurchaseOrderDetailsById(poId);

        }

        public PurchaseOrderBO getStatusAndOrderDateDetails(string poId)
        {
            poda = new PurchaseOrderDA();
            return poda.getStatusAndOrderDateById(poId);
        }

        public EmployeeBO getRequestorName(string requestorId)
        {
            poda = new PurchaseOrderDA();
            return poda.getRequestorName(requestorId);
        }

        public void updateQuantity(string poId, int quantity, string itemNumber)
        {
            poda = new PurchaseOrderDA();
            poda.updateQuantityById(poId, quantity, itemNumber);
        }

        public int deletePurchaseOrderDetails(string poId, string supplier)
        {
            poda = new PurchaseOrderDA();
            return poda.deletePurchaseOrderDetailsyById(poId, supplier);
        }

        public Object getCategoryList()
        {
            poda = new PurchaseOrderDA();
            return poda.getCategory();
        }

        public InventoryStockBO getUOMByItem(string itemName)
        {
            poda = new PurchaseOrderDA();
            return poda.getUOMForItem(itemName);
        }

        public InventoryStockBO getSupplierByItem(string itemName)
        {
            poda = new PurchaseOrderDA();
            return poda.getSupplierForItem(itemName);
        }

        public PurchaseOrderDetailsJoinBO getPriceBySupplier(string itemName, string supplier)
        {
            poda = new PurchaseOrderDA();
            return poda.getPriceForSupplier(itemName, supplier);
        }

        public List<String> getItemNameList(List<InventoryStockBO> itemList)
        {
            List<String> itemNamelst = new List<string>();
            foreach (InventoryStockBO lst in itemList)
            {
                String itemName = lst.ItemName;
                itemNamelst.Add(itemName);
            }
            return itemNamelst;
        }

        public List<String> getCategoryList(List<InventoryStockBO> categoryList)
        {
            List<String> categorylst = new List<string>();
            foreach (InventoryStockBO lst in categoryList)
            {
                String category = lst.ItemCategory;
                categorylst.Add(category);
            }
            return categorylst;
        }

        public List<InventoryStockBO> getItemListByCategory(String category)
        {
            poda = new PurchaseOrderDA();
            List<InventoryStock> stocklst = poda.getItemListByCategory(category);

            List<InventoryStockBO> stockBOlst = convertStockBOList(stocklst);

            return stockBOlst;
        }

        public List<InventoryStockBO> getItemListByCategoryAndSupplier(String category, String supplier)
        {
            poda = new PurchaseOrderDA();
            List<InventoryStock> stocklst = poda.getItemListByCategoryAndSupplier(category, supplier);

            List<InventoryStockBO> stockBOlst = convertStockBOList(stocklst);

            return stockBOlst;
        }

        public List<InventoryStockBO> getCategoryBySupplier(String supplierName)
        {
            poda = new PurchaseOrderDA();
            List<InventoryStock> categorylist = poda.getCategoryListBySupplier(supplierName);

            List<InventoryStockBO> categorylst = convertStockBOList(categorylist);
            return categorylst;
        }

        public List<InventoryStockBO> convertStockBOList(List<InventoryStock> stocklst)
        {
            List<InventoryStockBO> stockBOlst = new List<InventoryStockBO>();
            foreach (InventoryStock item in stocklst)
            {
                InventoryStockBO itemBo = convertStockBO(item);
                stockBOlst.Add(itemBo);
            }
            return stockBOlst;
        }

        public InventoryStockBO convertStockBO(InventoryStock item)
        {
            InventoryStockBO itemBo = new InventoryStockBO();
            itemBo.ItemNumber = item.ItemNumber;
            itemBo.Quantity = item.Quantity;
            itemBo.ItemName = item.ItemName;
            itemBo.ItemCategory = item.ItemCategory;
            itemBo.ReorderLevel = item.ReorderLevel;
            itemBo.ItemUOM = item.ItemUOM;
            itemBo.Bin = item.Bin;
            itemBo.Supplier1 = item.Supplier1;
            itemBo.Supplier2 = item.Supplier2;
            itemBo.Supplier3 = item.Supplier3;
            itemBo.Price1 = (double)item.Price1;
            itemBo.Price2 = (double)item.Price2;
            itemBo.Price3 = (double)item.Price3;
            return itemBo;
        }

        public Boolean updateStatusByPoId(string poId, string status, string supervisorComment, string approverId)
        {
            poda = new PurchaseOrderDA();
            return poda.updateStatus(poId, status, supervisorComment, approverId);
        }

        public PurchaseOrderBO countTotalPO()
        {
            poda = new PurchaseOrderDA();
            return poda.countTotalPoId();
        }

        public PurchaseOrderDetailsBO countTotalPODId()
        {
            poda = new PurchaseOrderDA();
            return poda.countTotalPodId();
        }

        public Boolean createPurchaseOrder(PurchaseOrderBO pobo)
        {
            poda = new PurchaseOrderDA();
            PurchaseOrder po = convertPurchaseOrder(pobo);
            return poda.createPO(po);
        }

        public Boolean createPurchaseOrderDetails(PurchaseOrderDetailsBO pod)
        {
            poda = new PurchaseOrderDA();
            return poda.createPODetails(convertPurchaseOrderDetails(pod));
        }

        public EmployeeBO getSupervisorId()
        {
            poda = new PurchaseOrderDA();
            return poda.getSupervisorId();
        }

        public PurchaseOrder convertPurchaseOrder(PurchaseOrderBO pobo)
        {
            PurchaseOrder po = new PurchaseOrder();
            po.POID = pobo.PoId;
            po.DeliveryDate = pobo.DeliveryDate;
            po.DateOfOrder = pobo.DateOfOrder;
            po.RequestorID = pobo.RequestorId;
            po.CommentByClerk = pobo.CommentByClerk;
            po.ApproverID = pobo.ApproverId;
            po.Status = pobo.Status;
            po.CommentsBySupervisor = pobo.CommentBySupervisor;
            return po;
        }

        public PurchaseOrderDetail convertPurchaseOrderDetails(PurchaseOrderDetailsBO pobo)
        {
            PurchaseOrderDetail pod = new PurchaseOrderDetail();
            pod.PODetailsID = pobo.PoDetailsId;
            pod.POID = pobo.PoId;
            pod.SupplierID = pobo.SupplierId;
            pod.ItemNumber = pobo.ItemNumber;
            pod.Quantity = pobo.Quantity;
            return pod;
        }

        public string getItemNumberByName(string itemName)
        {
            poda = new PurchaseOrderDA();
            return poda.getItemNumber(itemName);
        }

        public Boolean resubmitPO(string poId, string clerkComment)
        {
            poda = new PurchaseOrderDA();
            return poda.resubmitPO(poId, clerkComment);

        }
    }
}
