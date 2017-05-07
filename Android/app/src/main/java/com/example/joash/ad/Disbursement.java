package com.example.joash.ad;

import org.json.JSONArray;
import org.json.JSONObject;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * Created by JOASH on 5/2/2017.
 */


//To get the Disbursement of a particular Department

public class Disbursement extends HashMap<String,String> {

    final static String host = "http://172.17.248.177/joashwcf/Service.svc/";

    public Disbursement(){}


    public Disbursement(String ItemName,String ItemUOM,String OrderQuantity,String OutstandingQuantity,String DisbursementQuantity){

        put("ItemName", ItemName);
        put("ItemUOM", ItemUOM);
        put("OrderQuantity", OrderQuantity);
        put("OutstandingQuantity", OutstandingQuantity);
        put("DisbursementQuantity", DisbursementQuantity);
    }

    public static List<Disbursement> DisbursementList(String deptId) {
        List<Disbursement> dis = new ArrayList<Disbursement>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(host + "GetDisbursementList?DepartmentId=" + deptId);
            for (int i = 0; i < a.length(); i++) {
                JSONObject c = a.getJSONObject(i);
                dis.add(new Disbursement(c.getString("ItemName"), c.getString("ItemUOM"),c.getString("OrderQuantity"),c.getString("OutstandingQuantity"),c.getString("DisbursementQuantity")));
            }

        } catch (Exception e) {

        }
           return dis;
    }
}
