using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading.Tasks;

namespace DataAccess.DepartmentDA
{
    public class CatalogueListDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        public List<InventoryStockBO> getCatalogList()
        {
            var q = (from i in context.InventoryStocks
                     select new InventoryStockBO
                     {
                         ItemName = i.ItemName,
                         ItemCategory = i.ItemCategory,
                         ItemUOM = i.ItemUOM
                     }).ToList();
            return q;
        }
    }
}

