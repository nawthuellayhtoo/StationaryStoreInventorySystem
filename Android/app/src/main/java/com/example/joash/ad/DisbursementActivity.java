package com.example.joash.ad;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;
import java.util.List;


//To display the List of departments

public class DisbursementActivity extends AppCompatActivity implements AdapterView.OnItemClickListener{

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_disbursement);
        setTitle("Department List");

       final ListView l=(ListView)findViewById(R.id.l);
        new AsyncTask<Void, Void,List<String>>() {
            @Override
            protected List<String> doInBackground(Void... params) {
                return Employee.DepList();
            }
            @Override
            protected void onPostExecute(List<String> req) {

                ArrayAdapter<String> adapter = new ArrayAdapter<String>(getApplicationContext(),
                        R.layout.disrow, R.id.textView2, req);
                 l.setAdapter(adapter);



            }
        }.execute();

        l.setOnItemClickListener(this);


    }



    //Onclick Options for the Adapter



    @Override
    public void onItemClick(AdapterView<?> av, View v, int position, long id) {
        String item = (String) av.getAdapter().getItem(position);
        Toast.makeText(getApplicationContext(), item + " selected",
                Toast.LENGTH_LONG).show();
        Intent i=new Intent(this,CollectionActivity.class);
        i.putExtra("dep",item);
        startActivity(i);
    }



}
