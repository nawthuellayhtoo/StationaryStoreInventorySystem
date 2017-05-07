using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RetrivalListBL
/// </summary>
public class RetrivalListBL
{

    RetrievalListDA rda = new RetrievalListDA();
    DaToBoConversion dbConversion = new DaToBoConversion();



    public List<string> getItemNameList() //To show in GV
    {

        List<string> reqIdList = rda.getRequitionIdList();
        List<string> itemNameLst1 = rda.getItemNameByReqId(reqIdList);

        List<string> itemNumberLst2 = rda.getItemNumberListWithOutstanding();
        List<string> itemNameLst = new List<string>();
        foreach (string s in itemNumberLst2)
        {
            string n = rda.getItemNameByItemNumber(s);
            itemNameLst.Add(n);
        }
        foreach (string s in itemNameLst1)
        {
            itemNameLst.Add(s);
        }

        List<string> finalItemNameLst = itemNameLst.Distinct().ToList();

        return finalItemNameLst;
    }

    public List<string> getItemNumberLst(List<string> finalItemNameLst)  //To show in GV
    {
        List<string> finalItemNumberLst = new List<string>();
        foreach (string s in finalItemNameLst)
        {
            string itemNumber = rda.getItemNumberByItemName(s);
            finalItemNumberLst.Add(itemNumber);

        }
        return finalItemNumberLst;
    }

    public List<RetrivalBO> getRetrivalLst(List<string> finalItemNameLst) //To show in GV
    {
        List<RetrivalBO> rLst = new List<RetrivalBO>();

        foreach (string s in finalItemNameLst)
        {
            RetrivalBO rbo = new RetrivalBO();
            rbo.ItemName = s;
            string itemNumber = rda.getItemNumberByItemName(s);
            string bin = rda.getBinNumberByItemNumber(itemNumber);
            rbo.Needed = getTotalNeeded(itemNumber);
            rbo.Bin = bin;
            rLst.Add(rbo);

        }

        return rLst;
    }

    public List<string> getDepLst(string itemNumber)  //To show in GV
    {

        List<string> depLst1 = rda.getDepartmentListByItemNumber(itemNumber);
        List<string> depLst2 = rda.getDepartmentListWithOutstanding(itemNumber);

        foreach (string s in depLst2)
        {
            depLst1.Add(s);
        }

        List<string> finalDepLst = depLst1.Distinct().ToList();
        return finalDepLst;

    }
    public List<BreakDownByDepBO> getBreakDownLstByName(string itemName)  //To show in GV in GV
    {
        string itemNumber = rda.getItemNumberByItemName(itemName);
        List<string> list = getDepLst(itemNumber).Distinct().ToList();
        List<BreakDownByDepBO> bdLst = new List<BreakDownByDepBO>();

        foreach (string s in list)
        {
            BreakDownByDepBO bdbo = new BreakDownByDepBO();

            int os = rda.getOutstandingByDepAndItem(s, itemNumber);
            int od = rda.getOrderedQtyByDepAndItem(s, itemNumber);
            bdbo.Outstanding = os;
            bdbo.Needed = (od + os);

            bdbo.Dep = s;
            bdLst.Add(bdbo);
        }
        return bdLst;
    }

    public List<BreakDownByDepBO> updateBreakDownLst(string itemName, int totalRetrieved)  //To show in GV in GV after Retrieved changed
    {
        string itemName1 = itemName.Replace("_", " ");
        string itemNumber = rda.getItemNumberByItemName(itemName1);
        List<string> list = getDepLst(itemNumber).Distinct().ToList();
        List<BreakDownByDepBO> bdLst = new List<BreakDownByDepBO>();

        int totalOrdered = getTotalOrdered(itemNumber);
        int totalOutstanding = getTotalOutstanding(itemNumber);
        foreach (string s in list)
        {
            BreakDownByDepBO bdbo = new BreakDownByDepBO();

            int os = rda.getOutstandingByDepAndItem(s, itemNumber);
            int od = rda.getOrderedQtyByDepAndItem(s, itemNumber);
            bdbo.Dep = s;
            bdbo.Outstanding = os;
            bdbo.Needed = (od + os);
            if (totalRetrieved == totalOutstanding)
            {
                bdbo.Actual = os;
            }
            else if (totalRetrieved < totalOutstanding)
            {
                double d = Convert.ToDouble(os) / Convert.ToDouble(totalOutstanding);
                double a = (totalRetrieved * d);
                double r = Math.Round(a);
                bdbo.Actual = Convert.ToInt32(r);
            }
            else
            {
                double d = Convert.ToDouble(od) / Convert.ToDouble(totalOrdered);
                double a = os + ((totalRetrieved - totalOutstanding) * (d));
                double r = Math.Round(a);
                bdbo.Actual = Convert.ToInt32(r);
            }
            bdLst.Add(bdbo);
        }
        return bdLst;
    }

    public int getStockQty(string itemName)
    {
        return rda.getStockQtyByItemName(itemName);
    }

    public int getReorderLevel(string itemName)
    {
        return rda.getReorderLevelByItemNumber(itemName);
    }
    public int getTotalOrdered(string itemNumber)   //For calculation
    {

        List<string> list = getDepLst(itemNumber).Distinct().ToList();
        List<BreakDownByDepBO> bdLst = new List<BreakDownByDepBO>();
        int totalOrdered = 0;
        foreach (string s in list)
        {
            int od = rda.getOrderedQtyByDepAndItem(s, itemNumber);
            totalOrdered = totalOrdered + od;

        }

        return totalOrdered;
    }

    public int getTotalOutstanding(string itemNumber)   //For calculation
    {

        List<string> list = getDepLst(itemNumber).Distinct().ToList();
        List<BreakDownByDepBO> bdLst = new List<BreakDownByDepBO>();
        int totalOutstanding = 0;
        foreach (string s in list)
        {
            int os = rda.getOutstandingByDepAndItem(s, itemNumber);

            totalOutstanding = totalOutstanding + os;


        }

        return totalOutstanding;
    }
    public int getTotalNeeded(string itemNumber)     //For calculation
    {

        List<string> list = getDepLst(itemNumber).Distinct().ToList();
        List<BreakDownByDepBO> bdLst = new List<BreakDownByDepBO>();
        int totalNeeded = 0;
        foreach (string s in list)
        {
            int os = rda.getOutstandingByDepAndItem(s, itemNumber);
            int od = rda.getOrderedQtyByDepAndItem(s, itemNumber);

            totalNeeded = totalNeeded + os + od;


        }

        return totalNeeded;
    }
    public  List<RetrivalBO> getDataSource()  //To show in GV
    {
        List<string> itemNameLst = getItemNameList().Distinct().ToList();
        List<RetrivalBO> retrivalLst = getRetrivalLst(itemNameLst);

        return retrivalLst;
    }

    public string generateDisbursementId()  //For Update
    {
        string s = rda.getLastDisbursementId();
        string a = s.Substring(2);
        string b = s.Substring(0, 2);
        int n = Convert.ToInt32(a);
        string newDisId = b + (n + 1).ToString();
        return newDisId;

    }

    public void update(List<RetrievalDetailBO> rLst) //For update
    {
        List<string> depIdList = new List<string>();  //new added
        foreach (RetrievalDetailBO rdbo in rLst)
        {

            string itemName = rdbo.Rbo.ItemName;
            string itemNumber = rda.getItemNumberByItemName(itemName);
            int retrieved = rdbo.Rbo.Retrived;
            if (retrieved > 0)
            {
                updateInventoryStock(itemName, retrieved);
                List<BreakDownByDepBO> bdLst = rdbo.BdboLst;
                foreach (BreakDownByDepBO bdbo in bdLst)
                {
                    string depId = bdbo.Dep;

                    int distributed = bdbo.Actual;
                    if (distributed > 0)                 //new added
                    {
                        depIdList.Add(depId);

                    }
                    int outstanding = bdbo.Outstanding;
                    int needed = bdbo.Needed;
                    if (distributed > 0)
                    {
                        if (outstanding > distributed)
                        {
                            //rda.updateOutstandingStatus(itemNumber, depId);
                            int partialOutstanding = outstanding - distributed;
                            rda.updatePartialOutStanding(itemNumber, depId, partialOutstanding);
                        }
                        else if (outstanding == distributed)
                        {
                            rda.updateOutstandingStatus(itemNumber, depId);
                        }
                        else
                        {
                            if (outstanding != 0)
                            {
                                updateRetrivalStatus(depId, itemNumber, distributed);
                                rda.updateOutstandingStatus(itemNumber, depId);
                            }
                            else
                            {
                                updateRetrivalStatus(depId, itemNumber, distributed);
                            }
                        }

                    }
                }
            }
        }
        List<string> finalDepIdLst = depIdList.Distinct().ToList(); //new added

        foreach (string depId in finalDepIdLst)
        {
            sendNotificationForReadyForCollection(depId);
        }

    }

    public void sendNotificationForReadyForCollection(string depId)  //new Added
    {
        string emailId = rda.getEmailIdByDepId(depId);
        string depRepName = rda.getRepNameByDepId(depId);
        string sub = "Notification: Items ready for collection";
        string body = "Dear " + depRepName + ", \n \n Your stationery items are ready for collection. \n \n Regards, \n Stationery Store. ";
        SendEmail se = new SendEmail();
        se.sendEmail(sub, body, emailId);

    }

    public List<ReqIdAndQtyBO> distributeQtyToReq(string depId, string itemNumber, int distributed)
    {

        List<string> reqIdLst = rda.getRequisitionIdByDepAndItem(depId, itemNumber);
        double totalOrdered = 0;
        foreach (string s in reqIdLst)
        {
            int orderedQty = rda.getOrderQuantityByReqIdAndItemNumber(s, itemNumber);
            totalOrdered = totalOrdered + Convert.ToDouble(orderedQty);
        }
        List<ReqIdAndQtyBO> rqLst = new List<ReqIdAndQtyBO>();
        foreach (string s in reqIdLst)
        {
            ReqIdAndQtyBO r = new ReqIdAndQtyBO();
            r.ReqId = s;
            int orderedQty = rda.getOrderQuantityByReqIdAndItemNumber(s, itemNumber);
            double d = Convert.ToDouble(orderedQty);
            double disQty = d / totalOrdered * Convert.ToDouble(distributed);
            double finaldis = Math.Round(disQty);
            r.DisbursedQty = Convert.ToInt32(finaldis);
            rqLst.Add(r);
        }
        return rqLst;
    }
    public void updateInventoryStock(string itemName, int retrieved)
    {
        rda.updateInventoryStockQuanitity(itemName, retrieved);

    }

    public void updateRetrivalStatus(string depId, string itemNumber, int retrieved)  //For update
    {
        //List<string> reqIdLst = rda.getRequitionIdList();


        List<string> reqIdLst = rda.getRequisitionIdByDepAndItem(depId, itemNumber);
        foreach (string s in reqIdLst)
        {


            DisbursementBO dbo = new DisbursementBO();
            string disId = generateDisbursementId();
            dbo.DisbursementId = disId;
            dbo.DepartmentId = depId;
            dbo.ItemNumber = itemNumber;
            dbo.RequisitionId = s;
            dbo.Status = "Ready for collection";
            dbo.OrderQuantity = rda.getOrderQuantityByReqIdAndItemNumber(s, itemNumber);
            List<ReqIdAndQtyBO> rqLst = distributeQtyToReq(depId, itemNumber, retrieved);
            foreach (ReqIdAndQtyBO r in rqLst)
            {
                if (r.ReqId == s)
                {
                    dbo.DisbursementQuantity = r.DisbursedQty;
                }
            }

            Disbursement d = dbConversion.convertDBoToD(dbo);


            rda.createNewDisbursement(d);
            rda.updateReqStatus(s);



        }
    }


}


