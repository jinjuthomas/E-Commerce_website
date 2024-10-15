using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace project41
{
    public partial class Admin : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select max(Reg_id) from Login";
            string s = obj.Fn_Scalar(sel);
            int id = 0;
            if(s=="")
            {
                id = 1;
            }
            else
            {
                int newid = Convert.ToInt32(s);
                id = newid + 1;
            }
            string ins = "insert into Admin_de values(" + id + ",'" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "'," + TextBox4.Text + ")";
            int i = obj.Fn_Nonquery(ins);
            if(i==1)
            {
                string ins1 = "insert into Login values(" + id + ",'" + TextBox5.Text + "','" + TextBox6.Text + "','Admin','Active')";
                int j = obj.Fn_Nonquery(ins1);
            }
        }
    }
}