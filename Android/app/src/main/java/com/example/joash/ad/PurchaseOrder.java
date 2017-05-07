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

//To display the List of Purchase Orders

public class PurchaseOrder extends HashMap<String,String>{

    final static String host = "http://172.17.248.177/joashwcf/Service.svc/";

    public PurchaseOrder(String PONumber,String GeneratedBy,String CommentByClerk){
        put("PONumber",PONumber);
        put("GeneratedBy",GeneratedBy);
        put("commentByClerk",CommentByClerk);
    }

    public static List<PurchaseOrder> jread(){
        List<PurchaseOrder> list = new ArrayList<PurchaseOrder>();
        JSONArray a = JSONParser.getJSONArrayFromUrl(host+"PurchaseOrderList");
        try {
            for (int i =0; i<a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                list.add(new PurchaseOrder(b.getString("PONumber"), b.getString("GeneratedBy"),b.getString("CommentByClerk")));
            }
        } catch (Exception e) {
            Log.e("Purchase Order", "JSONArray error");
        }
        return(list);
    }

}