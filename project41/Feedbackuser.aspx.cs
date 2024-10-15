using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace project41
{
    public partial class Feedbackuser : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ins="insert into feedback_tab values("+Session["uid"]+",'"+TextBox1.Text+"',' ',0)";
            int i = obj.Fn_Nonquery(ins);
            if(i==1)
            {
                Label2.Visible = true;
                Label2.Text = "Submitted";
            }
        }
    }
}