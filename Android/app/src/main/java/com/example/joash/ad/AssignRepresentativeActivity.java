package com.example.joash.ad;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import java.util.ArrayList;
import java.util.List;



//To Assign a new Department Representative for a particular Department

public class AssignRepresentativeActivity extends AppCompatActivity {

    List<String> spinnerArray =  new ArrayList<String>();
    private Spinner spinner1;
    String deptId;
    String name;
    String delegate;
    String eh;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_assign_representative);
        setTitle("Assign Representative");
        ActionBar ab = getSupportActionBar();
        ab.setDisplayHomeAsUpEnabled(true);

        Intent i = getIntent();
        deptId = i.getStringExtra("DepartmentId");
        delegate=i.getStringExtra("Delegate");
        eh=i.getStringExtra("eh");
       final TextView old=(TextView)findViewById(R.id.textView9);
        new AsyncTask<String, Void,Employee>() {
            @Override
            protected Employee doInBackground(String... params) {
                return Employee.GetRep(params[0]);
            }

            @Override
            protected void onPostExecute(Employee req) {
                old.setText(req.get("EmpName"));
            }
        }.execute(deptId);




//To get a list of Employees to be displayed in the Spinner for a particular department

        spinner1 = (Spinner) findViewById(R.id.spinner2);
        new AsyncTask<String, Void, List<Employee>>() {
            @Override
            protected List<Employee> doInBackground(String... params) {
                return Employee.getEmpByDep(params[0]);

            }

            @Override
            protected void onPostExecute(List<Employee> req) {

                List<String> l = new ArrayList<String>();
                for (Employee e : req
                        ) {
                    l.add(e.get("EmpName"));

                }
                ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(getApplicationContext(),
                        android.R.layout.simple_spinner_item, l);
                dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                spinner1.setAdapter(dataAdapter);

            }
        }.execute(deptId);



        //To set the particular Employee chosen from the Spinner

        spinner1.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                spinner1.setSelection(position);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });




        //To assign the New Employee as the New Department Representative


        final Button assign=(Button)findViewById(R.id.button3);


        assign.setOnClickListener(new View.OnClickListener() {


            @Override
            public void onClick(View v) {
                name=spinner1.getSelectedItem().toString();

                new AsyncTask<String, Void, Void>() {

                    @Override
                    protected Void doInBackground(String... params) {
                        Employee.UpdateRep(params[0], params[1]);
                        return null;

                    }

                    @Override
                    protected void onPostExecute(Void req) {

                        Toast.makeText(getApplicationContext(), " New Representative: "+name , Toast.LENGTH_LONG).show();
                        assign.setEnabled(false);
                        Intent i=new Intent(getApplicationContext(),DepartmentHeadActivity.class);
                        i.putExtra("DepartmentId",deptId);
                        i.putExtra("Delegate",delegate);
                        i.putExtra("eh",eh);
                        startActivity(i);

                    }
                }.execute(deptId,name);

            }
        });


            }




    //To perform the particular action when the home button is clicked



    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        switch (item.getItemId()) {

            case android.R.id.home:
                Intent intent = new Intent(this, DepartmentHeadActivity.class);
                intent.putExtra("DepartmentId", deptId);
                intent.putExtra("Delegate", delegate);
                intent.putExtra("eh", eh);
                startActivity(intent);
                finish();
                return true;
            default:
                return super.onOptionsItemSelected(item);


        }
    }






}
