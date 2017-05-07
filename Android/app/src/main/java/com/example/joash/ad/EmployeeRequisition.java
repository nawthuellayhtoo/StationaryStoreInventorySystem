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


//To get the Requisition by an Employee

public class EmployeeRequisition extends HashMap<String,String> {

    final static String host = "http://172.17.248.177/joashwcf/Service.svc/";

    public EmployeeRequisition(String ReqID, String EmpName,String commentsEmp) {
        put("RequisitionId", ReqID);
        put("EmpName", EmpName);
        put("CommentsEmp",commentsEmp);
    }

    public EmployeeRequisition() {
    }



//To get the list of Employee Requisitions


    public static List<EmployeeRequisition> getRequisition(String deptid) {

       List<EmployeeRequisition>l=new ArrayList<EmployeeRequisition>();



        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(host + "Requisition?DepartmentId=" + deptid);
            Log.e("ee",a.toString());


            for (int i = 0; i < a.length(); i++) {


                JSONObject b = a.getJSONObject(i);
                l.add(new EmployeeRequisition(b.getString("EmpName"),b.getString("RequisitionId"),b.getString("CommentsEmp")));

            }
        }
        catch (Exception ex){

        }
      return l;


    }
}