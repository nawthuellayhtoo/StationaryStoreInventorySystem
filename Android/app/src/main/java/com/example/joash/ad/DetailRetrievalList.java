package com.example.joash.ad;

import java.util.HashMap;

/**
 * Created by JOASH on 18/1/2017.
 */


// To display the Detail Retrieval List


public class DetailRetrievalList extends HashMap<String,String>{


    int Dept=0;
    int OutstandingQty=0;


    public DetailRetrievalList(int Dept,int OutstandingQty){

this.Dept=Dept;
        this.OutstandingQty=OutstandingQty;


    }

    public int getDept() {
        return Dept;
    }

    public void setDept(int dept) {
        Dept = dept;
    }

    public int getOutstandingQty() {
        return OutstandingQty;
    }

    public void setOutstandingQty(int outstandingQty) {
        OutstandingQty = outstandingQty;
    }
}
