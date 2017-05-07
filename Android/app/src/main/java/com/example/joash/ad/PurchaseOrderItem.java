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

//To display the list of items in a particular Purchase Order


public class PurchaseOrderItem extends HashMap<String,String>{

    final static String host = "http://172.17.248.177/joashwcf/Service.svc/";

    public PurchaseOrderItem(String ItemName, int Quantity){

        put("ItemName",ItemName);
        put("Quantity",Integer.toString(Quantity));
    }


    //To get the List of Items for a particular Purchase Order


    public static List<PurchaseOrderItem> jread(String PONumber){

        List<PurchaseOrderItem> list = new ArrayList<PurchaseOrderItem>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(host+"PurchaseOrderList/"+PONumber);
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new PurchaseOrderItem(b.getString("ItemName"), b.getInt("Quantity")));
            }
        } catch (Exception e) {
            Log.e("Purchase Order Item", "JSONArray error");
        }
        return(list);
    }

    //To update or Reject a Purchase Order

    public static Boolean updatePO(String PONumber,String status,String commentBySupervisor){
        Boolean result=true;
        String update = JSONParser.getStream(host+"PurchaseOrderList/"+PONumber+"/"+status+"/"+commentBySupervisor);
        if(update=="Fail"){
            result=false;
        }
        return  result;
    }
}
