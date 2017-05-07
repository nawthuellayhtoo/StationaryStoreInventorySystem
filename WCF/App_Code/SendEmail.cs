using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

/// <summary>
/// Summary description for SendEmail
/// </summary>
public class SendEmail
{
    public void sendEmail(string sub, string body, String emailid)
    {
        try
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("sa43team2@gmail.com", "incrediblesuria");
            smtp.EnableSsl = true;

            MailMessage msg = new MailMessage("sa43team2@gmail.com", emailid);
            msg.Subject = sub;
            msg.Body = body;

            smtp.Send(msg);

        }
        catch (Exception ex)
        {}
    }
}