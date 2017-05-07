package com.example.joash.ad;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by JOASH on 18/1/2017.
 */


//To display the list of items that need to be retrieved by the Clerk


public class RetrievalList extends HashMap<String,String>{


    final static String host = "http://172.17.248.177/joashwcf/Service.svc/";

    String Bin = null;
    String ItemName = null;
    int Needed = 0;
    int Retrieved=0;


    public RetrievalList(String Bin, String ItemName, int Needed, int Retrieved) {


        this.Bin = Bin;
        this.ItemName = ItemName;
        this.Needed = Needed;
        this.Retrieved = Retrieved;

        put("Bin",Bin);
        put("ItemName",ItemName);
        put("Needed",Integer.toString(Needed));
        put("Retrieved",Integer.toString(Retrieved));
    }

    public RetrievalList(){}


    public String getBin() {
        return Bin;
    }

    public void setBin(String bin) {
        Bin = bin;
    }

    public String getItemName() {
        return ItemName;
    }

    public void setItemName(String itemName) {
        ItemName = itemName;
    }

    public int getNeeded() {
        return Needed;
    }

    public void setNeeded(int neededQty) {
        Needed = neededQty;
    }

    public int getRetrieved() {
        return Retrieved;
    }

    public void setRetrieved(int retrieved) {
        Retrieved = retrieved;
    }



    //To display a list of items to be Retrieved



    public static ArrayList<RetrievalList> listRetrieval(){

        ArrayList<RetrievalList> list = new ArrayList<RetrievalList>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host+"GetRetrievalList");

            for (int i =0; i<a.length(); i++)
            {
                RetrievalList r = null;
                JSONObject j = a.getJSONObject(i);
                r = new RetrievalList(j.getString("Bin"), j.getString("ItemName"),j.getInt("Needed"),j.getInt("Retrieved"));
                list.add(r);
            }
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return list;
    }

}

