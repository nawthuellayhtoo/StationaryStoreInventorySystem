using System;
using DataAccess;
using Model;
using System.Collections.Generic;
using DataAccess.StoreDA;

namespace BusinessLogic.StoreBL
{
    public class InventoryListBL
    {
        InventoryListDA da = new InventoryListDA();
        InventoryStockBO ibo;

        public InventoryStockBO convertInventoryStockBO(InventoryStock i)
        {
            ibo = new InventoryStockBO();
            ibo.Bin = i.Bin;
            ibo.ItemNumber = i.ItemNumber;
            ibo.ItemName = i.ItemName;
            ibo.ItemCategory = i.ItemCategory;
            ibo.Quantity = i.Quantity;
            ibo.ItemUOM = i.ItemUOM;
            ibo.Price1 = Convert.ToDouble(i.Price1);

            return ibo;
        }
        public List<InventoryStockBO> getInventoryList()
        {
            List<InventoryStock> invstock = da.getInventoryList();
            List<InventoryStockBO> invstockBO = new List<InventoryStockBO>();
            foreach (InventoryStock k in invstock)
            {
                invstockBO.Add(convertInventoryStockBO(k));
            }
            return invstockBO;
        }
    }
}
