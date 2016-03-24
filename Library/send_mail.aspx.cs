using System;
using System.Net;
using System.Net.Mail;

namespace Library
{
    public partial class send_mail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String myemailid = "yvss.santosh@gmail.com";
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(myemailid, "password");
                MailMessage message = new MailMessage();
                message.To.Add("to address");
                message.From = new MailAddress(myemailid);
                message.Subject = "subject";
                message.Body = "body";
                client.Send(message);
               System.Web.HttpContext.Current.Response.Write("<script language='JavaScript'>alert('Message Successfully Sent')</script>");
            }
            catch (Exception ex)
            {
               System.Web.HttpContext.Current.Response.Write(ex.Message);
            }
        }
    }
}
