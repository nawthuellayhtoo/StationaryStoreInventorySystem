package com.example.joash.ad;

/**
 * Created by JOASH on 27/1/2017.
 */
import java.io.PrintWriter;
import java.io.StringWriter;


//StackTrace Class for WCF Connections


public class StackTrace {
    public static String trace(Exception ex) {
        StringWriter outStream = new StringWriter();
        ex.printStackTrace(new PrintWriter(outStream));
        return outStream.toString();
    }
}
