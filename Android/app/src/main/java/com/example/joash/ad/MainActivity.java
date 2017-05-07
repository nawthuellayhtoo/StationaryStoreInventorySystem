package com.example.joash.ad;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;



//For checking the various user criteria for Login


public class MainActivity extends AppCompatActivity {

    static DateFormat df = new SimpleDateFormat("dd/MM/yyyy");
    Calendar cal = Calendar.getInstance();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        final EditText username=(EditText)findViewById(R.id.e1);
        final EditText password=(EditText)findViewById(R.id.e2);


        Button login=(Button)findViewById(R.id.login);
        login.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                String un=username.getText().toString().toUpperCase();
                String pw=password.getText().toString();


                new AsyncTask<String, Void, Employee>() {
                    @Override
                    protected Employee doInBackground(String... params) {
                        return Employee.getEmployee(params[0],params[1]);
                    }
                    @Override
                    protected void onPostExecute(Employee result) {
                       if(Integer.parseInt(result.get("Auth"))==0){
                           Toast.makeText(getApplicationContext(),"Incorrect Entry",Toast.LENGTH_LONG).show();
                       }
                        else{

                           if(result.get("EmpTitle").equals("Head")&& Integer.parseInt( result.get("Delegate"))==0){
                               String deptid=result.get("DepartmentId");
                               String delegate=result.get("Delegate");
                               Intent i=new Intent(getApplicationContext(),DepartmentHeadActivity.class);
                               i.putExtra("eh","a");
                               i.putExtra("Delegate",delegate);
                               i.putExtra("DepartmentId",deptid);
                               startActivity(i);

                           }

                           else if(result.get("EmpTitle").equals("Head")&& Integer.parseInt( result.get("Delegate"))==1){
                               try {
                                   String deptid = result.get("DepartmentId");
                                   String delegate=result.get("Delegate");
                                   String dsd = result.get("DelegateStartDate");
                                   String ded = result.get("DelegateEndDate");

                                   Log.e("dsd",dsd);
                                   Date dsd1 = df.parse(dsd);
                                   Date ded1=df.parse(ded);

                                   Log.e("dsd1",dsd1.toString());
                                   Log.e("ded1",ded1.toString());

                                   Calendar c = Calendar.getInstance();
                                   String strDate = df.format(c.getTime());
                                   Date now = df.parse(strDate);


                                if(now.before(dsd1)) {


                                    Intent i=new Intent(getApplicationContext(),DepartmentHeadActivity.class);
                                    i.putExtra("eh","a");
                                    i.putExtra("DepartmentId",deptid);
                                    i.putExtra("Delegate",delegate);
                                    startActivity(i);

                                }

                                   else{
                                    Intent i = new Intent(getApplicationContext(), DelegateAuthorityActivity.class);
                                    i.putExtra("c", "a");
                                    i.putExtra("DepartmentId", deptid);
                                    i.putExtra("Delegate",delegate);
                                    startActivity(i);

                                }
                           } catch (Exception e){
                                   Toast.makeText(getApplicationContext(),"Error",Toast.LENGTH_LONG).show();
                               }
                           }


                           else if (result.get("EmpTitle").equals("Employee") && Integer.parseInt(result.get("Delegate")) == 1) {

                               String EmpId = result.get("EmpId");
                              final String deptid=result.get("DepartmentId");
                             final  String delegate=result.get("Delegate");

                               new AsyncTask<String, Void, Boolean>() {
                                   @Override
                                   protected Boolean doInBackground(String... params) {
                                       return Employee.IsRep(params[0]);
                                   }

                                   @Override
                                   protected void onPostExecute(Boolean result) {





                                       if (result == true) {


                                           new AsyncTask<String, Void, Employee>() {
                                               @Override
                                               protected Employee doInBackground(String... params) {
                                                   return Employee.getDepHead(params[0]);
                                               }

                                               @Override
                                               protected void onPostExecute(Employee result1) {
                                                   try {

                                                       String dsd = result1.get("DelegateStartDate");
                                                       String ded = result1.get("DelegateEndDate");
                                                       Log.e("dsd", dsd);

                                                       Date dsd1 = df.parse(dsd);
                                                       Date ded1 = df.parse(ded);

                                                       Log.e("dsd1", dsd1.toString());
                                                       Log.e("ded1", ded1.toString());

                                                       Calendar c = Calendar.getInstance();
                                                       String strDate = df.format(c.getTime());
                                                       Date now = df.parse(strDate);

                                                       Log.e("now", now.toString());

                                                       if (now.after(ded1) || now.before(dsd1)) {
                                                           Intent i = new Intent(getApplicationContext(), DepartmentRepActivity.class);
                                                           i.putExtra("DepartmentId",deptid);
                                                           i.putExtra("cc","a");
                                                           startActivity(i);
                                                       } else {
                                                           Intent i = new Intent(getApplicationContext(), DepartmentHeadActivity.class);
                                                           i.putExtra("DepartmentId", deptid);
                                                           i.putExtra("eh", "c");
                                                           i.putExtra("Delegate", delegate);
                                                           startActivity(i);

                                                       }


                                                   } catch (Exception e) {
                                                   }
                                               }


                                           }.execute(deptid);
                                       }
                                           else {


                                           new AsyncTask<String, Void, Employee>() {
                                               @Override
                                               protected Employee doInBackground(String... params) {
                                                   return Employee.getDepHead(params[0]);
                                               }

                                               @Override
                                               protected void onPostExecute(Employee result1) {
                                                   try {

                                                       String dsd = result1.get("DelegateStartDate");
                                                       String ded = result1.get("DelegateEndDate");
                                                       Log.e("dsd",dsd);

                                                       Date dsd1 = df.parse(dsd);
                                                       Date ded1 = df.parse(ded);

                                                       Log.e("dsd1",dsd1.toString());
                                                       Log.e("ded1",ded1.toString());

                                                       Calendar c = Calendar.getInstance();
                                                       String strDate = df.format(c.getTime());
                                                       Date now = df.parse(strDate);

                                                       Log.e("now",now.toString());

                                                       if(now.after(ded1)||now.before(dsd1)) {

                                                           Toast.makeText(getApplicationContext(),"Duration Exceded",Toast.LENGTH_LONG).show();

                                                       }
                                                       else {

                                                           Intent i = new Intent(getApplicationContext(), DepartmentHeadActivity.class);
                                                           i.putExtra("DepartmentId", deptid);
                                                           i.putExtra("Delegate",delegate);
                                                           i.putExtra("eh", "b");
                                                           startActivity(i);

                                                       }



                                                   }
                                                   catch (Exception e){
                                                       Toast.makeText(getApplicationContext(),"Error",Toast.LENGTH_LONG).show();
                                                   }

                                               }
                                           }.execute(deptid);
                                       }

                                   }
                               }.execute(EmpId);

                           }



                           else if(result.get("EmpTitle").equals("Employee")&&Integer.parseInt( result.get("Delegate"))==0){

                               String EmpId = result.get("EmpId");
                               final String deptid=result.get("DepartmentId");

                               new AsyncTask<String, Void, Boolean>() {
                                   @Override
                                   protected Boolean doInBackground(String... params) {
                                       return Employee.IsRep(params[0]);
                                   }

                                   @Override
                                   protected void onPostExecute(Boolean result) {

                                       if (result == true) {

                                           Intent i=new Intent(getApplicationContext(),DepartmentRepActivity.class);
                                           i.putExtra("DepartmentId",deptid);
                                            i.putExtra("cc","a");
                                           startActivity(i);

                                       }
                                       else {
                                           Toast.makeText(getApplicationContext()," No Authorization",Toast.LENGTH_SHORT).show();
                                       }
                                   }
                               }.execute(EmpId);

                           }




                           else if(result.get("EmpTitle").equals("Supervisor")&&Integer.parseInt( result.get("Delegate"))==0){
                               Intent i=new Intent(getApplicationContext(),SupervisorActivity.class);
                               startActivity(i);
                           }
                           else if(result.get("EmpTitle").equals("Supervisor")&&Integer.parseInt( result.get("Delegate"))==1){

                               Toast.makeText(getApplicationContext(),"ACCESS DENIED",Toast.LENGTH_SHORT).show();

                           }
                           else if(result.get("EmpTitle").equals("Clerk")){

                               Intent i =new Intent(getApplicationContext(),ClerkActivity.class);
                               startActivity(i);

                           }




                       }

                    }
                }.execute(un,pw);


            }
        });

        Button clear=(Button)findViewById(R.id.button);
        clear.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                username.setText("");
                password.setText("");

            }
        });


    }



}
