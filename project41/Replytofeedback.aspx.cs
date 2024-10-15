using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace project41
{
    public partial class Replytofeedback : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {
            string sel = "select user_email from user_de where user_id= (select user_id from feedback_tab where feedid="+Session["getid"]+")";
            string s = obj.Fn_Scalar(sel);
            TextBox1.Text = s;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string t = TextBox1.Text;
            string s = TextBox2.Text;
            string r = TextBox3.Text;
            SendEmail2("superveg", "vegsuper402@gmail.com", "rgzp pvqp wben shxt", "Ammu", t, s, r);

            string up = "update feedback_tab set rplymsg='" + TextBox3.Text + "',feedstatus= 1 where feedid="+Session["getid"]+"";
            int i = obj.Fn_Nonquery(up);
            if(i==1)
            {
                Label4.Visible = true;
                Label4.Text = "sent.......";
            }
        }
        public static void SendEmail2(string yourName, string yourGmailUserName, string yourGmailPassword, string toName, string toEmail, string subject, string body)

        {
            string to = toEmail; //To address    
            string from = yourGmailUserName; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = body;
            message.Subject = subject;
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential(yourGmailUserName, yourGmailPassword);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}