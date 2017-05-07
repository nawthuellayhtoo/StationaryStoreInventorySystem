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



//Starting Activity for the Department Representative for a particular Department


public class DepartmentRepActivity extends AppCompatActivity {


    MyCustomAdapter dataAdapter = null;

    private ListView listView;
    String eh;
    String deptId;
    String delegate;
    String cc;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_rep);
        setTitle("Disbursement List");
        Intent intent=getIntent();
        String deptid = intent.getStringExtra("DepartmentId");
        delegate=intent.getStringExtra("Delegate");
        eh=intent.getStringExtra("eh");
        cc=intent.getStringExtra("cc");
        listView=(ListView) findViewById(R.id.list);


        //To get the list of Disbursement


        new AsyncTask<String, Void, ArrayList<DisbursementList>>() {
            @Override
            protected ArrayList<DisbursementList> doInBackground(String... params) {
                return DisbursementList.jread(params[0]);
            }
            @Override
            protected void onPostExecute(ArrayList<DisbursementList> result) {

                for (DisbursementList row:result
                        ) {
                    Log.e("eee",row.toString());
                }

                dataAdapter = new MyCustomAdapter(getApplicationContext(), result);
                listView.setAdapter(dataAdapter);
            }
        }.execute(deptid);



    }


//Custom Adapter class for the Listview


    public class MyCustomAdapter extends ArrayAdapter<DisbursementList> {

        private ArrayList<DisbursementList> dList;


        public MyCustomAdapter(Context context,
                               ArrayList<DisbursementList> dList) {
            super(context,R.layout.reprow, dList);
            this.dList = dList;
        }

        private class ViewHolder {
            TextView ItemName;
            TextView OrderedQty;
            TextView DisbursedQty;
            TextView OutstandingQty;

        }


        @Override
        public View getView(int position, View convertView, ViewGroup parent) {


            DisbursementList d=getItem(position);
            ViewHolder viewHolder;
            if (convertView==null){
                viewHolder=new ViewHolder();
                LayoutInflater inflater=LayoutInflater.from(getContext());
                convertView=inflater.inflate(R.layout.reprow,parent,false);
                viewHolder.ItemName = (TextView) convertView.findViewById(R.id.textView4);
                viewHolder.OrderedQty = (TextView) convertView.findViewById(R.id.textView6);
                viewHolder.DisbursedQty = (TextView) convertView.findViewById(R.id.e1);
                viewHolder.OutstandingQty = (TextView) convertView.findViewById(R.id.textView10);

                convertView.setTag(viewHolder);

            }
            else {
                viewHolder=(ViewHolder)convertView.getTag();
            }
            viewHolder.ItemName.setText(d.ItemName);
            viewHolder.OrderedQty.setText(Integer.toString( d.OrderedQty));
            viewHolder.DisbursedQty.setText(Integer.toString( d.DisbursedQty));
            viewHolder.OutstandingQty.setText(Integer.toString(d.OutstandingQty));

            return convertView;

        }


    }


    //Different menus on the type of way the activity is accessed


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {

        if(cc.equals("a")){


        getMenuInflater().inflate(R.menu.menu, menu);
        return true;

    }
        else if(cc.equals("b")){
            getMenuInflater().inflate(R.menu.nomenu, menu);
            return true;
        }
            return false;
        }




    //OnClick Actions for the Menu


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
}

