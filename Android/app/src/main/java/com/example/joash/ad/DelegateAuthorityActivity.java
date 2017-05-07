package com.example.joash.ad;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;




//To delegate Authority to a particular Employee of a particular Department


public class DelegateAuthorityActivity extends AppCompatActivity {


    private Calendar calendar;
    private TextView dateView;
    private TextView dateView1;
    private int year, month, day;
    String c;
    String deptId;
    static DateFormat df = new SimpleDateFormat("dd/MM/yyyy");
    String delegate;
    int p;
    String eh;






    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_delegate_authority);
        setTitle("Delegate Authority");
        ActionBar ab = getSupportActionBar();
        ab.setDisplayHomeAsUpEnabled(true);
        dateView = (TextView) findViewById(R.id.textView);
        dateView1=(TextView)findViewById(R.id.textView4);
        Intent i = getIntent();
        c= i.getStringExtra("c");
        delegate=i.getStringExtra("Delegate");
        eh=i.getStringExtra("eh");
      deptId=i.getStringExtra("DepartmentId");
        calendar = Calendar.getInstance();
        year = calendar.get(Calendar.YEAR);
        month = calendar.get(Calendar.MONTH);
        day = calendar.get(Calendar.DAY_OF_MONTH);
        showDate(year, month+1, day);
        showDate1(year,month+1,day);
       final Button b1=(Button)findViewById(R.id.button1);
        final Button b=(Button)findViewById(R.id.button);
       final Button deactivate=(Button)findViewById(R.id.button4);
       final Button activate=(Button)findViewById(R.id.button2);
        final Spinner sItems = (Spinner) findViewById(R.id.spinner);
        int del=Integer.parseInt(delegate);



        //To activate a particular Employee as the New Delegated head of a particular Department


            if (del == 0) {


                deactivate.setVisibility(View.GONE);

                activate.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {

                        try {


                            String sd1 = dateView.getText().toString();
                            String ed1 = dateView1.getText().toString();
                            Date startDate = df.parse(sd1);
                            Date endDate = df.parse(ed1);
                            Calendar c = Calendar.getInstance();
                            String strDate = df.format(c.getTime());
                            Date now = df.parse(strDate);
                            if (sd1.equals(null) || ed1.equals(null)) {
                                Toast.makeText(getApplicationContext(), "Enter value", Toast.LENGTH_LONG).show();
                            } else if (startDate.after(endDate)) {
                                Toast.makeText(getApplicationContext(), "Enter Correct Range", Toast.LENGTH_LONG).show();

                            } else if (endDate.equals(startDate)) {
                                Toast.makeText(getApplicationContext(), "Enter Correct Range", Toast.LENGTH_LONG).show();

                            } else if (startDate.before(now)) {
                                Toast.makeText(getApplicationContext(), "Enter Correct Range", Toast.LENGTH_LONG).show();

                            } else {

                                //Correct Entry of values

                                final String name = sItems.getSelectedItem().toString();
                                Log.e("name", name);
                                Log.e("sd1", sd1);
                                Log.e("ed1", ed1);
                                Log.e("ccc", deptId);


                                new AsyncTask<String, Void, Void>() {

                                    @Override
                                    protected Void doInBackground(String... params) {
                                        Employee.delegateEmp(params[0], params[1], params[2], params[3]);
                                        return null;

                                    }

                                    @Override
                                    protected void onPostExecute(Void req) {
                                        Toast.makeText(getApplicationContext(), name + " Delegated", Toast.LENGTH_LONG).show();
                                        activate.setEnabled(false);
                                        Intent i=new Intent(getApplicationContext(),DepartmentHeadActivity.class);
                                        int x=1;
                                        i.putExtra("Delegate",Integer.toString(x));
                                        i.putExtra("eh",eh);
                                        i.putExtra("DepartmentId",deptId);
                                        startActivity(i);


                                    }
                                }.execute(deptId, name, sd1, ed1);

                            }
                        } catch (Exception e) {
                            Toast.makeText(getApplicationContext(), "Error", Toast.LENGTH_LONG).show();
                        }


                    }
                });

            }



            //To Deactivate a particular Employee as the New Delegated head of a particular Department



        else{


            activate.setVisibility(View.GONE);



            new AsyncTask<String, Void,Employee>() {

                @Override
                protected Employee doInBackground(String... params) {
                  return Employee.getDelEmployee(params[0]);
                }
                @Override
                protected void onPostExecute(final Employee req) {
              try {
                   final String s = req.get("EmpName");
                    String d1 = req.get("DelegateStartDate");
                    String d2 = req.get("DelegateEndDate");

                   dateView.setText(df.format(df.parse(d1)));
                   dateView1.setText(df.format(df.parse(d2)));
                   b.setEnabled(false);
                  b1.setEnabled(false);
                  sItems.setSelection(p);
                  sItems.setEnabled(false);
                  sItems.setClickable(false);
                  Log.e("sas",deptId);
                  Log.e("ded",s);


                    deactivate.setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {

                            new AsyncTask<String, Void,Void>() {
                                @Override
                                protected Void doInBackground(String... params) {
                                    return Employee.dedelegateEmp(params[0], params[1]);

                                }

                                @Override
                                protected void onPostExecute(Void req) {

                                    Toast.makeText(getApplicationContext(), s+" DeDelegated",Toast.LENGTH_LONG).show();
                                    deactivate.setEnabled(false);
                                    int x=0;
                                    delegate=Integer.toString(x);


                                }
                            }.execute(deptId,s);

                        }
                    });
              }
              catch (Exception e){}

                }
            }.execute(deptId);


        }




//To display the Employee details by DepartmentId




        new AsyncTask<String, Void,List<Employee>>() {
            @Override
            protected List<Employee> doInBackground(String... params) {
                return Employee.getEmpByDep(params[0]);

            }

            @Override
            protected void onPostExecute(List<Employee> req) {

                List<String>l=new ArrayList<String>();
                for (Employee e:req
                     ) {
                    l.add(e.get("EmpName"));

                }

              ArrayAdapter<String> adapter = new ArrayAdapter<String>(
                        getApplicationContext(), android.R.layout.simple_spinner_item, l);
                adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                sItems.setAdapter(adapter);

            }
        }.execute(deptId);


        //ActionListener on Spinner Control


        sItems.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int myPosition, long myID) {

                sItems.setSelection(myPosition);
                p=myPosition;


            }
            @Override
            public void onNothingSelected(AdapterView<?> parentView) {

            }

        });

        }





    //To select the menu options Onclick actions



    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        switch (item.getItemId()) {

            case android.R.id.home:
                Intent intent = new Intent(this, DepartmentHeadActivity.class);
                intent.putExtra("DepartmentId", deptId);
                intent.putExtra("Delegate", delegate);
                intent.putExtra("eh",eh);
                startActivity(intent);
                finish();
                return true;

            case R.id.Logout:
                Intent k=new Intent(this,MainActivity.class);
                Toast.makeText(this, "Successfully Logged Out", Toast.LENGTH_SHORT).show();
                startActivity(k);


            default:
                return super.onOptionsItemSelected(item);


        }


    }




        public void setDate(View view) {

        showDialog(999);

    }

    public void setDate1(View view) {

        showDialog(1000);

    }


    protected Dialog onCreateDialog(int id) {
        // TODO Auto-generated method stub
        if (id == 999) {
            return new DatePickerDialog(this,
                    myDateListener, year, month, day);
        }
        if(id==1000){
            return new DatePickerDialog(this,
                    myDateListener1, year, month, day);

        }
        return null;
    }


    private DatePickerDialog.OnDateSetListener myDateListener1 = new
            DatePickerDialog.OnDateSetListener() {
                @Override
                public void onDateSet(DatePicker arg0,
                                      int arg1, int arg2, int arg3) {

                    showDate1(arg1, arg2+1, arg3);

                }
            };


    private DatePickerDialog.OnDateSetListener myDateListener = new
            DatePickerDialog.OnDateSetListener() {
                @Override
                public void onDateSet(DatePicker arg0,
                                      int arg1, int arg2, int arg3) {

                   showDate(arg1, arg2+1, arg3);

                }
            };

    private void showDate(int year, int month, int day) {
        dateView.setText(new StringBuilder().append(day).append("/")
                .append(month).append("/").append(year));
    }
    private void showDate1(int year, int month, int day) {
        dateView1.setText(new StringBuilder().append(day).append("/")
                .append(month).append("/").append(year));
    }



    //Different menus on the type of way the activity is accessed

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {

        if (c.equals("a")) {


            getMenuInflater().inflate(R.menu.menu, menu);
            return true;
        }
        if(c.equals("b")){

            getMenuInflater().inflate(R.menu.nomenu, menu);
            return true;

        }
        else{
            return  false;
        }
    }






}

