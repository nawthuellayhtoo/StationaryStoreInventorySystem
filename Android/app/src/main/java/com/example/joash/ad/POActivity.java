package com.example.joash.ad;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;
import java.util.HashMap;
import java.util.List;


//To display the list of Purchase Oders made by Clerk


public class POActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_po);
        ActionBar ab = getSupportActionBar();
        ab.setDisplayHomeAsUpEnabled(true);
        Intent intent=getIntent();
        final HashMap<String,String> pOItem = (HashMap<String,String>) intent.getExtras().get("PO");
        TextView t1=(TextView)findViewById(R.id.Text1);
        final String PONumber=pOItem.get("PONumber");
        t1.setText(PONumber);
        TextView t2=(TextView)findViewById(R.id.Text2);
        t2.setText(pOItem.get("GeneratedBy"));
        TextView e=(TextView) findViewById(R.id.e1);
        e.setText(pOItem.get("commentByClerk"));
        final ListView l=(ListView)findViewById(R.id.list);


        //Gets a List of Purchase Orders


        new AsyncTask<String, Void, List<PurchaseOrderItem>>() {
            @Override
            protected List<PurchaseOrderItem> doInBackground(String... params) {
                return PurchaseOrderItem.jread(params[0]);
            }
            @Override
            protected void onPostExecute(List<PurchaseOrderItem> result) {
                l.setAdapter(new SimpleAdapter
                        (getApplicationContext(),result,R.layout.suprow2,new String[]{"ItemName", "Quantity"},
                                new int[]{R.id.Text1, R.id.Text2}));

            }
        }.execute(pOItem.get("PONumber"));


        //For Approving Purchase Orders

        final Button btnApprove = (Button) findViewById(R.id.b1);
        btnApprove.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                EditText et=(EditText)findViewById(R.id.e2);
                String commentBySupervisor=et.getText().toString();
                new AsyncTask<String, Void, Boolean>() {
                    @Override
                    protected Boolean doInBackground(String... params) {
                        return PurchaseOrderItem.updatePO(PONumber,"Approved",params[0]);
                    }
                    @Override
                    protected void onPostExecute(Boolean result) {
                        if(result==true){
                            Toast.makeText(getApplicationContext(),PONumber+" Approved",Toast.LENGTH_SHORT).show();
                            btnApprove.setEnabled(false);
                        }
                        else if(result==false){
                            Toast.makeText(getApplicationContext(),"Approved Failed",Toast.LENGTH_SHORT).show();
                        }
                    }}.execute(commentBySupervisor);

            }
        });



        //For rejecting Purchase Orders


        final Button btnReject = (Button) findViewById(R.id.b2);
        btnReject.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                EditText et=(EditText)findViewById(R.id.e2);
                String commentBySupervisor=et.getText().toString();
                new AsyncTask<String, Void, Boolean>() {
                    @Override
                    protected Boolean doInBackground(String... params) {
                        return PurchaseOrderItem.updatePO(PONumber,"Rejected",params[0]);
                    }
                    @Override
                    protected void onPostExecute(Boolean result) {
                        if(result==true){
                            Toast.makeText(getApplicationContext(),PONumber+" Rejected",Toast.LENGTH_SHORT).show();
                            btnReject.setEnabled(false);

                        }
                        else if(result==false){
                            Toast.makeText(getApplicationContext(),"Reject Failed",Toast.LENGTH_SHORT).show();
                        }
                    }}.execute(commentBySupervisor);
            }
        });

    }


}
