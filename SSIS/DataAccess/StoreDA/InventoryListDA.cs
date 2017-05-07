using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.StoreDA
{
    public class InventoryListDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        public List<InventoryStock> getInventoryList()
        {
            List<InventoryStock> i = new List<InventoryStock>();

            var query = (from x in context.InventoryStocks
                         select new { x.Bin, x.ItemNumber, x.ItemName, x.ItemCategory, x.Quantity, x.ItemUOM, x.Price1 }).ToList();
            foreach (var q in query)
            {
                InventoryStock b = new InventoryStock();
                b.Bin = q.Bin;
                b.ItemNumber = q.ItemNumber;
                b.ItemName = q.ItemName;
                b.ItemCategory = q.ItemCategory;
                b.Quantity = q.Quantity;
                b.ItemUOM = q.ItemUOM;
                b.Price1 = Convert.ToInt32(q.Price1);
                i.Add(b);
            }
            return i;
        }

    }
}
