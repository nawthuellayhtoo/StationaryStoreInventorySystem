package com.example.joash.ad;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import java.util.List;



//To display the Collection Information for a particular Department


public class CollectionActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_collection);
        setTitle("Collection Information");
        ActionBar ab = getSupportActionBar();
        ab.setDisplayHomeAsUpEnabled(true);

        Intent i=getIntent();
        final String depId=i.getStringExtra("dep");
        TextView did=(TextView)findViewById(R.id.textView16);
        final TextView drep=(TextView)findViewById(R.id.textView27);
        final TextView dcp=(TextView)findViewById(R.id.textView29);
        final TextView dct=(TextView)findViewById(R.id.textView31);
        Button gil=(Button)findViewById(R.id.button5);
        gil.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent i=new Intent(getApplicationContext(),DetailDisbursementActivity.class);
                i.putExtra("dept",depId);
                startActivity(i);

            }
        });

        did.setText(depId);



        //To return the Department Representative of a particular Department



        new AsyncTask<String, Void,Employee>() {
            @Override
            protected Employee doInBackground(String... params) {
                return Employee.GetRep(params[0]);

            }

            @Override
            protected void onPostExecute(Employee req) {
                drep.setText(req.get("EmpName"));

            }
        }.execute(depId);


        //To get the Collection Point for a particular Department


        new AsyncTask<String, Void, List<String>>() {
            @Override
            protected  List<String> doInBackground(String... params) {
                return Employee.getCP(params[0]);

            }
            @Override
            protected void onPostExecute( List<String> req) {

                for(int i=0;i<req.size();i++){
                    String c=req.get(0);
                    dcp.setText(c);
                }


            }
        }.execute(depId);



        //To get the Collection Time for a particular Department



        new AsyncTask<String, Void, List<String>>() {
            @Override
            protected  List<String> doInBackground(String... params) {
                return Employee.getCT(params[0]);

            }
            @Override
            protected void onPostExecute( List<String> req) {
                for(int i=0;i<req.size();i++){
                    String c=req.get(0);
                    dct.setText(c);
                }


            }
        }.execute(depId);







    }


}
