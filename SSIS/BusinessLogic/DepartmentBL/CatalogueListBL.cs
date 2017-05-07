using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Model;
using DataAccess.DepartmentDA;

namespace BusinessLogic.DepartmentBL
{
    public class CatalogueListBL
    {
        CatalogueListDA cda = new CatalogueListDA();
        public List<InventoryStockBO> getCatalogList()
        {
            return cda.getCatalogList();
        }
    }
}
