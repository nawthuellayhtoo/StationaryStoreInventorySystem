package com.example.joash.ad;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * Created by JOASH on 18/1/2017.
 */


//To get the Disbursement List for a particular Department



public class DisbursementList {

    final static String host = "http://172.17.248.177/joashwcf/Service.svc/";

    String ItemName = null;
    int OrderedQty = 0;
    int DisbursedQty = 0;
    int OutstandingQty = 0;
    boolean selected = false;

    public DisbursementList(String ItemName, int OrderedQty, int DisbursedQty, int OutstandingQty) {
        super();
        this.ItemName=ItemName;
        this.OrderedQty=OrderedQty;
        this.DisbursedQty=DisbursedQty;
        this.OutstandingQty=OutstandingQty;

    }

    public String getItemName() {
        return ItemName;
    }

    public void setItemName(String itemName) {
        ItemName = itemName;
    }

    public int getOrderedQty() {
        return OrderedQty;
    }

    public void setOrderedQty(int orderedQty) {
        OrderedQty = orderedQty;
    }

    public int getDisbursedQty() {
        return DisbursedQty;
    }

    public void setDisbursedQty(int disbursedQty) {
        DisbursedQty = disbursedQty;
    }

    public int getOutstandingQty() {
        return OutstandingQty;
    }

    public void setOutstandingQty(int outstandingQty) {
        OutstandingQty = outstandingQty;
    }

    public boolean isSelected() {
        return selected;
    }
    public void setSelected(boolean selected) {
        this.selected = selected;
    }

    public static ArrayList<DisbursementList> jread(String DepartmentId){
        ArrayList<DisbursementList> list = new ArrayList<DisbursementList>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(host+"GetDisList?DepartmentId="+DepartmentId);
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new DisbursementList(b.getString("ItemName"), b.getInt("OrderQuantity"),b.getInt("DisbursementQuantity"),b.getInt("Quantity")));
            }
        } catch (Exception e) {
            Log.e("Disbursement List", "JSONArray error");
        }
        return(list);
    }


    //To update the Disbursement Status


    public static Boolean updateDisbusement(List<DisbursementList> disbursementListList){
        Boolean result=true;
        String update = JSONParser.getStream(host+disbursementListList);
        if(update=="Fail"){
            result = false;
        }
        return  result;
    }


}
