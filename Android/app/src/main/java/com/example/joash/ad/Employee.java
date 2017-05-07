package com.example.joash.ad;

import android.util.Log;
import org.json.JSONArray;
import org.json.JSONObject;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
/**
 * Created by JOASH on 27/1/2017.
 */




public class Employee extends HashMap<String,String> {

    final static String host = "http://172.17.248.177/joashwcf/Service.svc/";

  static DateFormat df = new SimpleDateFormat("yyyy/mm/dd");

    public Employee(String EmpID, String DepartmentID, String EmpTitle, String EmpName, String Email, String Delegate, String Auth,String delegateStartDate,String delegateEndDate) {

        put("EmpId", EmpID);
        put("DepartmentId", DepartmentID);
        put("EmpTitle", EmpTitle);
        put("EmpName", EmpName);
        put("Email", Email);
        put("Delegate", Delegate);
        put("DelegateStartDate",delegateStartDate);
        put("DelegateEndDate",delegateEndDate);
        put("Auth", Auth);


    }

    public Employee() {
    }


    //To get the List of Employee Id

    public static List<String> EmployeeList() {
        List<String> emp = new ArrayList<String>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(host);
            for (int i = 0; i < a.length(); i++) {
                String c = a.getString(i);
                emp.add(c);
            }

        } catch (Exception e) {

        }

        return emp;

    }


    //To get the list of Employee by a particular department


    public  static List<Employee>getEmpByDep(String deptId){
        List<Employee> emp = new ArrayList<Employee>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(host + "EmpByDep?DepartmentId=" + deptId);
            Log.e("c", a.toString());
            for (int i = 0; i < a.length(); i++) {

                JSONObject c = a.getJSONObject(i);
                emp.add(new Employee(c.getString("EmpId"), c.getString("DepartmentId"), c.getString("EmpTitle"), c.getString("EmpName"),c.getString("Email"),Integer.toString(c.getInt("Delegate")),Integer.toString(c.getInt("Auth")),c.getString("DelegateStartDate"),c.getString("DelegateEndDate")));
            }
        } catch (Exception e) {

        }
         return emp;

    }


    //To get the List of Departments


    public static List<String> DepList() {
        List<String> dep = new ArrayList<String>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(host+"GetDepList");
            Log.e("aaa",a.toString());
            for (int i = 0; i < a.length(); i++) {
                String c = a.getString(i);
                dep.add(c);
            }

        } catch (Exception e) {

        }

        return dep;

    }


    //To get the list of Collection Points for a particular department


    public static List<String> getCP(String deptId) {

        List<String>l=new ArrayList<String>();
        String b=null;
        try {
            JSONArray c = JSONParser.getJSONArrayFromUrl(host + "GetCP?DepartmentId=" + deptId);

            for (int i = 0; i < c.length(); i++) {
                b = c.getString(i);
                l.add(b);
            }


        } catch (Exception e) {

        }

        return l;


    }



    //To get the list of Collection Times for a particular department



    public static List<String> getCT(String deptId) {

        List<String>l=new ArrayList<String>();
        String b=null;
        try {
            JSONArray c = JSONParser.getJSONArrayFromUrl(host + "GetCT?DepartmentId=" + deptId);

            for (int i = 0; i < c.length(); i++) {
                b = c.getString(i);
                l.add(b);
            }


        } catch (Exception e) {

        }

        return l;


    }



//To authenticate a particular User



    public static Employee getEmployee(String un,String psd) {
        Employee emp=new Employee() ;
        try {
            JSONObject c = JSONParser.getJSONFromUrl(host +"Authenticate?username="+un+"&password="+psd);
            Log.e("c", c.toString());
            emp= new Employee(c.getString("EmpId"), c.getString("DepartmentId"), c.getString("EmpTitle"), c.getString("EmpName"),c.getString("Email"),Integer.toString(c.getInt("Delegate")),Integer.toString(c.getInt("Auth")),c.getString("DelegateStartDate"),c.getString("DelegateEndDate"));
        } catch (Exception e) {
        }
        Log.e("dddddddddddddc", emp.toString());
        return emp;


    }


    //To check if a particular Employee is a Department Representative


    public static boolean IsRep(String EmpId){
        boolean b=false;
        List<String>rep=new ArrayList<String>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(host+"Representatives");
            for (int i = 0; i < a.length(); i++) {
                String c = a.getString(i);
                rep.add(c);
            }

            int size= rep.size();
            for(int i=0;i<size;i++){


                if(rep.get(i).equals(EmpId)){
                    b=true;
                }

            }


        }catch (Exception e){}

     return b;
    }



    //To get the Department Head for a particular department


    public static Employee getDepHead(String deptId){
        Employee emp=new Employee() ;
        try {
            JSONObject c = JSONParser.getJSONFromUrl(host +"DepartmentHead?DepartmentId="+deptId);
            emp= new Employee(c.getString("EmpId"), c.getString("DepartmentId"), c.getString("EmpTitle"), c.getString("EmpName"),c.getString("Email"),Integer.toString(c.getInt("Delegate")),Integer.toString(c.getInt("Auth")),c.getString("DelegateStartDate"),c.getString("DelegateEndDate"));
        } catch (Exception e) {
        }
        return emp;
    }


    //To Delegate a particular Employee


    public static void delegateEmp(String DeptId,String EmpName, String startDate, String endDate){
        try{
            String empTempName = EmpName.replaceAll(" ", "_");
            JSONObject c = JSONParser.getJSONFromUrl(host +"DelegateEmp?DepartmentId="+DeptId.trim()+"&EmployeeName="+empTempName+"&StartDate="+startDate+"&EndDate="+endDate);
        }
        catch (Exception e){}


    }


    //To Dedelegate a particular Employee


    public static Void dedelegateEmp(String DeptId,String EmpName){

        try{
            String empTempName = EmpName.replaceAll(" ", "_");
            JSONObject c = JSONParser.getJSONFromUrl(host +"DeDelegateEmp?DepartmentId="+DeptId.trim()+"&EmployeeName="+empTempName);
        }
        catch (Exception e){}

        return null ;
    }



    //To get the Delegated Employee of a particular Department



    public static Employee getDelEmployee(String deptId){
        Employee emp=new Employee() ;
        try {
            JSONObject c = JSONParser.getJSONFromUrl(host +"GetDelEmployee?DepartmentId="+deptId);
            emp= new Employee(c.getString("EmpId"), c.getString("DepartmentId"), c.getString("EmpTitle"), c.getString("EmpName"),c.getString("Email"),Integer.toString(c.getInt("Delegate")),Integer.toString(c.getInt("Auth")),c.getString("DelegateStartDate"),c.getString("DelegateEndDate"));
        } catch (Exception e) {
        }
        return emp;

    }


    //To get the Department Representative of a particular Department


    public static Employee GetRep(String deptId){

        Employee emp=new Employee() ;
        try {
            JSONObject c = JSONParser.getJSONFromUrl(host +"Rep?DepartmentId="+deptId);
            emp= new Employee(c.getString("EmpId"), c.getString("DepartmentId"), c.getString("EmpTitle"), c.getString("EmpName"),c.getString("Email"),Integer.toString(c.getInt("Delegate")),Integer.toString(c.getInt("Auth")),c.getString("DelegateStartDate"),c.getString("DelegateEndDate"));
        } catch (Exception e) {
        }
        return emp;

    }



    //To update an Employee as the New Department Representative


    public  static void UpdateRep(String deptId,String EmpName){
        try{
            String empTempName = EmpName.replaceAll(" ", "_");
            JSONObject c = JSONParser.getJSONFromUrl(host +"UpdateRep?DepartmentId="+deptId.trim()+"&EmpName="+empTempName);
        }
        catch (Exception e){}

    }


}