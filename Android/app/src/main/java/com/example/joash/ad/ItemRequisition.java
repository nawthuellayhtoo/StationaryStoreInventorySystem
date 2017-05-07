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



//To get the list of Items Requested


public class ItemRequisition extends HashMap<String,String>{

    final static String host = "http://172.17.248.177/joashwcf/Service.svc/";

    public ItemRequisition(String ItemName, String Quantity,String UOM){


        put("ItemName",ItemName);
        put("Quantity",Quantity);
        put("UOM",UOM);

    }

    public ItemRequisition(){}



    //To get the list of items requested for a particular Requisition Id


    public static List<ItemRequisition>getItemReq(String reqId){

        List<ItemRequisition>l=new ArrayList<ItemRequisition>();

        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(host +"RequisitionDetail?RequisitionId=" + reqId);
            Log.e("ssd",a.toString());
            for (int i = 0; i < a.length(); i++) {

                JSONObject b = a.getJSONObject(i);
                l.add(new ItemRequisition(b.getString("ItemName"),b.getString("ItemUOM"),Integer.toString(b.getInt("Quantity"))));

            }
        }catch (Exception ex){


        }
        return l;


    }


    //To approve Requisition

    public static Boolean ApproveReq(String comment,String reqId) {

        Boolean update = false;
        try {
            String s = JSONParser.getStream(host + "ApproveRequisition?Comment=" + comment + "&RequisitionId=" + reqId);
            Log.e("da",s);

            if (s.equals("true")) {
                update = true;
            }
            else {
                update = false;
            }


        } catch (Exception e) {

        }
        return update;


    }

    //To reject a Requisition


    public static Boolean RejectReq(String comment,String reqId) {

        Boolean update = false;
        try {
            String s = JSONParser.getStream(host + "RejectRequisition?Comment=" + comment + "&RequisitionId=" + reqId);

            if (s.equals("true")) {
                update = true;
            } else {
                update = false;
            }


        } catch (Exception e) {

        }
        return update;


    }

}
