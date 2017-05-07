package com.example.joash.ad;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;
import java.util.HashMap;
import java.util.List;


//To Approve or Reject the request made by the Employee

public class ApproveRequisitionActivity extends AppCompatActivity {


    List<ItemRequisition> l;
    String deptId;
    String delegate;
    String eh;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_approve_requisition);
        setTitle("Approve/Reject Order");
        ActionBar ab = getSupportActionBar();
        ab.setDisplayHomeAsUpEnabled(true);

        Bundle b = getIntent().getExtras();
        HashMap<String, String> item = (HashMap<String, String>) b.get("AR");
        setTitle(item.get("EmpName"));
        TextView t1 = (TextView) findViewById(R.id.Text2);
        final String reqId = item.get("EmpName");
        eh=getIntent().getStringExtra("eh");
        Log.e("a",reqId);
       deptId=getIntent().getStringExtra("DepartmentId");
       t1.setText(item.get("RequisitionId"));
        delegate=getIntent().getStringExtra("Delegate");
        String EmpComments=item.get("CommentsEmp");
        TextView e1=(TextView) findViewById(R.id.e1);
        final ListView listView = (ListView) findViewById(R.id.listviewapp);
        e1.setText(EmpComments);

//To approve the request made by the Employee

        final Button approve=(Button)findViewById(R.id.b1);
        approve.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
               EditText e2=(EditText)findViewById(R.id.e2);
                 String SupComm=e2.getText().toString();

                new AsyncTask<String, Void, Boolean>() {
                    @Override
                    protected Boolean doInBackground(String... params) {
                        return ItemRequisition.ApproveReq(params[0], params[1]);
                    }

                    @Override
                    protected void onPostExecute(Boolean req) {

                        if(req==false){
                            Toast.makeText(getApplicationContext(),reqId+" Approved",Toast.LENGTH_SHORT).show();
                            approve.setEnabled(false);

                        }
                        else if(req==true) {
                            Toast.makeText(getApplicationContext(),"Not Approved",Toast.LENGTH_SHORT).show();

                        }



                    }

                }.execute(SupComm, reqId);
            }
        });


        //For rejecting the request made by the Employee


        final Button reject=(Button)findViewById(R.id.b2);
        reject.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                EditText e2 = (EditText) findViewById(R.id.e2);
                String SupComm = e2.getText().toString();

                new AsyncTask<String, Void, Boolean>() {
                    @Override
                    protected Boolean doInBackground(String... params) {
                        return ItemRequisition.RejectReq(params[0], params[1]);
                    }

                    @Override
                    protected void onPostExecute(Boolean req) {

                        if (req == false) {
                            Toast.makeText(getApplicationContext(), reqId + " Rejected", Toast.LENGTH_SHORT).show();
                            reject.setEnabled(false);

                        } else if (req == true) {
                            Toast.makeText(getApplicationContext(), "Not Approved", Toast.LENGTH_SHORT).show();

                        }
                    }

                }.execute(SupComm, reqId);
            }
        });


//To retrieve the list of requisition items and to display in each row of the Listview

        new AsyncTask<String, Void, List<ItemRequisition>>() {
            @Override
            protected List<ItemRequisition> doInBackground(String... params) {
                return ItemRequisition.getItemReq(params[0]);
            }

            @Override
            protected void onPostExecute(List<ItemRequisition> req) {


                listView.setAdapter(new SimpleAdapter
                        (getApplicationContext(), req, R.layout.dhrow2, new String[]{"ItemName", "Quantity", "UOM"},
                                new int[]{R.id.textView21, R.id.textView23, R.id.textView19}));


            }

        }.execute(reqId);
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
