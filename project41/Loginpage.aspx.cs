using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace project41
{
    public partial class Loginpage : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string sel = "select count(reg_id) from Login where username='" + TextBox1.Text + "' and password='" + TextBox2.Text + "'";
            string s = obj.Fn_Scalar(sel);
            if (s == "1")
            {
                string sel1 = "select reg_id from Login where username='" + TextBox1.Text + "'and password='" + TextBox2.Text + "'";
                string id = obj.Fn_Scalar(sel1);
                Session["uid"] = id;
                string sel2 = "select log_type from Login where username='" + TextBox1.Text + "'and password='" + TextBox2.Text + "'";
                string log = obj.Fn_Scalar(sel2);
                if (log == "Admin")
                {
                    Response.Redirect("Adminprof.aspx");
                }
                else if (log == "User")
                {
                    Response.Redirect("Userprof.aspx");
                }
            }
            else
            {
                Label3.Visible = true;
                Label3.Text = "Invalid username and password";
            }
        }
    }
    }
