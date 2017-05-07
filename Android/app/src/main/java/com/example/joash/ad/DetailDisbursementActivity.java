package com.example.joash.ad;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import java.util.List;



//Activity to display the Disbursement List

public class DetailDisbursementActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_detail_disbursement);
        setTitle("Disbursement List");
        ActionBar ab = getSupportActionBar();
        ab.setDisplayHomeAsUpEnabled(true);
        final ListView l = (ListView) findViewById(R.id.list1);
        Intent i = getIntent();
        String deptId = i.getStringExtra("dept");
        new AsyncTask<String, Void, List<Disbursement>>() {
            @Override
            protected List<Disbursement> doInBackground(String... params) {
                return Disbursement.DisbursementList(params[0]);
            }

            @Override
            protected void onPostExecute(List<Disbursement> req) {

                l.setAdapter(new SimpleAdapter
                        (getApplicationContext(), req, R.layout.disrow2, new String[]{"ItemName", "ItemUOM", "OrderQuantity", "OutstandingQuantity", "DisbursementQuantity"},
                                new int[]{R.id.textView4, R.id.textView6, R.id.textView8, R.id.textView10, R.id.t2}));


            }
        }.execute(deptId);

    }
}
