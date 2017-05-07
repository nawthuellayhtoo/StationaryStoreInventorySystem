using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccess.StoreDA;
using DataAccess;

namespace BusinessLogic.StoreBL
{
    public class MaintainSupplierBL
    {
        MaintainSupplierDA maintainSupplierDA = new MaintainSupplierDA();
        public List<String> getDistinctCategory()
        {
            List<String> distinctCategoryLst = maintainSupplierDA.getDistinctCategory();

            return distinctCategoryLst;
        }

        public List<InventoryStockBO> getItemListByCategory(String category)
        {
            List<InventoryStock> stocklst = maintainSupplierDA.getItemListByCategory(category);

            List<InventoryStockBO> stockBOlst = convertStockBOList(stocklst);

            return stockBOlst;
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

        public InventoryStockBO getItemByName(string itemName)
        {
            InventoryStock item = maintainSupplierDA.getItemByName(itemName);
            InventoryStockBO itemBo = convertStockBO(item);
            return itemBo;
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

        public InventoryStock convertInventoryStock(InventoryStockBO itemBo)
        {
            InventoryStock item = new InventoryStock();
            item.ItemNumber = itemBo.ItemNumber;
            item.Quantity = itemBo.Quantity;
            item.ItemName = itemBo.ItemName;
            item.ItemCategory = itemBo.ItemCategory;
            item.ReorderLevel = itemBo.ReorderLevel;
            item.ItemUOM = itemBo.ItemUOM;
            item.Bin = itemBo.Bin;
            item.Supplier1 = itemBo.Supplier1;
            item.Supplier2 = itemBo.Supplier2;
            item.Supplier3 = itemBo.Supplier3;
            item.Price1 = (decimal)itemBo.Price1;
            item.Price2 = (decimal)itemBo.Price2;
            item.Price3 = (decimal)itemBo.Price3;
            return item;
        }

        public SupplierBO getSupplierBySupplierId(String supplierId)
        {
            Supplier supplier = maintainSupplierDA.getSupplierBySupplierId(supplierId);
            SupplierBO supplierBO = convertSupplierBO(supplier);
            return supplierBO;
        }

        public SupplierBO convertSupplierBO(Supplier supplier)
        {
            SupplierBO supplierBO = new SupplierBO();
            supplierBO.SupplierId = supplier.SupplierID;
            supplierBO.SupplierName = supplier.SupplierName;
            supplierBO.ContactName = supplier.ContactName;
            supplierBO.PhoneNumber = supplier.PhoneNumber;
            supplierBO.Address = supplier.Address;
            supplierBO.Fax = supplier.Fax;
            supplierBO.Email = supplier.Email;
            supplierBO.GstRegistrationNumber = supplier.GSTRegistrationNumber;

            return supplierBO;
        }

        public int updateSupplier(InventoryStockBO itemBO)
        {
            InventoryStock item = convertInventoryStock(itemBO);
            int status = maintainSupplierDA.updateSupplier(item);

            return status;
        }

        public List<SupplierBO> getAllSupplier()
        {
            List<Supplier> supList = maintainSupplierDA.getAllSupplier();
            List<SupplierBO> supBOList = convertToSupplerBOList(supList);
            return supBOList;
        }

        public List<SupplierBO> convertToSupplerBOList(List<Supplier> supList)
        {
            List<SupplierBO> supBOList = new List<SupplierBO>();
            foreach (Supplier sup in supList)
            {
                SupplierBO supBo = convertSupplierBO(sup);
                supBOList.Add(supBo);
            }

            return supBOList;
        }

        public int updateStockSupplier(List<InventoryStockBO> stockBOlist)
        {
            List<InventoryStock> stockLst = convertStockList(stockBOlist);
            int status = maintainSupplierDA.updateStockSupplier(stockLst);

            return status;
        }

        public List<InventoryStock> convertStockList(List<InventoryStockBO> stockBOlst)
        {
            List<InventoryStock> stocklst = new List<InventoryStock>();
            foreach (InventoryStockBO item in stockBOlst)
            {
                InventoryStock itemBo = convertInventoryStock(item);
                stocklst.Add(itemBo);
            }
            return stocklst;
        }
    }
}
