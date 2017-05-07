using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess
{
    public class PurchaseOrderDA
    {
        SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();

        public List<PurchaseOrderBO> getPurchaseOrderById(string userId)
        {
            var query = (from po in context.PurchaseOrders
                         where po.RequestorID == userId
                         orderby po.Status descending, po.POID descending
                         select new { po.POID, po.DeliveryDate, po.DateOfOrder, po.Status, po.ApprovalDate });

            List<PurchaseOrderBO> list = new List<PurchaseOrderBO>();
            foreach (var q in query)
            {
                PurchaseOrderBO obj = new PurchaseOrderBO();
                obj.PoId = q.POID;
                obj.DeliveryDate = q.DeliveryDate;
                obj.DateOfOrder = q.DateOfOrder;
                obj.Status = q.Status;
                obj.ApprovalDate = q.ApprovalDate;
                list.Add(obj);
            }
            return list;
        }

        public List<PurchaseOrderBO> getPurchaseOrderAll()
        {
            var query = (from po in context.PurchaseOrders
                         orderby po.Status descending, po.DateOfOrder descending
                         select new { po.POID, po.DeliveryDate, po.DateOfOrder, po.Status, po.ApprovalDate });

            List<PurchaseOrderBO> list = new List<PurchaseOrderBO>();
            foreach (var q in query)
            {
                PurchaseOrderBO obj = new PurchaseOrderBO();
                obj.PoId = q.POID;
                obj.DeliveryDate = q.DeliveryDate;
                obj.DateOfOrder = q.DateOfOrder;
                obj.Status = q.Status;
                obj.ApprovalDate = q.ApprovalDate;
                list.Add(obj);
            }
            return list;

        }

        public List<PurchaseOrderDetailsJoinBO> getPurchaseOrderDetailsById(string poId)
        {
            var query = (from po in context.PurchaseOrders
                         join pod in context.PurchaseOrderDetails on po.POID equals pod.POID
                         join ins in context.InventoryStocks on pod.ItemNumber equals ins.ItemNumber
                         where po.POID == poId
                         select new { ins.ItemNumber, ins.ItemName, pod.Quantity, pod.SupplierID, ins.Supplier1, ins.Supplier2, ins.Supplier3, ins.ItemUOM, ins.Price1, ins.Price2, ins.Price3 });

            List<PurchaseOrderDetailsJoinBO> list = new List<PurchaseOrderDetailsJoinBO>();
            foreach (var q in query)
            {
                PurchaseOrderDetailsJoinBO obj = new PurchaseOrderDetailsJoinBO();
                obj.ItemNumber = q.ItemNumber;
                obj.ItemName = q.ItemName;
                obj.Quantity = q.Quantity;
                obj.ItemUOM = q.ItemUOM;
                if (q.SupplierID.Equals(q.Supplier1) && !(q.SupplierID.Equals(q.Supplier2)) && !(q.SupplierID.Equals(q.Supplier3)))
                {
                    obj.Supplier = q.Supplier1;
                    obj.Price = (Double)q.Price1;
                    obj.TotalPrice = (Double)q.Quantity * obj.Price;
                }
                else if (q.SupplierID.Equals(q.Supplier2) && !(q.SupplierID.Equals(q.Supplier1)) && !(q.SupplierID.Equals(q.Supplier3)))
                {
                    obj.Supplier = q.Supplier2;
                    obj.Price = (Double)q.Price2;
                    obj.TotalPrice = (Double)q.Quantity * obj.Price;
                }
                else
                {
                    obj.Supplier = q.Supplier3;
                    obj.Price = (Double)q.Price3;
                    obj.TotalPrice = (Double)q.Quantity * obj.Price;
                }
                list.Add(obj);
            }
            return list;
        }

        public PurchaseOrderBO getStatusAndOrderDateById(string poId)
        {
            var query = (from po in context.PurchaseOrders
                         where po.POID == poId
                         select new { po.DateOfOrder, po.Status, po.CommentByClerk, po.CommentsBySupervisor, po.RequestorID });
            PurchaseOrderBO obj = new PurchaseOrderBO();
            foreach (var q in query)
            {
                obj.DateOfOrder = q.DateOfOrder;
                obj.Status = q.Status;
                obj.CommentByClerk = q.CommentByClerk;
                obj.CommentBySupervisor = q.CommentsBySupervisor;
                obj.RequestorId = q.RequestorID;
            }
            return obj;
        }

        public EmployeeBO getRequestorName(string requestorId)
        {
            var query = (from emp in context.Employees
                         where emp.EmpID == requestorId
                         select new { emp.EmpName, emp.Email });
            EmployeeBO obj = new EmployeeBO();
            foreach (var q in query)
            {
                obj.EmployeeName = q.EmpName;
                obj.EmployeeEmail = q.Email;
            }
            return obj;
        }

        public void updateQuantityById(string poId, int quantity, string itemNumber)
        {
            PurchaseOrderDetail pod = context.PurchaseOrderDetails.Where(x => x.POID == poId && x.ItemNumber == itemNumber).FirstOrDefault();
            pod.Quantity = quantity;
            context.SaveChanges();
        }

        public Boolean resubmitPO(string poId, string clerkComment)
        {
            PurchaseOrder po = context.PurchaseOrders.Where(x => x.POID == poId).FirstOrDefault();
            po.CommentByClerk = clerkComment;
            po.DateOfOrder = DateTime.Now;
            if (context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int deletePurchaseOrderDetailsyById(string poId, string supplier)
        {
            int returnId = 0;
            PurchaseOrderDetail pod = context.PurchaseOrderDetails.Where(x => x.POID == poId && x.SupplierID == supplier).FirstOrDefault();
            context.PurchaseOrderDetails.Remove(pod);
            context.SaveChanges();
            PurchaseOrderDetail pod1 = context.PurchaseOrderDetails.Where(x => x.POID == poId).FirstOrDefault();
            if (pod1 == null)
            {
                PurchaseOrder po = context.PurchaseOrders.Where(x => x.POID == poId).FirstOrDefault();
                context.PurchaseOrders.Remove(po);
                context.SaveChanges();
                returnId = 1;
            }
            return returnId;
        }

        public Object getCategory()
        {
            return context.spCategoryList();
        }

        public List<InventoryStock> getItemListByCategory(string category)
        {
            List<InventoryStock> itemLst = context.InventoryStocks.Where(x => x.ItemCategory.Equals(category)).ToList();

            return itemLst;
        }

        public List<InventoryStock> getItemListByCategoryAndSupplier(string category, string supplier)
        {
            List<InventoryStock> itemLst = context.InventoryStocks.Where(x => x.ItemCategory.Equals(category) && (x.Supplier1 == supplier || x.Supplier2 == supplier || x.Supplier3 == supplier)).ToList();

            return itemLst;
        }

        public List<InventoryStock> getCategoryListBySupplier(string supplierName)
        {
            List<InventoryStock> categoryList = context.InventoryStocks.Where(x => x.Supplier1 == supplierName || x.Supplier2 == supplierName || x.Supplier3 == supplierName).Distinct().ToList();

            return categoryList;
        }

        public InventoryStockBO getUOMForItem(string itemName)
        {
            var query = (from invs in context.InventoryStocks
                         where invs.ItemName == itemName
                         select new { invs.ItemUOM });
            InventoryStockBO obj = new InventoryStockBO();
            foreach (var q in query)
            {
                obj.ItemUOM = q.ItemUOM;
            }
            return obj;
        }

        public InventoryStockBO getSupplierForItem(string itemName)
        {
            var query = (from invs in context.InventoryStocks
                         where invs.ItemName == itemName
                         select new { invs.Supplier1, invs.Supplier2, invs.Supplier3 });
            InventoryStockBO obj = new InventoryStockBO();
            foreach (var q in query)
            {
                obj.Supplier1 = q.Supplier1;
                obj.Supplier2 = q.Supplier2;
                obj.Supplier3 = q.Supplier3;
            }
            return obj;
        }

        public PurchaseOrderDetailsJoinBO getPriceForSupplier(string itemName, string supplier)
        {
            var query = (from invs in context.InventoryStocks
                         where invs.ItemName == itemName
                         select new { invs.Supplier1, invs.Supplier2, invs.Supplier3, invs.Price1, invs.Price2, invs.Price3 });
            PurchaseOrderDetailsJoinBO obj = new PurchaseOrderDetailsJoinBO();
            foreach (var q in query)
            {
                if (supplier == q.Supplier1)
                {
                    obj.Price = (Double)q.Price1;
                }
                else if (supplier == q.Supplier2)
                {
                    obj.Price = (Double)q.Price2;
                }
                else
                {
                    obj.Price = (Double)q.Price3;
                }
            }
            return obj;
        }

        public Boolean updateStatus(string poId, string status, string supervisorComment, string approverId)
        {
            PurchaseOrder po = context.PurchaseOrders.Where(x => x.POID == poId).FirstOrDefault();
            po.Status = status;
            po.ApprovalDate = DateTime.Now;
            po.CommentsBySupervisor = supervisorComment;
            po.ApproverID = approverId;
            if (context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public EmployeeBO getSupervisorId()
        {
            Employee ebo = context.Employees.Where(x => x.EmpTitle == "Supervisor" || (x.EmpTitle == "Manager" && x.Delegate == 1)).FirstOrDefault();
            EmployeeBO empBo = new EmployeeBO();
            empBo.EmployeeId = ebo.EmpID;
            empBo.EmployeeEmail = ebo.Email;
            empBo.EmployeeName = ebo.EmpName;
            return empBo;
        }

        public PurchaseOrderBO countTotalPoId()
        {
            var query = (from po in context.PurchaseOrders
                         orderby po.POID descending
                         select new { po.POID }).First();
            PurchaseOrderBO pob = new PurchaseOrderBO();
            pob.PoId = query.POID;
            return pob;
        }

        public PurchaseOrderDetailsBO countTotalPodId()
        {
            var query = (from pod in context.PurchaseOrderDetails
                         orderby pod.PODetailsID descending
                         select new { pod.PODetailsID }).First();
            PurchaseOrderDetailsBO pbo = new PurchaseOrderDetailsBO();
            pbo.PoDetailsId = query.PODetailsID;
            return pbo;
        }

        public Boolean createPO(PurchaseOrder po)
        {
            context.PurchaseOrders.Add(po);
            if (context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean createPODetails(PurchaseOrderDetail pod)
        {
            context.PurchaseOrderDetails.Add(pod);
            if (context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string getItemNumber(string itemName)
        {
            InventoryStock inv = context.InventoryStocks.Where(x => x.ItemName == itemName).FirstOrDefault();
            return inv.ItemNumber;
        }
    }
}
