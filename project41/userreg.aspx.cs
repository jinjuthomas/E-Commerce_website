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
    public partial class userreg : System.Web.UI.Page
    {
        conectcls obj = new conectcls();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string sel = "select state_id,state_name from statede";
                DataSet ds = obj.Fn_Dataset(sel);
                DropDownList1.DataSource = ds;
                DropDownList1.DataValueField = "State_id";
                DropDownList1.DataTextField = "State_name";
                DropDownList1.DataBind();
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                string sel1 = "select id,dis_name from distb where State_id='"+DropDownList1.SelectedItem.Value+"' ";
                DataSet ds1 = obj.Fn_Dataset(sel1);
                DropDownList2.DataSource = ds1;
                DropDownList2.DataValueField = "id";
                DropDownList2.DataTextField = "dis_name";
                DropDownList2.DataBind();
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select max(Reg_id) from Login";
            string s = obj.Fn_Scalar(sel);
            int id = 0;
            if (s == "")
            {
                id = 1;
            }
            else
            {
                int newid = Convert.ToInt32(s);
                id = newid + 1;
            }
            string ins = "insert into User_de values(" + id + ",'" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "'," + TextBox4.Text + ",'" + TextBox5.Text + "','" + DropDownList1.SelectedItem.Text + "','" + DropDownList2.SelectedItem.Text + "','Active')";
            int i = obj.Fn_Nonquery(ins);
            if (i == 1)
            {
                string ins1 = "insert into login values(" + id + ",'" + TextBox7.Text + "','" + TextBox8.Text + "','User','Active')";
                int j = obj.Fn_Nonquery(ins1);
            }
        }

       
    }
}