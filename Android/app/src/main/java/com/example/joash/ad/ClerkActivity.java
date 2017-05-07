package com.example.joash.ad;

import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;
import java.util.ArrayList;


//The starting Activity for the clerk

public class ClerkActivity extends AppCompatActivity
{

    MyCustomAdapter dataAdapter = null;
    String[] arrTemp;
    ListView l;
    String ret;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_clerk);
        setTitle("Retrieval List");
        l = (ListView) findViewById(R.id.list);
        arrTemp = new String[100];


        //To get the retrieval list needed for the Clerk

        AsyncTask<Void, Void, ArrayList<RetrievalList>> asyncTask = new AsyncTask<Void, Void, ArrayList<RetrievalList>>() {
            @Override
            protected ArrayList<RetrievalList> doInBackground(Void... params) {
                return RetrievalList.listRetrieval();
            }

            @Override
            protected void onPostExecute(ArrayList<RetrievalList> result) {
                dataAdapter = new MyCustomAdapter(getApplicationContext(),R.layout.clerkrow, result);
                l.setAdapter(dataAdapter);
            }
        }.execute();


    }


    //Menu options for the Clerk Activity (Disbursement List and Logout)


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu1, menu);
        return true;
    }


    //OnClick Actions for the menu for the Clerk

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {

            case R.id.DisbursementList:
                Intent l = new Intent(this,DisbursementActivity.class);
                startActivity(l);
                return true;

            case R.id.Logout:
                Intent k = new Intent(this, MainActivity.class);
                Toast.makeText(this, "Successfully Logged Out", Toast.LENGTH_SHORT).show();
                startActivity(k);
                return true;

            default:
                return super.onOptionsItemSelected(item);

        }


    }





// Custom Adapter class to display the Listview


    public class MyCustomAdapter extends ArrayAdapter<RetrievalList>   {

        private ArrayList<RetrievalList> rList;
        int resource;

        public MyCustomAdapter(Context context, int resource,
                               ArrayList<RetrievalList> rList) {

            super(context, resource , rList);
            this.resource = resource;
            this.rList = rList;
            Log.e("rrr", rList.toString());

        }



//ViewHolder Class to define the controls in each row of the Listview

        private class ViewHolder {
            TextView Bin;
            TextView ItemName;
            TextView Needed;
            int ref;
        }

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {


            RetrievalList d = getItem(position);
            final ViewHolder viewHolder;
            if (convertView == null) {
                viewHolder = new ViewHolder();
                LayoutInflater inflater = LayoutInflater.from(getContext());
                convertView = inflater.inflate(R.layout.clerkrow, parent, false);
                viewHolder.Bin = (TextView) convertView.findViewById(R.id.textView17);

                viewHolder.ItemName = (TextView) convertView.findViewById(R.id.textView24);
                viewHolder.Needed = (TextView) convertView.findViewById(R.id.textView25);

                convertView.setTag(viewHolder);


            } else {
                viewHolder = (ViewHolder) convertView.getTag();
            }

            viewHolder.ref = position;
            viewHolder.Bin.setText(d.getBin());
            viewHolder.ItemName.setText(d.getItemName());
            viewHolder.Needed.setText(Integer.toString(d.getNeeded()));



            return convertView;
        }
    }

}