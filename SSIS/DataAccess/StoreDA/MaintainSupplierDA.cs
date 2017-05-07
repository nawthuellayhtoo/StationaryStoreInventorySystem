using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.StoreDA
{
    public class MaintainSupplierDA

    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        /*get distinct item category*/
        public List<String> getDistinctCategory()
        {
            List<String> distinctCategoryLst = new List<string>();
            distinctCategoryLst = context.InventoryStocks.Select(x => x.ItemCategory).Distinct().ToList();

            return distinctCategoryLst;
        }

        /*get item list by category*/
        public List<InventoryStock> getItemListByCategory(string category)
        {
            List<InventoryStock> itemLst = context.InventoryStocks.Where(x => x.ItemCategory.Equals(category)).ToList();

            return itemLst;
        }

        /*get item by item name*/
        public InventoryStock getItemByName(string itemName)
        {
            InventoryStock item = context.InventoryStocks.Where(x => x.ItemName.Equals(itemName)).FirstOrDefault();

            return item;
        }

        /*get supplier by supplier id*/
        public Supplier getSupplierBySupplierId(string supplierId)
        {
            Supplier supplier = context.Suppliers.Where(x => x.SupplierID.Equals(supplierId)).FirstOrDefault();

            return supplier;
        }

        /*get update supplier*/
        public int updateSupplier(InventoryStock item)
        {
            InventoryStock stock = getInventoryById(item.ItemNumber);
            stock.Supplier1 = item.Supplier1;
            stock.Supplier2 = item.Supplier2;
            stock.Supplier3 = item.Supplier3;
            stock.Price1 = item.Price1;
            stock.Price2 = item.Price2;
            stock.Price3 = item.Price3;

            context.SaveChanges();

            return 1;
        }

        /*get inventory by item id*/
        public InventoryStock getInventoryById(String itemNumber)
        {
            InventoryStock item = context.InventoryStocks.Where(x => x.ItemNumber.Equals(itemNumber)).FirstOrDefault();
            return item;
        }

        /*get all supplier list*/
        public List<Supplier> getAllSupplier()
        {
            List<Supplier> supList = context.Suppliers.ToList();
            return supList;
        }

        /*update inventory supplier*/
        public int updateStockSupplier(List<InventoryStock> stockList)
        {
            foreach (InventoryStock item in stockList)
            {
                updateSupplier(item);
            }
            return 1;
        }
    }
}
