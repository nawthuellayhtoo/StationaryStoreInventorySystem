package com.example.joash.ad;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Toast;
import java.util.List;


//Starting Activity for the Department Head


public class DepartmentHeadActivity extends AppCompatActivity implements AdapterView.OnItemClickListener {


    String eh;
    String deptId;
    String delegate;



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_departmenthead);
        setTitle("Requisition List");
       final ListView lv = (ListView) findViewById(R.id.listviewdp);
        Intent i = getIntent();
        deptId = i.getStringExtra("DepartmentId");
        delegate=i.getStringExtra("Delegate");
       eh=i.getStringExtra("eh");


        //To get the list of Requisition

        new AsyncTask<String, Void,List<EmployeeRequisition>>() {
            @Override
            protected List<EmployeeRequisition> doInBackground(String... params) {
                return EmployeeRequisition.getRequisition(params[0]);
            }
            @Override
            protected void onPostExecute(List<EmployeeRequisition> req) {

                lv.setAdapter(new SimpleAdapter
                        (getApplicationContext(), req, R.layout.dhrow, new String[]{"EmpName","RequisitionId"},
                                new int[]{ R.id.textView13, R.id.textView14}));

            }
        }.execute(deptId);

        lv.setOnItemClickListener(this);

    }


    //To pass the EmployeeRequisition object to the next activity ApproveRequisition


    @Override
    public void onItemClick(AdapterView<?> av, View v, int position, long id) {
        EmployeeRequisition s = (EmployeeRequisition) av.getAdapter().getItem(position);
        Intent intent = new Intent(this, ApproveRequisitionActivity.class);
        intent.putExtra("AR", s);
        intent.putExtra("Delegate",delegate);
        intent.putExtra("DepartmentId",deptId);
        intent.putExtra("eh",eh);
        startActivity(intent);

    }


    //Different menus on the type of way the activity is accessed


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        if (eh.equals("a")) {

            getMenuInflater().inflate(R.menu.menudh, menu);
            return true;
        }
      else  if (eh.equals("c")) {
            getMenuInflater().inflate(R.menu.menudh2, menu);
            return true;
        }

        else if(eh.equals("b")) {
            getMenuInflater().inflate(R.menu.menudh3, menu);
            return true;
        }

        else {
            return false;
        }
    }



    //OnClick Actions for the Menu

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.DelegateAuthority:
               Intent i=new Intent(this,DelegateAuthorityActivity.class);

                Toast.makeText(this, "Delegate Authority", Toast.LENGTH_SHORT).show();
                i.putExtra("c","b");
                i.putExtra("Delegate",delegate);
                i.putExtra("eh",eh);
                i.putExtra("DepartmentId",deptId);
               startActivity(i);
                return true;

            case R.id.UDS:
                Intent l=new Intent(this,DepartmentRepActivity.class);
                l.putExtra("DepartmentId",deptId);
                l.putExtra("Delegate",delegate);
                l.putExtra("eh",eh);
                l.putExtra("cc","b");
                startActivity(l);
                return true;



            case R.id.AssignRep:
             Intent j=new Intent(this,AssignRepresentativeActivity.class);

                Toast.makeText(this, "Assign Representative", Toast.LENGTH_SHORT).show();
                j.putExtra("DepartmentId",deptId);
                j.putExtra("Delegate",delegate);
                j.putExtra("eh",eh);
               startActivity(j);
                return true;
            case R.id.Logout:
                Intent k=new Intent(this,MainActivity.class);
                Toast.makeText(this, "Successfully Logged Out", Toast.LENGTH_SHORT).show();
                startActivity(k);

            default:
                return super.onOptionsItemSelected(item);
        }

    }


}
