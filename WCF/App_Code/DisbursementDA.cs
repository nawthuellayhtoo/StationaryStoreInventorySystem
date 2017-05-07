﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DisbursementDA
/// </summary>
public class DisbursementDA
{
    SA43Team2StoreDBEntities context = new SA43Team2StoreDBEntities();
    public List<DisbursedInventoryBO> getDisListByDepId(string depId)
    {
        List<DisbursedInventoryBO> b = new List<DisbursedInventoryBO>();
        List<string> itemNumberwithorders = new List<string>();  //3rd feb
        var query = (from x in context.Disbursements
                     join y in context.InventoryStocks on x.ItemNumber equals y.ItemNumber
                     where x.DepartmentID.Equals(depId) && x.Status.Equals("Ready for collection")
                     select new { y.ItemName, y.ItemNumber, x.RequisitionID, y.ItemUOM, x.OrderQuantity, x.DisbursementQuantity }).ToList();


        foreach (var q in query)
        {
            itemNumberwithorders.Add(q.ItemNumber);  //3rd feb
            var q2 = (from x in context.OutstandingInfoes
                      where x.DepartmentID.Equals(depId) && x.ItemNumber.Equals(q.ItemNumber) && x.Status != "Pending"
                      select x.Quantity).ToList();

            DisbursedInventoryBO di = new DisbursedInventoryBO();
            di.ItemName = q.ItemName;
            di.ItemNumber = q.ItemNumber;
            di.ItemUOM = q.ItemUOM;
            di.OrderQuantity = Convert.ToInt32(q.OrderQuantity);
            di.DisbursementQuantity = Convert.ToInt32(q.DisbursementQuantity);
            if (q2.Count() == 0)
            {
                di.OutstandingQuantity = 0;
            }
            else
            {
                di.OutstandingQuantity = Convert.ToInt32(q2[0]);
            }
            di.RequisitionId = q.RequisitionID;
            b.Add(di);
        }
        List<DisbursedInventoryBO> b1 = getOutstandingWithoutOrders(depId, itemNumberwithorders); //3rd feb
        foreach (DisbursedInventoryBO dbo in b1)
        {
            b.Add(dbo);
        }
        return b;
    }
    public List<DisbursedInventoryBO> getOutstandingWithoutOrders(string depId, List<string> itemNumberwithorders) //3rd feb
    {
        var q2 = (from x in context.OutstandingInfoes
                  where x.DepartmentID.Equals(depId) && x.Status != "Pending"
                  select x);

        List<OutstandingInfo> list = q2.ToList();
        List<DisbursedInventoryBO> l = new List<DisbursedInventoryBO>();
        foreach (OutstandingInfo o in list)
        {

            if (!(itemNumberwithorders.Contains(o.ItemNumber)))
            {
                if (o.Quantity != 0)
                {
                    DisbursedInventoryBO b = new DisbursedInventoryBO();
                    b.ItemNumber = o.ItemNumber;
                    b.ItemName = getItemNameByItemNumber(o.ItemNumber);
                    b.ItemUOM = getUOMbyItemNumber(o.ItemNumber);
                    b.OrderQuantity = 0;

                    b.OutstandingQuantity = (int)o.Quantity;


                    if (o.Status == "Received")
                    {
                        b.DisbursementQuantity = (int)o.Quantity;
                    }
                    else
                    {
                        b.DisbursementQuantity = (int)(o.Quantity - o.PartialPendingQty);
                    }
                    l.Add(b);
                }

            }
        }
        return l;

    }
    public string getItemNameByItemNumber(string itemNumber)  //3rd feb
    {
        InventoryStock i = context.InventoryStocks.Where(x => x.ItemNumber == itemNumber).FirstOrDefault();
        return i.ItemName;
    }

    public string getUOMbyItemNumber(string itemNumber)   //3rd feb
    {
        InventoryStock i = context.InventoryStocks.Where(x => x.ItemNumber == itemNumber).FirstOrDefault();
        return i.ItemUOM;

    }
  
    public void updateDisList(List<ReadyForCollectionBO> rcboLst)
    {
        foreach (ReadyForCollectionBO rbo in rcboLst)
        {
            Disbursement d = (from x in context.Disbursements
                              where x.DepartmentID == rbo.DepId && x.RequisitionID.Equals(rbo.ReqisitionId) && x.ItemNumber == rbo.ItemNumber
                              select x).FirstOrDefault();
            if (d != null)
            {
                d.Status = "Close";
                d.DisbursementQuantity = rbo.DisbursedQuantity;
                context.SaveChanges();
                OutstandingInfo q2 = (from x in context.OutstandingInfoes
                                      where x.DepartmentID.Equals(rbo.DepId) && x.ItemNumber.Equals(rbo.ItemNumber)
                                      select x).FirstOrDefault();

                if (q2 != null)
                {
                    int requestedQty = Convert.ToInt32(d.OrderQuantity);
                    int dispQty = (int)rbo.DisbursedQuantity; //To read from text box
                                                              //Check for outstanding status
                    if (q2.Status != "Partial Received")
                    {
                        if ((requestedQty - dispQty) > 0)
                        {
                            q2.Quantity += (requestedQty - dispQty);
                        }
                        else if ((requestedQty - dispQty) < 0)
                        {
                            q2.Quantity -= (dispQty - requestedQty);

                        }
                        else
                        {
                            q2.Quantity = 0;
                        }


                    }

                    else
                    {
                        if ((requestedQty - dispQty) > 0)
                        {
                            q2.Quantity = q2.PartialPendingQty + (requestedQty - dispQty);
                        }
                        else if ((requestedQty - dispQty) < 0)
                        {
                            q2.Quantity = q2.PartialPendingQty + (dispQty - requestedQty);

                        }
                        else
                        {
                            q2.Quantity = 0;
                        }
                    }

                    if (q2.Quantity > 0)
                    {
                        q2.Status = "Pending";
                    }
                    else
                    {
                        q2.Status = "Received";
                    }
                    context.SaveChanges();
                }

            }



        }




    }



}